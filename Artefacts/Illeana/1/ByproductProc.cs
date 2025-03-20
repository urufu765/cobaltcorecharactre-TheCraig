using System.Collections.Generic;

namespace Craig.Artifacts;

/// <summary>
/// Remove a corrode when ending a turn with more than 1 energy
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common })]
public class ByproductProcessor : Artifact
{
    public override void OnTurnEnd(State state, Combat combat)
    {
        if (combat.energy > 0)
        {
            combat.QueueImmediate(new AStatus
            {
                status = Status.corrode,
                statusAmount = -1,
                targetPlayer = true,
                artifactPulse = base.Key(),
            });
        }
    }

    public override List<Tooltip> GetExtraTooltips()
    {
        return
        [
            new TTGlossary("status.corrode", ["-1"])
        ];
    }
}