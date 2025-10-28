using System.Collections.Generic;
using Illeana.Cards;
using Nickel;

namespace Illeana.Actions;

public class ABakeACake : CardAction
{
    public required int uuid;
    public required int amount;  // Max hull amount to give as reward
    public bool constantReward;  // Keep rewarding even after
    public bool destroyOnCompletion;  // Destroy card as soon as goal is met
    public bool healOnCompletion;  // Give heal as soon as goal is met
    public bool healAfterCompletion;  // Start healing after goal is met (and not on)
    public bool refundAfterCompletion;  // Start refunding after goal is met (and not on)
    public int refundAmount = 0;  // Refund amount
    public bool drawAfterCompletion;  // Start drawing after goal is met (and not on)
    public bool drawOnCompletion;  // Draw cards as reward for goal met
    public int drawAmount = 0;  // Draw amount
    
    public override void Begin(G g, State s, Combat c)
    {
        Card? card = s.FindCard(uuid);
        if (card is BakedHull bh)
        {
            bool incremented = false;

            // Increments if below limit
            if (bh.Uses < bh.Limit)
            {
                bh.Uses++;
                incremented = true;
            }

            if (bh.Uses == bh.Limit)
            {
                // Rewards when reaching limit or keep rewarding when at limit
                if (incremented || constantReward)
                {
                    c.Queue(new AHullMax
                    {
                        amount = amount,
                        targetPlayer = true
                    });
                    if (healOnCompletion)
                    {
                        c.Queue(new AHeal
                        {
                            healAmount = amount,
                            targetPlayer = true
                        });
                    }
                    if (destroyOnCompletion)
                    {
                        c.Queue(new ADestroyCard
                        {
                            uuid = uuid
                        });
                    }
                    if (drawOnCompletion)
                    {
                        c.Queue(new ADrawCard
                        {
                            count = drawAmount
                        });
                    }
                }
                else
                {
                    // Actions that occur after being rewarded
                    if (healAfterCompletion)
                    {
                        c.Queue(new AHeal
                        {
                            healAmount = amount,
                            targetPlayer = true
                        });
                    }
                    if (refundAfterCompletion)
                    {
                        c.Queue(new AEnergy
                        {
                            changeAmount = refundAmount,
                        });
                    }
                    if (drawAfterCompletion)
                    {
                        c.Queue(new ADrawCard
                        {
                            count = drawAmount
                        });
                    }
                }
            }
        }
    }
}
