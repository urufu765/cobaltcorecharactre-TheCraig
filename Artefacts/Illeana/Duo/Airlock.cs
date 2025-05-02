using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// Start combat with 3 overdrive && 2 missing illeana
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.peri)]
public class AirlockSnek : Artifact
{
    public override void OnCombatStart(State state, Combat combat)
    {
        combat.Queue(
            new AStatus{
                status = ModEntry.IlleanaTheSnek.MissingStatus.Status,
                statusAmount = 2,
                targetPlayer = true
            }
        );
    }
    public override void OnTurnStart(State state, Combat combat)
    {
        if (combat.turn == 1){
            combat.Queue(
                new AStatus{
                    status = Status.overdrive,
                    statusAmount = 3,
                    artifactPulse = Key(),
                    targetPlayer = true
                }
            );
        }
    }

    public override List<Tooltip> GetExtraTooltips()
    {
        List<Tooltip> l = StatusMeta.GetTooltips(ModEntry.IlleanaTheSnek.MissingStatus.Status, 2);
        l.Insert(0, new TTGlossary("status.overdrive", ["3"]));
        return l;
    }
}