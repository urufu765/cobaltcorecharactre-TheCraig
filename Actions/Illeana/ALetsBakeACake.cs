using System.Collections.Generic;
using Illeana.Cards;
using Nickel;

namespace Illeana.Actions;

public class ABakeACake : CardAction
{
    public required int uuid;
    public required int amount;
    public bool constantReward;
    public bool destroyOnCompletion;
    public bool healToo;
    
    public override void Begin(G g, State s, Combat c)
    {
        Card? card = s.FindCard(uuid);
        if (card is BakedHull bh)
        {
            bool incremented = false;
            if (bh.Uses < bh.Limit)
            {
                bh.Uses++;
                incremented = true;
            }
            if (bh.Uses == bh.Limit && (incremented || constantReward))
            {
                c.Queue(new AHullMax
                {
                    amount = amount,
                    targetPlayer = true
                });
                if (healToo)
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
            }
        }
    }
}
