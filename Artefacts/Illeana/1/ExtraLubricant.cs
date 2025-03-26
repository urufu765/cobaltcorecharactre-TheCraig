using System.Collections.Generic;
using System.Reflection;
using Illeana.Actions;
using Illeana.Features;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Artifacts;

/// <summary>
/// Gives boots if corrode is present, stall if not
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }, unremovable = true)]
public class ExperimentalLubricant : Artifact
{
    public bool Corroded {get; set;} = false;
    public override void OnTurnStart(State state, Combat combat)
    {
        Corroded = false;
    }

    public override void AfterPlayerStatusAction(State state, Combat combat, Status status, AStatusMode mode, int statusAmount)
    {
        if (!Corroded && status == Status.corrode && statusAmount > 0)
        {
            Corroded = true;
            combat.QueueImmediate(new AStatus
            {
                status = Status.hermes,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key()
            });
        }
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return [new TTGlossary("status.corrode", ["1"]), new TTGlossary("status.hermes", ["1"])];
    }
}