using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nickel;
using Carrier = (int origCorrode, int origHull, int byproductReduction);

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
/// Blocks corrode damage if the 50% proc, or do things with Byproduct Processor
/// </summary>
public static class ShouldWeCorrode
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(ACorrodeDamage), nameof(ACorrodeDamage.Begin)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(DoesItHurt)),
            finalizer: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(DoesIt))
        );
    }

    /// <summary>
    /// Resets the corrode value
    /// </summary>
    private static void DoesIt(ACorrodeDamage __instance, ref Carrier __state, State s, Combat c)
    {
        if (__state.byproductReduction > 0 && s.ship.hull < __state.origHull)
        {
            c.QueueImmediate(new AStatus
            {
                status = Status.evade,
                statusAmount = 1,
                targetPlayer = true
            });
        }
        s.ship.Set(Status.corrode, __state.origCorrode);
    }

    /// <summary>
    /// Skips the corrode damage begin method entirely if the 50% proc... or negates damage by 1 if ByproductProcessor
    /// </summary>
    /// <returns>Does it skip (false) or not (true)</returns>
    private static bool DoesItHurt(ACorrodeDamage __instance, ref Carrier __state, State s, Combat c)
    {
        if (
            __instance.targetPlayer &&
            s.ship.Get(Status.corrode) > 0 &&
            s.EnumerateAllArtifacts().Find(a => a is ChanceOfAcid) is ChanceOfAcid coa &&
            s.rngActions.Next() < 0.5
        )
        {
            coa.Pulse();
            return false;  // Skipping prefix that doesn't care about the other reductors.
        }
        bool dontSkip = true;
        Ship ship = __instance.targetPlayer? s.ship : c.otherShip;
        int shipCorrode = __instance.targetPlayer? s.ship.Get(Status.corrode) : c.otherShip.Get(Status.corrode);
        int shipHull = __instance.targetPlayer? s.ship.hull : c.otherShip.hull;
        int reducerFromBP = 0;

        if (
            __instance.targetPlayer &&
            s.ship.Get(Status.corrode) > 0 &&
            s.EnumerateAllArtifacts().Find(a => a is ByproductProcessor) is ByproductProcessor bp
        )
        {
            s.ship.Set(Status.corrode, shipCorrode - 1);
            reducerFromBP++;
            double shake = s.shake;
            double shakeShip = s.ship.shake;
            s.ship.NormalDamage(s, c, 1, null);
            s.shake = shake;
            s.ship.shake = shakeShip;
            // if (s.ship.Get(Status.corrode) <= 0)
            // {
            //     dontSkip = false;
            //     s.ship.PulseStatus(Status.corrode);
            // }
            bp.Pulse();
        }
        __state = (shipCorrode, shipHull, reducerFromBP);
        return dontSkip;
    }
}
