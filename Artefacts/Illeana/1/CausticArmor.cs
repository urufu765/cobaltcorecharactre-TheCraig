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
        combat.Queue(new AShielderator());
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return [new TTGlossary("status.shieldAlt"), new TTGlossary("status.corrode")];
    }

}