using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A card that attempts to find a cure, creating The Cure and The Accident cards
/// </summary>
public class FindCure : Card, IRegisterable
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
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "FindACure", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/FindACure.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.B => 
            [
                new AAddCard
                {
                    card = new TheCure
                    {
                        upgrade = Upgrade.B
                    },
                    destination = CardDestination.Deck,
                    insertRandomly = true
                },
                new AAddCard
                {
                    card = new TheAccident
                    {
                        upgrade = Upgrade.B
                    },
                    destination = CardDestination.Deck,
                    insertRandomly = true
                },
            ],
            Upgrade.A =>
            [
                new AAddCard
                {
                    card = new TheCure(),
                    destination = CardDestination.Deck,
                    insertRandomly = true,
                    amount = 2
                },
                new AAddCard
                {
                    card = new TheAccident
                    {
                        upgrade = Upgrade.A
                    },
                    destination = CardDestination.Deck,
                    insertRandomly = true
                },
            ],
            _ => 
            [
                new AAddCard
                {
                    card = new TheCure(),
                    destination = CardDestination.Deck,
                    insertRandomly = true,
                },
                new AAddCard
                {
                    card = new TheAccident(),
                    destination = CardDestination.Deck,
                    insertRandomly = true
                },
            ],
        };
    }


    public override CardData GetData(State state)
    {
        return new CardData
        {
            cost = 0,
            exhaust = true,
            description = ModEntry.Instance.Localizations.Localize(["card", "Common", "FindACure", upgrade switch { Upgrade.A => "descA", Upgrade.B => "descB", _ => "desc" }]),
            artTint = "a43fff"
        };
    }
}