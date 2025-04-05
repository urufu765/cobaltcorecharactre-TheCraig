using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Illeana went to go get a snack, and she was back in a jiffy.
/// </summary>
public class GoneJiffy : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Uncommon", "GoneJiffy", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/2/GoneInAJiffy.png").Sprite
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
                    status = Status.autododgeLeft,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AHeal
                {
                    healAmount = 2,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = ModEntry.IlleanaTheSnek.MissingStatus.Status,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AEndTurn()
            ],
            Upgrade.A => 
            [
                new AStatus
                {
                    status = Status.autododgeRight,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AHeal
                {
                    healAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = ModEntry.IlleanaTheSnek.MissingStatus.Status,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AEndTurn()
            ],
            _ => 
            [
                new AStatus
                {
                    status = Status.autododgeRight,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AHeal
                {
                    healAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = ModEntry.IlleanaTheSnek.MissingStatus.Status,
                    statusAmount = 2,
                    targetPlayer = true
                },
                new AEndTurn()
            ],
        };
    }


    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.B => new CardData
            {
                cost = 1,
                exhaust = true,
            },
            _ => new CardData
            {
                cost = 1,
            }
        };
    }
}