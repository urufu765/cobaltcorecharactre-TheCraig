using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// Remove a corrode when ending a turn with more than 1 energy
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.EventOnly })]
public class LightenedLoad : Artifact
{
    public override void AfterPlayerStatusAction(State state, Combat combat, Status status, AStatusMode mode, int statusAmount)
    {
        if (status == ModEntry.Instance.TarnishStatus.Status && statusAmount > 0)
        {
            combat.QueueImmediate(new AStatus
            {
                status = Status.autododgeRight,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key()
            });
        }
    }


    public override List<Tooltip>? GetExtraTooltips()
    {
        List<Tooltip> l = StatusMeta.GetTooltips(ModEntry.Instance.TarnishStatus.Status, 1);
        l.Insert(0, new TTGlossary("status.autododgeRight", ["1"]));
        return l;
    }
}