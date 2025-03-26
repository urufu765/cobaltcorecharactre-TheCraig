
namespace Illeana.Artifacts;

/// <summary>
/// Wings count as empty, empty parts take 1 damage when a shot passes through
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Boss }, unremovable = true)]
public class Limbless : Artifact
{
    /// <summary>
    /// All because of HunterWings, gotta make sure that artifact doesn't cause the wing to be a wing again.
    /// </summary>
    public override void OnTurnStart(State state, Combat combat)
    {
        foreach (Part p in state.ship.parts)
        {
            if (p.type == PType.wing)
            {
                p.type = PType.empty;
            }
        }
    }
    /// <summary>
    /// All because of HunterWings, gotta make sure that artifact doesn't cause the wing to be a wing again.
    /// </summary>
    public override void OnTurnEnd(State state, Combat combat)
    {
        foreach (Part p in state.ship.parts)
        {
            if (p.type == PType.wing)
            {
                p.type = PType.empty;
            }
        }
    }

    // TODO add the part that does grazing but on both player and enemy
    public override void OnPlayerDodgeHit(State state, Combat combat)
    {
        base.OnPlayerDodgeHit(state, combat);
    }
}