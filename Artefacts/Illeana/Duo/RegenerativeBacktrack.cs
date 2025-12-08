using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// On turn end, refund at most half of backtrack
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "TheJazMaster.Nibbs::Nibbs")]
public class RegenerativeBacktrack : Artifact
{
    private static Status? BacktrackLeftLoc => ModEntry.Instance.Helper.Content.Statuses.LookupByUniqueName("TheJazMaster.Nibbs::BacktrackLeft")?.Status;
    private static Status? BacktrackRightLoc => ModEntry.Instance.Helper.Content.Statuses.LookupByUniqueName("TheJazMaster.Nibbs::BacktrackRight")?.Status;
    private Status? BacktrackLeft = null;
    private Status? BacktrackRight = null;

    public override void OnTurnEnd(State state, Combat combat)
    {
        BacktrackLeft ??= BacktrackLeftLoc;
        BacktrackRight ??= BacktrackRightLoc;
        if (BacktrackLeft is null || BacktrackRight is null)
        {
            return;
        }

        int evadeGenerated = (state.ship.Get(BacktrackLeft.Value) + state.ship.Get(BacktrackRight.Value)) / 2;

        if (evadeGenerated > 0)
        {
            combat.QueueImmediate(new AStatus
            {
                status = Status.evade,
                statusAmount = evadeGenerated,
                targetPlayer = true,
                artifactPulse = Key()
            });
        }
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        BacktrackLeft ??= BacktrackLeftLoc;
        BacktrackRight ??= BacktrackRightLoc;
        if (BacktrackLeft is null || BacktrackRight is null)
        {
            return null;
        }

        List<Tooltip> l = StatusMeta.GetTooltips(BacktrackLeft.Value, 1);
        l.AddRange(StatusMeta.GetTooltips(BacktrackRight.Value, 1));
        l.Add(new TTGlossary("status.evade", ["1"]));
        return l;
    }

}