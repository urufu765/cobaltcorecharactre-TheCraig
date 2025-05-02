using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// Autopilot when corroded
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.hacker)]
public class ExtraSlip : Artifact
{
    public override int ModifyAutopilotAmount()
    {
        if (MG.inst.g.state.ship.Get(Status.corrode) > 0)
        {
            return 1;
        }
        return 0;
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return [new TTGlossary("status.corrode", ["1"])];
    }
}