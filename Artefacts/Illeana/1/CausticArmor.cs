using System.Collections.Generic;
using System.Reflection;
using Illeana.Actions;
using Illeana.Features;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Artifacts;

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
        if (state.ship.Get(ModEntry.Instance.TarnishStatus.Status) > 0)
        {
            combat.QueueImmediate(new AStatus
            {
                status = Status.tempShield,
                statusAmount = state.ship.Get(ModEntry.Instance.TarnishStatus.Status),
                targetPlayer = true,
                artifactPulse = Key()
            });
        }
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        List<Tooltip> l = StatusMeta.GetTooltips(ModEntry.Instance.TarnishStatus.Status, 1);
        l.Insert(0, new TTGlossary("status.corrode", ["1"]));
        l.Insert(0, new TTGlossary("status.tempShieldAlt"));
        return l;
    }
}