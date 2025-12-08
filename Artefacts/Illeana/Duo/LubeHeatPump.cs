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
/// +1 overheat, convert overhat damage to corrode instead
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.eunice)]
public class LubricatedHeatpump : Artifact
{
    public override void OnReceiveArtifact(State state)
    {
        state.ship.heatTrigger += 1;
    }

    public override void OnRemoveArtifact(State state)
    {
        state.ship.heatTrigger -= 1;
    }

    public override List<Tooltip> GetExtraTooltips()
    {
        return
        [
            new TTGlossary("status.corrode", ["1"])
        ];
    }
}

/// <summary>
/// Make overheat not overheat
/// </summary>
public static class HeatpumpLubricator
{
    private static bool onOverheat;
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AOverheat), nameof(AOverheat.Begin)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(HeatPumper)),
            finalizer: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(HeatPumpCleaner))
        );
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.DirectHullDamage)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(DontHurtMe))
        );
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Audio), nameof(Audio.Play), [typeof(GUID), typeof(bool)]),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(ReplaceBoomWithFizz))
        );
    }

    private static void ReplaceBoomWithFizz(ref GUID? maybeId)
    {
        if(onOverheat) maybeId = new GUID?(Event.Status_CorrodeApply);
    }

    private static void DontHurtMe(ref int amt)
    {
        if(onOverheat) amt = 0;
    }

    private static void HeatPumpCleaner()
    {
        onOverheat = false;
    }

    private static void HeatPumper(AOverheat __instance, State s)
    {
        if (__instance.targetPlayer && s.EnumerateAllArtifacts().Find(a => a is LubricatedHeatpump) is LubricatedHeatpump lh)
        {
            onOverheat = true;
            s.ship.Add(Status.corrode, 1);
            lh.Pulse();
        }
    }
}
