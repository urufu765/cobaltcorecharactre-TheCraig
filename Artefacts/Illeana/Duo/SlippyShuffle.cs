using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// Gain 1 oxidation whenever drawing (not amount drawn), then send it to enemy if cards shuffled back to draw. The last bit's gonna be tricky.
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "Mezz.TwosCompany::Mezz.TwosCompany.NolaDeck")]
public class SlippyShuffle : Artifact
{
    private Status Oxidize => ModEntry.Instance.KokoroApi.V2.OxidationStatus.Status;
    public override void OnPlayerDeckShuffle(State state, Combat combat)
    {
        int oxidation = state.ship.Get(Oxidize);

        if (oxidation > 0)
        {
            combat.QueueImmediate([
                new AStatus
                {
                    status = Oxidize,
                    statusAmount = 0,
                    mode = AStatusMode.Set,
                    artifactPulse = Key(),
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Oxidize,
                    statusAmount = oxidation,
                    artifactPulse = Key(),
                    targetPlayer = false
                }
            ]);
        }
    }

    public override void OnDrawCard(State state, Combat combat, int count)
    {
        combat.QueueImmediate(
            new AStatus
            {
                status = Oxidize,
                statusAmount = 1,
                artifactPulse = Key(),
                targetPlayer = true
            }
        );
    }
}