using System;
using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Craig.Cards;

/// <summary>
/// A card that attempts to build a cure, creating The Cure and The Failure cards
/// </summary>
public class BuildCure : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "BuildACure", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/BuildACure.png").Sprite
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
                    card = new TheFailure
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
                    card = new TheFailure
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
                    amount = 2
                },
                new AAddCard
                {
                    card = new TheFailure(),
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
            cost = 1,
            exhaust = true,
            description = ModEntry.Instance.Localizations.Localize(["card", "Common", "BuildACure", upgrade switch { Upgrade.A => "descA", Upgrade.B => "descB", _ => "desc" }]),
        };
    }
}