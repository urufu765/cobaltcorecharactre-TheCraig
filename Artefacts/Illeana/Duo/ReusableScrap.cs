using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// When shield gained, gain temp shield equal to tarnish
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Unreleased }), DuoArtifactMeta(duoDeck = Deck.dizzy)]
public class ReusableScrap : Artifact
{
    public override void AfterPlayerStatusAction(State state, Combat combat, Status status, AStatusMode mode, int statusAmount)
    {
        if(status == Status.shield && mode == AStatusMode.Add && statusAmount > 0 && state.ship.Get(ModEntry.Instance.TarnishStatus.Status) > 0)
        {
            combat.QueueImmediate(new AStatus
            {
                status = Status.tempShield,
                targetPlayer = true,
                statusAmount = state.ship.Get(ModEntry.Instance.TarnishStatus.Status),
                artifactPulse = this.Key()
            });
        }
    }

    public override List<Tooltip> GetExtraTooltips()
    {
        List<Tooltip> l = StatusMeta.GetTooltips(ModEntry.Instance.TarnishStatus.Status, 1);
        l.Insert(0, new TTGlossary("status.tempShieldAlt"));
        l.Insert(0, new TTGlossary("status.shieldAlt"));
        return l;
    }
}