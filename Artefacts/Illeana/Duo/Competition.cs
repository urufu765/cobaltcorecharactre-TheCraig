using System.Collections.Generic;
using Illeana.Midrow;
using Nickel;

namespace Illeana.Artifacts;

public enum CompetitionState
{
    Ready,
    IlleanaTiem,
    EddieTiem,
    Depleted
}

/// <summary>
/// Launch corrode 2 missiles
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "TheJazMaster.Eddie::Eddie.EddieDeck")]
public class Competition : Artifact
{
    public CompetitionState ComState{get; set;}
    public override Spr GetSprite()
    {
        if (ModEntry.Instance.shoeanaMode)
        {
            return ComState switch
            {
                CompetitionState.IlleanaTiem => ModEntry.Instance.SprCompetitionShoeana,
                CompetitionState.EddieTiem => ModEntry.Instance.SprCompetitionEddie,
                CompetitionState.Depleted => ModEntry.Instance.SprCompetitionShoeDepleted,
                _ => ModEntry.Instance.SprCompetitionShoe
            };
        }
        return ComState switch
        {
            CompetitionState.IlleanaTiem => ModEntry.Instance.SprCompetitionIlleana,
            CompetitionState.EddieTiem => ModEntry.Instance.SprCompetitionEddie,
            CompetitionState.Depleted => ModEntry.Instance.SprCompetitionDepleted,
            _ => base.GetSprite()
        };
    }
    public override void OnTurnStart(State state, Combat combat)
    {
        if (ComState == CompetitionState.Depleted) ComState = CompetitionState.Ready;
    }
    public override void OnCombatEnd(State state)
    {
        ComState = CompetitionState.Ready;
    }
    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
        if(ModEntry.Instance.Helper.Content.Decks.LookupByUniqueName("TheJazMaster.Eddie::Eddie.EddieDeck")?.Deck == deck)
        {
            if (ComState == CompetitionState.Ready)
            {
                ComState = CompetitionState.IlleanaTiem;
                combat.QueueImmediate(new AStatus
                {
                    status = ModEntry.Instance.Helper.Content.Characters.V2.LookupByDeck(deck)?.MissingStatus.Status??ModEntry.IlleanaTheSnek.MissingStatus.Status,
                    statusAmount = 2,
                    targetPlayer = true,
                    artifactPulse = Key()
                });
            }
            else if (ComState == CompetitionState.EddieTiem)
            {
                ComState = CompetitionState.Depleted;
                combat.QueueImmediate(new AEnergy
                {
                    changeAmount = energyCost,
                    artifactPulse = Key()
                });
            }
        }
        if(deck == ModEntry.Instance.IlleanaDeck.Deck)
        {
            if (ComState == CompetitionState.Ready)
            {
                ComState = CompetitionState.EddieTiem;
                combat.QueueImmediate(new AStatus
                {
                    status = ModEntry.IlleanaTheSnek.MissingStatus.Status,
                    statusAmount = 2,
                    targetPlayer = true,
                    artifactPulse = Key()
                });
            }
            else if (ComState == CompetitionState.IlleanaTiem)
            {
                ComState = CompetitionState.Depleted;
                List<CardAction> actions = card.GetActionsOverridden(state, combat);
                foreach (CardAction action in actions)
                {
                    action.whoDidThis = deck;
                }
                combat.QueueImmediate(
                    actions
                );
                Pulse();
            }
        }
    }
}