using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace Illeana.Artifacts;

/// <summary>
/// Gain 1 more evade
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.riggs)]
public class ThrustThursters : Artifact
{
    public bool Depleted {get; set;} = false;

    public override Spr GetSprite()
    {
        return Depleted? ModEntry.Instance.SprThurstDepleted : base.GetSprite();
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        Depleted = false;
    }

    public override void OnCombatEnd(State state)
    {
        Depleted = false;
    }

    public override void AfterPlayerStatusAction(State state, Combat combat, Status status, AStatusMode mode, int statusAmount)
    {
        if(!Depleted && status == Status.evade && mode == AStatusMode.Add && statusAmount > 0)
        {
            combat.QueueImmediate(
                new AStatus
                {
                    status = Status.evade,
                    targetPlayer = true,
                    statusAmount = state.ship.Get(Status.evade) + 1,
                    mode = AStatusMode.Set,
                    artifactPulse = this.Key()
                }
            );
            Depleted = true;
        }
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return [new TTGlossary("status.evade", ["1"])];
    }
}

/// <summary>
/// Make Evade go bloop bloop faster
/// </summary>
public static class ThrustMaster
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AStatus), nameof(AStatus.Begin)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AStatusExceptFaster))
        );
    }

    private static void AStatusExceptFaster(AStatus __instance, State s)
    {
        if (
            __instance.status == Status.evade &&
            __instance.mode == AStatusMode.Add &&
            Math.Abs(__instance.timer - AStatus.TIMER_DEFAULT) < 0.0001 &&
            s.EnumerateAllArtifacts().Find(a => a is ThrustThursters) is ThrustThursters tt &&
            !tt.Depleted
            )
        {
            __instance.timer = 0.2;
        }
    }
}