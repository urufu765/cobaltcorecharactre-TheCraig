using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using FMOD;
using FSPRO;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nanoray.Shrike;
using Nanoray.Shrike.Harmony;

namespace Illeana.Artifacts;

/// <summary>
/// Turn that heal overflow into SHIELD!
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.dizzy)]
public class ResourceOverflowCatcher : Artifact
{
    public override List<Tooltip> GetExtraTooltips()
    {
        return
        [
            new TTGlossary("status.shieldAlt")
        ];
    }
}


/// <summary>
/// Converts heal overflow to shield
/// </summary>
public static class HealToShield // TODO: Find a better solution that will work with all types of movements like PAWSAI's Pivot.
{
    private static int lastHull = 0;
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AHeal), nameof(AHeal.Begin)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(OriginalHullCheck)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(OverflowHeal)),
            finalizer: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(CleanupHullCheck))
        );
    }

    /// <summary>
    /// Saves the last hull amount of the ship
    /// </summary>
    /// <param name="s">State</param>
    private static void OriginalHullCheck(State s)
    {
        lastHull = s.ship.hull;
    }

    /// <summary>
    /// Overflows heal into shield.
    /// </summary>
    /// <param name="__instance"></param>
    /// <param name="s"></param>
    /// <param name="c"></param>
    private static void OverflowHeal(AHeal __instance, State s, Combat c)
    {
        if (
            __instance.targetPlayer && 
            s.ship.hull == s.ship.hullMax &&  // Only allow this if it's an overflow
            lastHull >= 0  // And player wasn't dead or sth
        )
        {
            // Get overflow amount
            int overflow = __instance.RealHealAmount(s, __instance.healAmount) - (s.ship.hull - lastHull);
            if (overflow <= 0) return;  // Negative or zero overflow = not overflow... panic if disputed.
            if (s.EnumerateAllArtifacts().Find(a => a is ResourceOverflowCatcher) is ResourceOverflowCatcher roc)
            {
                c.QueueImmediate(new AStatus
                {
                    status = Status.shield,
                    statusAmount = overflow,
                    targetPlayer = true,
                    artifactPulse = roc.Key()
                });
            }
        }
    }

    /// <summary>
    /// Resets the value such that the next check doesn't confuse itself with the previous if somehow the prefix is suddenly skipped.
    /// </summary>
    private static void CleanupHullCheck()
    {
        lastHull = 0;
    }
}
