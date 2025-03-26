using System.Collections.Generic;

namespace Craig.Artifacts;

/// <summary>
/// Gives 1 evade per temporary card gained
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common })]
public class ExternalFuelSource : Artifact
{
    public override void OnPlayerRecieveCardMidCombat(State state, Combat combat, Card card)
    {
        if (card.GetData(state).temporary)
        {
            combat.QueueImmediate(new AStatus
            {
                status = Status.evade,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key()
            });
        }
    }

    public override List<Tooltip> GetExtraTooltips()
    {
        return
        [
            new TTGlossary("cardtrait.temporary"),
            new TTGlossary("status.evade", ["1"])
        ];
    }
}