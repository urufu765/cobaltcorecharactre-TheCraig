using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Craig.Cards;

/// <summary>
/// Nothing works as best as homemade spaceship hulls... or was it the other way around?
/// </summary>
public class MakeshiftHull : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.IlleanaDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Uncommon", "MakeshiftHull", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/2/MakeshiftHull.png").Sprite
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
                    status = Status.corrode,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AHullMax
                {
                    amount = 1,
                    targetPlayer = true
                },
                new AEndTurn()
            ],
            Upgrade.A => 
            [
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AHullMax
                {
                    amount = 3,
                    targetPlayer = true
                }
            ],
            _ => 
            [
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AHullMax
                {
                    amount = 1,
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
                cost = 1
            },
            Upgrade.A => new CardData
            {
                cost = 1,
                singleUse = true
            },
            _ => new CardData
            {
                cost = 2,
                exhaust = true
            },
        };
    }
}