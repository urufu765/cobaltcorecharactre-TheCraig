using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DemoMod.External;
using HarmonyLib;

namespace DemoMod.Features;

public class KnowledgeManager : IStatusRenderHook
{
    public KnowledgeManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusRenderHook(this, 0);
        /*
         * It is occasionally helpful to utilize an Artifact hook, while not being an Artifact.
         * Nickel offers methods for hooking into this lifecycle.
         * RegisterBefore allows it to activate before any player-owner artifacts, while RegisterAfter actives after.
         */
        // ModEntry.Instance.Helper.Events.RegisterAfterArtifactsHook("AfterPlayerStatusAction", AfterPlayerStatusAction, 0);
        /*
         * There are times in which you need to hook code onto after a method.
         * Harmony allows us to achieve this on any method.
         * It is advised to name arguments for Harmony patches, to make them unambiguous to the reader.
         */
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AStatus), nameof(AStatus.Begin)),
            postfix: new HarmonyMethod(GetType(), nameof(AStatus_Begin_Postfix))
        );
    }

    /*
     * As an IStatusRenderHook, the KnowledgeManager has the power to make any status render as bars.
     * However, it only knows how to render Knowledge as bars - so it says true to it, and null to anything else.
     */
    public bool? ShouldOverrideStatusRenderingAsBars(State state, Combat combat, Ship ship, Status status, int amount)
    {
        if (status != ModEntry.Instance.KnowledgeStatus.Status) return null;
        return true;
    }

    public (IReadOnlyList<Color> Colors, int? BarTickWidth) OverrideStatusRendering(State state, Combat combat, Ship ship, Status status,
        int amount)
    {
        var expected = GetKnowledgeLimit(ship);
        var current = ship.Get(ModEntry.Instance.KnowledgeStatus.Status);

        var filled = Math.Min(expected, current);
        var empty = Math.Max(expected - current, 0);
        var overflow = Math.Max(current - expected, 0);

        return (Enumerable.Repeat(new Color("fbb954"), filled)
                .Concat(Enumerable.Repeat(new Color("7a3045"), empty)
                .Concat(Enumerable.Repeat(new Color("f78716"), overflow)))
                .ToImmutableList(),
            null);
    }

    /*
     * Harmony Postfixes have access to all the arguments of the original method.
     * The arguments must be the exact same name and type as the original.
     * There are also special arguments that can be added.
     * __instance is the "this" in the original call.
     */
    public static void AStatus_Begin_Postfix(AStatus __instance, State s, Combat c)
    {
        if (__instance.status != ModEntry.Instance.KnowledgeStatus.Status) return;
        
        var ship = __instance.targetPlayer ? s.ship : c.otherShip;
        if (ship.Get(ModEntry.Instance.KnowledgeStatus.Status) < GetKnowledgeLimit(ship)) return;
        
        c.QueueImmediate([
            new AStatus
            {
                status = Status.powerdrive,
                statusAmount = 1,
                targetPlayer = __instance.targetPlayer
            },
            new AStatus
            {
                status = ModEntry.Instance.LessonStatus.Status,
                statusAmount = 1,
                targetPlayer = __instance.targetPlayer
            },
            new AStatus
            {
                status = ModEntry.Instance.KnowledgeStatus.Status,
                statusAmount = -GetKnowledgeLimit(ship),
                targetPlayer = __instance.targetPlayer
            }
        ]);
    }

    public static int GetKnowledgeLimit(Ship ship)
    {
        return 3 + ship.Get(ModEntry.Instance.LessonStatus.Status);
    }
}