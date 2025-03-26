
namespace Illeana.Artifacts;

/// <summary>
/// Gives the amount of bosses defeated as artifacts, then an artifact per event gone through
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Boss }, unremovable = true)]
public class ConstantInnovation : Artifact
{
    const int bossDefeated = 0;
    public override void OnReceiveArtifact(State state)
    {
        // TODO: Find how many bosses player has defeated
        for (int i = 0; i < bossDefeated; i++)
        {
            state.GetCurrentQueue().QueueImmediate(new AArtifactOffering
            {
                amount = 2,
                limitPools = [ ArtifactPool.Common ]
            });
        }
    }

    // TODO: Find how to make things proc on event trigger
}