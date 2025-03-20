using System.Collections.Generic;
using System.Reflection;
using Craig.Actions;
using Craig.Features;
using Nanoray.PluginManager;
using Nickel;

namespace Craig.Artifacts;

/// <summary>
/// Gives boots if corrode is present, stall if not
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }, extraGlossary = [ "status.corrode", "status.hermes", "status.engineStall" ], unremovable = true)]
public class ExperimentalLubricant : Artifact
{
    public override void OnTurnStart(State state, Combat combat)
    {
        combat.Queue(new ABootsOrNoBoots());
    }
}