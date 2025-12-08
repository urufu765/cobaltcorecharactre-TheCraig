using System;
using System.Collections.Generic;
using System.Reflection;
using FMOD;
using FSPRO;
using HarmonyLib;
using Microsoft.Extensions.Logging;

namespace Illeana.Artifacts;

/// <summary>
/// After overheat, store excess heat value, and give that as bonus healing for next heal.
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "Mezz.TwosCompany::Mezz.TwosCompany.IlyaDeck")]
public class ResidualHeatWelder : Artifact
{
    public int LastHeat {get;set;}  // A value set from a prefix. Transferred to ResidualHeat when overheated.
    public int ResidualHeat {get;set;}  // Bonus healing

    public override int? GetDisplayNumber(State s)
    {
        return ResidualHeat > 0? ResidualHeat : null;
    }

    /// <summary>
    /// Sets ResidualHeat to the last extra heat value. This is to keep track of the excess heat until the player overheats.
    /// </summary>
    /// <param name="state"></param>
    /// <param name="combat"></param>
    public override void AfterPlayerOverheat(State state, Combat combat)
    {
        // ModEntry.Instance.Logger.LogInformation("Wowza! Here's the fun! " + LastHeat);
        combat.QueueImmediate(new AHeatWelderHelper
        {
            artifactPulse = Key()
        });
    }

    public override void OnCombatEnd(State state)
    {
        ResidualHeat = LastHeat = 0;
    }

    public override Spr GetSprite()
    {
        return ResidualHeat > 0? base.GetSprite() : ModEntry.Instance.SprResidualHeatWelderInactive;
    }

    public override int ModifyHealAmount(int baseAmount, State state, bool targetPlayer)
    {
        if (ResidualHeat > 0)
        {
            return ResidualHeat;
        }
        return 0;
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return 
        [
            new TTGlossary("status.heat", ["1"]),
            new TTGlossary("action.overheat")
        ];
    }

}

public static class GetExtraHeat
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.OnAfterTurn)),
            prefix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(CheckExtraHeat))
        );
        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(AHeal), nameof(AHeal.Begin)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(UseUpHeal))
        );
    }

    private static void UseUpHeal(State s)
    {
        if (s.EnumerateAllArtifacts().Find(a => a is ResidualHeatWelder) is ResidualHeatWelder rhw && rhw.ResidualHeat > 0)
        {
            rhw.ResidualHeat = 0;
            rhw.Pulse();
        }
    }

    private static void CheckExtraHeat(Ship __instance, State s)
    {
        if (__instance.Get(Status.heat) > __instance.heatTrigger && s.EnumerateAllArtifacts().Find(a => a is ResidualHeatWelder) is ResidualHeatWelder rhw)
        {
            rhw.LastHeat = __instance.Get(Status.heat) - __instance.heatTrigger;
            // ModEntry.Instance.Logger.LogInformation("Wowza! Here's the heat! " + rhw.LastHeat);
        }
    }
}

/// <summary>
/// An action that gives the artifact power up a suitable delay
/// </summary>
public class AHeatWelderHelper : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.4;
        if (s.EnumerateAllArtifacts().Find(a => a is ResidualHeatWelder) is ResidualHeatWelder rhw)
        {
            if (rhw.LastHeat <= 0)
            {
                timer = 0;
                return;
            }

            rhw.ResidualHeat = rhw.LastHeat;
            Audio.Play(Event.Status_PowerUp);
            rhw.LastHeat = 0;
        }
    }
}