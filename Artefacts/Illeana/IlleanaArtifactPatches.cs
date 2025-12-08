using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FMOD;
using FSPRO;
using HarmonyLib;
using Microsoft.Extensions.Logging;

namespace Illeana.Artifacts;

/// <summary>
/// To combine and simplify all the artifact patches at once... abandoned since it's better that the artifact files each have their own.
/// </summary>
public static class IlleanaArtifactPatches
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: typeof(ArtifactReward).GetMethod("GetBlockedArtifacts", AccessTools.all),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(ArtifactRewardPreventer))
        );

        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(ACorrodeDamage), nameof(ACorrodeDamage.Begin)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(DoesItHurt))
        );

        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AMove), nameof(AMove.Begin)),
            prefix: new HarmonyMethod(typeof(IlleanaArtifactPatches.JitterTheStick), nameof(JitterTheStick.OriginalPositionCheck)) {priority = Priority.First},
            postfix: new HarmonyMethod(typeof(IlleanaArtifactPatches.JitterTheStick), nameof(JitterTheStick.FinalPositionCheck)) {priority = Priority.Last},
            finalizer: new HarmonyMethod(typeof(IlleanaArtifactPatches.JitterTheStick), nameof(JitterTheStick.CleanupCheck))
        );

        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AOverheat), nameof(AOverheat.Begin)),
            prefix: new HarmonyMethod(typeof(IlleanaArtifactPatches.HeatpumpLubricator), nameof(HeatpumpLubricator.HeatPumper)),
            finalizer: new HarmonyMethod(typeof(IlleanaArtifactPatches.HeatpumpLubricator), nameof(HeatpumpLubricator.HeatPumpCleaner))
        );
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.DirectHullDamage)),
            prefix: new HarmonyMethod(typeof(IlleanaArtifactPatches.HeatpumpLubricator), nameof(HeatpumpLubricator.DontHurtMe))
        );
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Audio), nameof(Audio.Play), [typeof(GUID), typeof(bool)]),
            prefix: new HarmonyMethod(typeof(IlleanaArtifactPatches.HeatpumpLubricator), nameof(HeatpumpLubricator.ReplaceBoomWithFizz))
        );

        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnAfterTurn)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(CheckExtraHeat))
        );

        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AHeal), nameof(AHeal.Begin)),
            prefix: new HarmonyMethod(typeof(IlleanaArtifactPatches.HealToShield), nameof(HealToShield.OriginalHullCheck)),
            postfix: new HarmonyMethod(typeof(IlleanaArtifactPatches.HealToShield), nameof(HealToShield.OverflowHeal)),
            finalizer: new HarmonyMethod(typeof(IlleanaArtifactPatches.HealToShield), nameof(HealToShield.CleanupHullCheck))
        );

        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AStatus), nameof(AStatus.Begin)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(AStatusExceptFaster))
        );

        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.Set)),
            prefix: new HarmonyMethod(typeof(IlleanaArtifactPatches.ShardStorageUnlimiter), nameof(ShardStorageUnlimiter.FlagUpOnSet)),
            postfix: new HarmonyMethod(typeof(IlleanaArtifactPatches.ShardStorageUnlimiter), nameof(ShardStorageUnlimiter.UpdateExcessShards)),
            finalizer: new HarmonyMethod(typeof(IlleanaArtifactPatches.ShardStorageUnlimiter), nameof(ShardStorageUnlimiter.FlagCleanup))
        );
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.GetMaxShard)),
            postfix: new HarmonyMethod(typeof(IlleanaArtifactPatches.ShardStorageUnlimiter), nameof(ShardStorageUnlimiter.MaximiseShard))
        );

    }

    /// <summary>
    /// Adds a condition where if Warp Prep is not in the artifact list, prevent the game from offering Warp Prototype. Also used to prevent artifacts that are just killers on super low HP.
    /// </summary>
    /// <param name="s"></param>
    /// <param name="__result"></param>
    private static void ArtifactRewardPreventer(State s, ref HashSet<Type> __result)
    {
        try
        {
            if (!s.EnumerateAllArtifacts().Any(a => a is ShieldPrep))
            {
                __result.Add(typeof(WarpPrototype));
            }
            if (!s.EnumerateAllArtifacts().Any(a => a is PersonalStereo))
            {
                __result.Add(typeof(SportsStereo));
                __result.Add(typeof(DigitalizedStereo));
            }
            if (s.EnumerateAllArtifacts().Any(a => a is SportsStereo))
            {
                __result.Add(typeof(PersonalStereo));
            }
            if (s.EnumerateAllArtifacts().Any(a => a is DigitalizedStereo))
            {
                __result.Add(typeof(PersonalStereo));
            }
            if (s.ship is not null && s.ship.hullMax <= 3)
            {
                __result.Add(typeof(ByproductProcessor));
                __result.Add(typeof(CausticArmor));
                __result.Add(typeof(PersonalStereo));
                
            }
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Fuck, fucked up adding WarpPrototype to the list of don'ts");
        }
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
            return true;  // Skipping prefix
        }
        return false;
    }

    /// <summary>
    /// A solution that only accommodates for the in-game AMove and any subclasses that call the base method.
    /// </summary>
    private static class JitterTheStick // TODO: Find a better solution that will work with all types of movements like PAWSAI's Pivot.
    {
        private static int lastPos = int.MaxValue;

        /// <summary>
        /// Saves the last position of the ship
        /// </summary>
        /// <param name="s">State</param>
        internal static void OriginalPositionCheck(State s)
        {
            lastPos = s.ship.x;
        }

        /// <summary>
        /// Compares the position of the ship to determine if the movement was restricted or not. If so, give 1 evade.
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="s"></param>
        /// <param name="c"></param>
        internal static void FinalPositionCheck(AMove __instance, State s, Combat c)
        {
            if (
                __instance.targetPlayer && 
                __instance.dir != 0 && 
                lastPos == s.ship.x && 
                s.ship.shake > 0  // Check for if no movement was caused by the wall or not.
            )
            {
                if (s.EnumerateAllArtifacts().Find(a => a is LooseStick) is LooseStick ls && ls.StallingLeft > 0)
                {
                    c.QueueImmediate(new AStatus
                    {
                        status = Status.evade,
                        statusAmount = 1,
                        targetPlayer = true,
                        artifactPulse = ls.Key()
                    });
                    ls.StallingLeft--;
                }
            }
        }

        /// <summary>
        /// Resets the value such that the next check doesn't confuse itself with the previous if somehow the prefix is suddenly skipped.
        /// </summary>
        internal static void CleanupCheck()
        {
            lastPos = int.MaxValue;
        }
    }


    /// <summary>
    /// Make overheat not overheat
    /// </summary>
    private static class HeatpumpLubricator
    {
        private static bool onOverheat;

        internal static void ReplaceBoomWithFizz(ref GUID? maybeId)
        {
            if(onOverheat) maybeId = new GUID?(Event.Status_CorrodeApply);
        }

        internal static void DontHurtMe(ref int amt)
        {
            if(onOverheat) amt = 0;
        }

        internal static void HeatPumpCleaner()
        {
            onOverheat = false;
        }

        internal static void HeatPumper(AOverheat __instance, State s)
        {
            if (__instance.targetPlayer && s.EnumerateAllArtifacts().Find(a => a is LubricatedHeatpump) is LubricatedHeatpump lh)
            {
                onOverheat = true;
                s.ship.Add(Status.corrode, 1);
                lh.Pulse();
            }
        }
    }

    /// <summary>
    /// Checks last excess heat and sends it to the welder
    /// </summary>
    /// <param name="__instance"></param>
    /// <param name="s"></param>
    private static void CheckExtraHeat(Ship __instance, State s)
    {
        if (__instance.Get(Status.heat) > __instance.heatTrigger && s.EnumerateAllArtifacts().Find(a => a is ResidualHeatWelder) is ResidualHeatWelder rhw)
        {
            rhw.LastHeat = __instance.Get(Status.heat) - __instance.heatTrigger;
        }
    }

    /// <summary>
    /// Converts heal overflow to shield
    /// </summary>
    private static class HealToShield // TODO: Find a better solution that will work with all types of movements like PAWSAI's Pivot.
    {
        private static int lastHull = 0;

        /// <summary>
        /// Saves the last hull amount of the ship
        /// </summary>
        /// <param name="s">State</param>
        internal static void OriginalHullCheck(State s)
        {
            lastHull = s.ship.hull;
        }

        /// <summary>
        /// Overflows heal into shield.
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="s"></param>
        /// <param name="c"></param>
        internal static void OverflowHeal(AHeal __instance, State s, Combat c)
        {
            if (
                __instance.targetPlayer && 
                s.ship.hull == s.ship.hullMax &&  // Only allow this if it's an overflow
                lastHull <= 0  // And player wasn't dead or sth
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
        internal static void CleanupHullCheck()
        {
            lastHull = 0;
        }
    }

    /// <summary>
    /// Make Evade go bloop bloop faster
    /// </summary>
    /// <param name="__instance"></param>
    /// <param name="s"></param>
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


    /// <summary>
    /// Make shard storage unlimited
    /// </summary>
    private static class ShardStorageUnlimiter
    {
        private static bool isSet;

        internal static void UpdateExcessShards(Ship __instance, Status status)
        {
            if (status == Status.shard)
            {
                isSet = false;
                __instance.Set(ModEntry.Instance.ExcessShardStatus.Status, __instance.Get(Status.shard) - __instance.GetMaxShard());
            }
        }

        internal static void FlagCleanup()
        {
            isSet = false;
        }

        internal static void FlagUpOnSet()
        {
            isSet = true;
        }

        internal static void MaximiseShard(Ship __instance, ref int __result)
        {
            if (isSet && MG.inst.g.state.EnumerateAllArtifacts().Any(a => a is UnprotectedStorage))
            {
                __result = __instance.Get(Status.shard) + UnprotectedStorage.MAXEXCESS;
            }
        }
    }


}