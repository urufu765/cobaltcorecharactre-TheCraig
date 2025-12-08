using System;
using System.Collections.Generic;
using System.Linq;

namespace Illeana.Artifacts;

/// <summary>
/// Playing Jost's Off Balance card does heal based on how many cards played before
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "Mezz.TwosCompany::Mezz.TwosCompany.JostDeck")]
public class TakeABreak : Artifact
{
    //public Type OffBalance => Type.GetType("TwosCompany.Cards.Jost.OffBalance");
    private static Type? OffBalance => ModEntry.Instance.Helper.Content.Cards.LookupByUniqueName("Mezz.TwosCompany::Mezz.TwosCompany.Cards.JostOffBalance")?.Configuration.CardType;

    public int CardsPlayed {get;set;}

    /// <summary>
    /// A flag that is only reset from combat end. True if you get your hands on Off Balance, and stays true even when Off Balance is out of your hands but unplayed.
    /// </summary>
    public bool BreakTime {get;set;}

    public bool BreakUsed {get;set;}

    public override void OnCombatStart(State state, Combat combat)
    {
        BreakTime = BreakUsed = false;
    }

    /// <summary>
    /// Only show number if Off Balance is in hand.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public override int? GetDisplayNumber(State s)
    {
        if (!BreakUsed && s.route is Combat c && c.hand.Any(a => a.GetType() == OffBalance))
        {
            BreakTime = true;
            return CardsPlayed;
        }
        return null;
    }

    public override Spr GetSprite()
    {
        return BreakUsed? ModEntry.Instance.SprTakeABreakDepleted : BreakTime? ModEntry.Instance.SprTakeABreakActive : base.GetSprite();
    }

    public override void OnCombatEnd(State state)
    {
        BreakUsed = BreakTime = false;
        CardsPlayed = 0;
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        CardsPlayed = 0;
    }

    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
        if (!BreakUsed)
        {
            if (card.GetType() == OffBalance)
            {
                combat.QueueImmediate(
                    new AHeal
                    {
                        healAmount = CardsPlayed,
                        targetPlayer = true,
                        artifactPulse = Key()
                    }
                );
                BreakUsed = true;
            }
            else if (combat.hand.Any(a => a.GetType() == OffBalance))
            {
                CardsPlayed++;
            }
        }
    }
}