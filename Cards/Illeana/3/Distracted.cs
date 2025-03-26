using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Illeana got distracted again
/// </summary>
public class Distracted : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Rare", "Distracted", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/3/Distracted.png").Sprite
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
                    statusAmount = 3,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = ModEntry.IlleanaTheSnek.MissingStatus.Status,
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
                    status = ModEntry.IlleanaTheSnek.MissingStatus.Status,
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
            Upgrade.A => new CardData
            {
                cost = 0,
            },
            _ => new CardData
            {
                cost = 1,
            },
        };
    }
}