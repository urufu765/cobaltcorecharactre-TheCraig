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
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.shard)]
public class UnprotectedStorage : Artifact
{
    public const int MAXEXCESS = 5;
    // public override void AfterPlayerStatusAction(State state, Combat combat, Status status, AStatusMode mode, int statusAmount)
    // {
    //     if (status == Status.shard)
    //     {
    //         combat.QueueImmediate(
    //             new AStatus
    //             {
    //                 status = ModEntry.Instance.ExcessShardStatus.Status,
    //                 statusAmount = state.ship.Get(Status.shard) - state.ship.GetMaxShard(),
    //                 targetPlayer = true,
    //                 statusPulse = ModEntry.Instance.ExcessShardStatus.Status
    //             }
    //         );
    //     }
    // }
    public override void OnTurnEnd(State state, Combat combat)
    {
        if (state.ship.Get(Status.shard) > state.ship.GetMaxShard())
        {
            combat.QueueImmediate(
                new AHurt
                {
                    hurtAmount = state.ship.Get(Status.shard) - state.ship.GetMaxShard(),
                    hurtShieldsFirst = true,
                    targetPlayer = true,
                    artifactPulse = Key()
                }
            );
        }
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        int maxAmount = (MG.inst.g?.state ?? DB.fakeState).ship.GetMaxShard();
        if (maxAmount == 0) maxAmount = 3;
        return [new TTGlossary("status.shard", [maxAmount])];
    }
}

public static class ShardStorageUnlimiter
{
    private static bool isSet;
    private static bool isVerySet;
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.Set)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(FlagUpOnSet)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(UpdateExcessShards)),
            finalizer: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(FlagCleanup))
        );
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.GetMaxShard)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(MaximiseShard))
        );
    }

    private static void UpdateExcessShards(Ship __instance, Status status)
    {
        if (status == Status.shard)
        {
            isSet = false;
            __instance.Set(ModEntry.Instance.ExcessShardStatus.Status, __instance.Get(Status.shard) - __instance.GetMaxShard());
        }
    }

    private static void FlagCleanup()
    {
        isSet = false;
    }

    private static void FlagUpOnSet()
    {
        //ModEntry.Instance.Logger.LogInformation("BANG");
        isSet = true;
    }

    private static void MaximiseShard(Ship __instance, ref int __result)
    {
        if (isSet && MG.inst.g.state.EnumerateAllArtifacts().Any(a => a is UnprotectedStorage))
        {
            //ModEntry.Instance.Logger.LogInformation("BOOM!");
            __result = __instance.Get(Status.shard) + UnprotectedStorage.MAXEXCESS;
        }
    }
}
