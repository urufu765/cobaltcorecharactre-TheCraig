using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Nickel;

namespace Illeana.Artifacts;

/// <summary>
/// Chance of acid
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "Dave::Dave")]
public class ChanceOfAcid : Artifact
{
    public override List<Tooltip>? GetExtraTooltips()
    {
        return 
        [
            new TTGlossary("status.corrode", ["1"])
        ];
    }
}

/// <summary>
/// Blocks corrode damage if the 50% proc
/// </summary>
public static class ShouldWeCorrode
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(ACorrodeDamage), nameof(ACorrodeDamage.Begin)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(DoesItHurt))
        );
    }

    /// <summary>
    /// Skips the corrode damage begin method entirely if the 50% proc
    /// </summary>
    /// <param name="__instance"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    private static bool DoesItHurt(ACorrodeDamage __instance, State s)
    {
        if (
            __instance.targetPlayer &&
            s.ship.Get(Status.corrode) > 0 &&
            s.EnumerateAllArtifacts().Find(a => a is ChanceOfAcid) is ChanceOfAcid coa &&
            s.rngActions.Next() < 0.5
        )
        {
            coa.Pulse();
            return false;  // Skipping prefix
        }
        return true;
    }
}
