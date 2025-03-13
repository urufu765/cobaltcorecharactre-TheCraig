using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Craig.Cards;

/// <summary>
/// Stop mixing acid in my diesel!
/// </summary>
public class IncompatibleFuel : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.IlleanaDeck.Deck,
                rarity = Rarity.common,
                //upgradesTo = [Upgrade.A, Upgrade.B]  Once Corrode-cost is implemented, add this
                upgradesTo = [Upgrade.A]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "IncompatibleFuel", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/IncompatibleFuel.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.B => 
            [
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 2,
                    targetPlayer = true
                },
                // Cost thing here
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            _ => 
            [
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
        };
    }


    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.B => new CardData
            {
                cost = 2
            },
            Upgrade.A => new CardData
            {
                cost = 0
            },
            _ => new CardData
            {
                cost = 1
            }
        };
    }
}