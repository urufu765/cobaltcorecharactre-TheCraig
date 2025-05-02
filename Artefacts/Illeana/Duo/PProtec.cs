using System.Collections.Generic;
using Illeana.Cards;

namespace Illeana.Artifacts;

/// <summary>
/// Gives a card
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }, unremovable = true), DuoArtifactMeta(duoDeck = Deck.colorless)]
public class PerfectedProtection : Artifact
{
    public override void OnReceiveArtifact(State state)
    {
        state.GetCurrentQueue().Queue(new AAddCard{card = new PerfectShieldColourless()});
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return [new TTCard{card = new PerfectShieldColourless()}];
    }
}