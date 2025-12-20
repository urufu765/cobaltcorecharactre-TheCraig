using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// Remove a corrode when ending a turn with more than 1 energy
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common })]
public class ByproductProcessor : Artifact
{
    // public override void OnTurnEnd(State state, Combat combat)
    // {
    //     if (state.ship.Get(Status.corrode) > 0)
    //     {
    //         combat.QueueImmediate([
    //         new AStatus
    //         {
    //             status = Status.corrode,
    //             statusAmount = -1,
    //             targetPlayer = true,
    //             artifactPulse = Key(),
    //         },
    //         new AStatus
    //         {
    //             status = Status.evade,
    //             statusAmount = 1,
    //             targetPlayer = true,
    //             artifactPulse = Key()
    //         }
    //         ]);
    //     }
    // }

    public override List<Tooltip> GetExtraTooltips()
    {
        return
        [
            new TTGlossary("status.corrode", ["1"]),
            new TTGlossary("status.evade", ["1"])
        ];
    }
}