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
/// Refund-your-no-movement-due-to-movement-restrictions-inator-2000
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.peri)]
public class LooseStick : Artifact
{
    /// <summary>
    /// Max stall
    /// </summary>
    public const int MAX_STALL = 3;

    /// <summary>
    /// Amount of evades left that can be given per movement action that is mitigated by a restriction.
    /// </summary>
    public int StallingLeft {get;set;} = MAX_STALL;

    public bool InCombat { get; set; } = false;  // Visual purposes

    public override void OnTurnStart(State state, Combat combat)
    {
        StallingLeft = MAX_STALL;  // Refill on turn start
        combat.QueueImmediate(
            new AStatus  // Apply 1 engine stall
            {
                status = Status.engineStall,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key()
            }
        );
    }

    /// <summary>
    /// Just so the sprite used is the full sprite
    /// </summary>
    /// <param name="state"></param>
    public override void OnCombatEnd(State state)
    {
        StallingLeft = MAX_STALL;
        InCombat = false;
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        InCombat = true;
    }

    public override int? GetDisplayNumber(State s)
    {
        return InCombat? StallingLeft : null;
    }

    public override Spr GetSprite()
    {
        return (StallingLeft>0 || !InCombat)?base.GetSprite():ModEntry.Instance.SprLooseStickDepleted;
    }

    public override List<Tooltip> GetExtraTooltips()
    {
        return
        [
            new TTGlossary("status.engineStall", [1]),
            new TTGlossary("status.evade", ["1"])
        ];
    }
}


/// <summary>
/// A solution that only accommodates for the in-game AMove and any subclasses that call the base method. Used both by LooseStick and SwaySwheel
/// </summary>
public static class JitterTheStick // TODO: Find a better solution that will work with all types of movements like PAWSAI's Pivot.
{
    private static int lastPos = int.MaxValue;
    private static int lastEvadePos = int.MaxValue;
    private static bool readyCheck = false;  // To disable duplication
    // private static int accumulator = 0;  // Just to check logs
    // private static int Acc
    // {
    //     get
    //     {
    //         accumulator++;
    //         return accumulator;
    //     }
    // }
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(  // For movement actions that use AMove
            original: AccessTools.DeclaredMethod(typeof(AMove), nameof(AMove.Begin)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(OriginalPositionCheck)) {priority = Priority.VeryHigh + ((Priority.First - Priority.VeryHigh) / 2)},
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(FinalPositionCheck)) {priority = Priority.Low},
            finalizer: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(CleanupCheck))
        );

        harmony.Patch(  // For specifically evade...
            original: AccessTools.DeclaredMethod(typeof(Combat), nameof(Combat.DoEvade)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(OriginalEvadePositionCheck)) {priority = Priority.VeryHigh},
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(FinalEvadePositionCheck)) {priority = Priority.VeryLow},
            finalizer: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(CleanupCheck))
        );
    }

    /// <summary>
    /// Saves the last position of the ship
    /// </summary>
    /// <param name="s">State</param>
    private static void OriginalPositionCheck(State s)
    {
        lastPos = s.ship.x;
        readyCheck = true;
        // ModEntry.Instance.Logger.LogInformation("Jitter_" + Acc + " Hey, Position check! " + lastPos);
    }

    /// <summary>
    /// Saves the last position of the ship for specifically evade.
    /// </summary>
    /// <param name="s">State</param>
    private static void OriginalEvadePositionCheck(G g)
    {
        if (g?.state?.ship is null) return;
        lastEvadePos = g.state.ship.x;
        readyCheck = true;
        // ModEntry.Instance.Logger.LogInformation("Jitter_" + Acc + " Hey, Evade position check! " + lastEvadePos);
    }


    /// <summary>
    /// For AMove
    /// </summary>
    /// <param name="__instance"></param>
    /// <param name="s"></param>
    /// <param name="c"></param>
    private static void FinalPositionCheck(AMove __instance, State s, Combat c)
    {
        DoTheThing(lastPos, __instance.dir, __instance.targetPlayer, s, c, __instance.fromEvade);
    }

    /// <summary>
    /// for Evade
    /// </summary>
    /// <param name="__instance"></param>
    /// <param name="g"></param>
    /// <param name="dir"></param>
    private static void FinalEvadePositionCheck(Combat __instance, G g, int dir)
    {
        if (g?.state?.ship is null) return;
        DoTheThing(lastEvadePos, dir, true, g.state, __instance, true);
    }

    /// <summary>
    /// Compares the position of the ship to determine if it fits artifact conditions.
    /// </summary>
    /// <param name="lastPosition"></param>
    /// <param name="dir"></param>
    /// <param name="targetPlayer"></param>
    /// <param name="s"></param>
    /// <param name="c"></param>
    /// <param name="fromEvade"></param>
    private static void DoTheThing(int lastPosition, int dir, bool targetPlayer, State s, Combat c, bool fromEvade)
    {
        // ModEntry.Instance.Logger.LogInformation("Hey, Position! " + lastPosition);
        // ModEntry.Instance.Logger.LogInformation("Current Pos? " + s.ship.x);
        // ModEntry.Instance.Logger.LogInformation("Is evade? " + fromEvade);
        // ModEntry.Instance.Logger.LogInformation("Dir? " + dir);
        // ModEntry.Instance.Logger.LogInformation("Shake! " + s.ship.shake);
        // ModEntry.Instance.Logger.LogInformation("Target? " + targetPlayer);
        if (
            targetPlayer && 
            dir != 0 &&
            readyCheck
        )
        {
            if (
                lastPosition == s.ship.x && 
                s.ship.shake > 0 && // Check for if no movement was caused by the wall or not.
                s.EnumerateAllArtifacts().Find(a => a is LooseStick) is LooseStick ls && 
                ls.StallingLeft > 0
            )
            {
                int reward = Math.Min(ls.StallingLeft, Math.Abs(dir));
                c.QueueImmediate(new AStatus
                {
                    status = Status.evade,
                    statusAmount = reward,
                    targetPlayer = true,
                    artifactPulse = ls.Key()
                });
                ls.StallingLeft -= reward;
                readyCheck = false;
            }
            else if (
                lastPosition != s.ship.x &&
                s.EnumerateAllArtifacts().Find(a => a is SwaySwheel) is SwaySwheel ss &&
                !ss.ShieldGiven
            )
            {
                c.QueueImmediate(new AStatus
                {
                    status = Status.tempShield,
                    statusAmount = Math.Abs(dir),
                    targetPlayer = true,
                    artifactPulse = ss.Key()
                });
                ss.ShieldGiven = true;
                readyCheck = false;
            }
        }
    }


    /// <summary>
    /// Resets the value such that the next check doesn't confuse itself with the previous if somehow the prefix is suddenly skipped.
    /// </summary>
    private static void CleanupCheck()
    {
        lastPos = lastEvadePos = int.MaxValue;
        readyCheck = false;
    }
}
