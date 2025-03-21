namespace Craig.Artifacts;

/// <summary>
/// Heals 1 per 4 hull lost
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common })]
public class ForgedCertificate : Artifact
{
    public int TimesHit { get; set; }

    public override int? GetDisplayNumber(State s)
    {
        return TimesHit;
    }

    public override void OnPlayerLoseHull(State state, Combat combat, int amount)
    {
        if (TimesHit >= 4)
        {
            combat.QueueImmediate(new AHeal
            {
                targetPlayer = true,
                healAmount = 1,
            });
            TimesHit = 0;
        }
        else
        {
            TimesHit++;
        }
    }
}