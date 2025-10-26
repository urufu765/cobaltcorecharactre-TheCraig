
using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Microsoft.Extensions.Logging;

namespace Illeana.Artifacts;


[ArtifactMeta(pools = new[] { ArtifactPool.Boss }, unremovable = false)]
public class WarpPrototype : Artifact
{
    // TODO: Find a way to not offer this if Warp Prep doesn't exist
    public override void OnTurnStart(State state, Combat combat)
    {
		if (combat.turn == 1)
		{
			combat.QueueImmediate(new AStatus
			{
				status = Status.perfectShield,
				statusAmount = 1,
				targetPlayer = true,
				artifactPulse = Key()
			});
			combat.QueueImmediate(new AStatus
			{
				status = Status.evade,
				statusAmount = 4,
				targetPlayer = true,
				artifactPulse = Key()
			});
		}
    }

    public override void OnReceiveArtifact(State state)
    {
        string artifactType = "ShieldPrep";
        foreach (Artifact artifact in state.artifacts)
        {
            if (artifact.Key() == artifactType)
            {
                artifact.OnRemoveArtifact(state);
            }
        }
        state.artifacts.RemoveAll(r => r.Key() == artifactType);
        //state.UpdateArtifactCache();
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return [new TTGlossary("status.perfectShield", ["1"]), new TTGlossary("status.evade", [])];
    }
}

public static class WarpPrototypeHelper
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: typeof(ArtifactReward).GetMethod("GetBlockedArtifacts", AccessTools.all),
            postfix: new HarmonyMethod(typeof(WarpPrototypeHelper), nameof(ArtifactRewardPreventer))
        );
    }

    /// <summary>
    /// Adds a condition where if Warp Prep is not in the artifact list, prevent the game from offering Warp Prototype
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
}