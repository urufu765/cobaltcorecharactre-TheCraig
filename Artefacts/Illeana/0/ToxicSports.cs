using System.Collections.Generic;
using HarmonyLib;

namespace Illeana.Artifacts;

/// <summary>
/// BALL!!!!!!!!!
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.EventOnly })]
public class ToxicSports : Artifact
{
    public override void AfterPlayerStatusAction(State state, Combat combat, Status status, AStatusMode mode, int statusAmount)
    {
        if (status == Status.corrode && statusAmount > 0)
        {
            combat.QueueImmediate(new AAttack
            {
                damage = state.ship.Get(Status.corrode)
            });
        }
    }


    public override void OnCombatEnd(State state)
    {
        // new ALoseArtifact{artifactType = this.Key()}
        foreach (Artifact artifact in state.artifacts)
        {
            if (artifact.Key() == this.Key())
            {
                artifact.OnRemoveArtifact(state);
            }
        }
        state.artifacts.RemoveAll((Artifact r) => r.Key() == this.Key());
    }


    public override List<Tooltip> GetExtraTooltips()
    {
        return
        [
            new TTGlossary("status.corrode", ["1"])
        ];
    }


}

public static class SashaSportingSession
{
    internal static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: typeof(Events).GetMethod("SashaFootballChoices", AccessTools.all),
            postfix: new HarmonyMethod(typeof(SashaSportingSession), nameof(AddArtifactForSashaEvent_Postfix))
        );
        harmony.Patch(
            original: typeof(FootballFoe).GetMethod("SomeoneGotAGoal", AccessTools.all),
            prefix: new HarmonyMethod(typeof(SashaSportingSession), nameof(RemoveArtifactAfterWinningOrLosingInSashaSportEvent_Prefix))
        );
    }

    private static void AddArtifactForSashaEvent_Postfix(ref List<Choice> __result)
    {
        foreach (Choice c in __result)
        {
            if (c?.key is "Sasha_Midcombat_Yes")
            {
                c.actions.Add(new AAddArtifact
                {
                    artifact = new ToxicSports()
                });
            }
        }
    }

    public static void RemoveArtifactAfterWinningOrLosingInSashaSportEvent_Prefix(Combat c)
    {
        c.Queue(new ALoseArtifact
        {
            artifactType = typeof(ToxicSports).Name
        });
    }
}