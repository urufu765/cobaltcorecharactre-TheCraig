using System.Collections.Generic;
using System.Reflection;
using Craig.Actions;
using Craig.Features;
using Nanoray.PluginManager;
using Nickel;

namespace Craig.Artifacts;

/// <summary>
/// Remove a corrode when ending a turn with more than 1 energy
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }, unremovable = true)]
public class CausticArmor : Artifact
{
    public override void OnTurnStart(State state, Combat combat)
    {
        if (state.ship.Get(Status.corrode) > 0)
        {
            combat.QueueImmediate(new AStatus
            {
                status = Status.tempShield,
                statusAmount = state.ship.Get(Status.corrode) * 2,
                targetPlayer = true,
                artifactPulse = Key()
            });
        }
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return [new TTGlossary("status.tempShield", ["1"]), new TTGlossary("status.corrode", ["1"])];
    }

}