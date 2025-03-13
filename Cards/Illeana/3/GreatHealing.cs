using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Craig.Cards;

/// <summary>
/// I need healing
/// </summary>
public class GreatHealing : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.IlleanaDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Rare", "GreatHealing", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/3/GreatHealing.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        int x = s.ship.Get(Status.corrode);
        return upgrade switch
        {
            Upgrade.B => 
            [
                new AVariableHint
                {
                    status = new Status?(Status.corrode)
                },
                new AHeal
                {
                    healAmount = x * 5,
                    targetPlayer = true,
                    xHint = new int?(5)
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            _ => 
            [
                new AVariableHint
                {
                    status = new Status?(Status.corrode)
                },
                new AHeal
                {
                    healAmount = x * 3,
                    targetPlayer = true,
                    xHint = new int?(3)
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 2,
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
                cost = 2,
                singleUse = true
            },
            Upgrade.A => new CardData
            {
                cost = 1,
                exhaust = true
            },
            _ => new CardData
            {
                cost = 2,
                exhaust = true
            },
        };
    }
}