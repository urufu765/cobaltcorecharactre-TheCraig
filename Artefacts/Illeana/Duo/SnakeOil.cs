using System;
using System.Collections.Generic;
using FMOD;
using FSPRO;
using Illeana.Cards;
using Nickel;

namespace Illeana.Artifacts;


/// <summary>
/// Let's go gambling!
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "Shockah.Dracula::Dracula")]
public class BountifulBloodBank : Artifact
{
    public int Encounter {get; set;}
    public static List<int> BaseBloods {get;} = [4, 2, 1];
    public int Deposited {get; set;}
    public int HullNotMaxDeposited {get; set;}
    

    private int Withdrawal()
    {
        return Deposited + Deposited / BaseBloods[Encounter];
    }

    public override int? GetDisplayNumber(State s)
    {
        return Withdrawal();
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        if (state?.map?.markers[state.map.currentLocation]?.contents is MapBattle mb)
        {
            Encounter = mb.battleType switch
            {
                BattleType.Elite => 1,
                BattleType.Boss => 2,
                _ => 0
            };
        }
    }
    public override void OnTurnStart(State state, Combat combat)
    {
        if (combat.turn == 1){
            combat.Queue(
                new AAddCard{
                    card = new CreditCard
                    {
                        BaseBlood = BaseBloods[Encounter]
                    },
                    destination = CardDestination.Hand,
                    artifactPulse = Key(),
                }
            );
        }
    }

    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
        if(card is CreditCard cc)
        {
            int depositing = Math.Min(state.ship.hullMax - 1 - ((state.ship.hullMax - 1) % BaseBloods[Encounter]), cc.GetBlood(state));
            if(depositing > 0)
            {
                Deposited += depositing;
                HullNotMaxDeposited += Math.Min(state.ship.hull - 1, depositing);
                state.ship.hull = Math.Max(1, state.ship.hull - depositing);
                state.ship.hullMax = Math.Max(1, state.ship.hullMax - depositing);
                state.ship.pendingMaxHullParticles += 20;
                Audio.Play(new GUID?(Event.Status_PowerDown), true);
                Pulse();
            }
        }
    }

    public override void OnCombatEnd(State state)
    {
        if(Deposited > 0)
        {
            state.ship.hullMax += Withdrawal();
            int healing = HullNotMaxDeposited;
            foreach (Artifact artifact in state.EnumerateAllArtifacts())
            {
                healing += artifact.ModifyHealAmount(HullNotMaxDeposited, state, true);
            }
            state.ship.Heal(healing);
            Deposited = HullNotMaxDeposited = 0;
            Pulse();
        }    
    }

    public override List<Tooltip> GetExtraTooltips()
    {
        return [
            new TTCard
            {
                card = new CreditCard(),
                showCardTraitTooltips = true
            }
        ];
    }
}