using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Illeana.Artifacts;

/// <summary>
/// Equalize evade and droneshift
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.goat)]
public class FuelEqualizer : Artifact
{
    public bool startOfTurn;
    public override void OnTurnStart(State state, Combat combat)
    {
        startOfTurn = true;
    }

    public override void OnQueueEmptyDuringPlayerTurn(State state, Combat combat)
    {
        if (startOfTurn)
        {
            int equality = state.ship.Get(Status.evade) + state.ship.Get(Status.droneShift);
            // ModEntry.Instance.Logger.LogInformation("Equality: " + equality);
            if (equality % 2 == 1) equality++;
            // ModEntry.Instance.Logger.LogInformation("Equality Now: " + equality);
            // ModEntry.Instance.Logger.LogInformation("Equality Div: " + (equality / 2));

            combat.Queue([
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = equality / 2,
                    artifactPulse = Key(),
                    targetPlayer = true,
                    mode = AStatusMode.Set
                },
                new AStatus
                {
                    status = Status.droneShift,
                    statusAmount = equality / 2,
                    artifactPulse = Key(),
                    targetPlayer = true,
                    mode = AStatusMode.Set
                },
            ]);
        }
        startOfTurn = false;
    }


    public override List<Tooltip>? GetExtraTooltips()
    {
        return 
        [
            new TTGlossary("status.evade", ["1"]),
            new TTGlossary("status.droneShift", ["1"]),
        ];
    }
}