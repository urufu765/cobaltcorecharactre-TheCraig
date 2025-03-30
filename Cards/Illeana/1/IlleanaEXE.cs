using System;
using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A card that attempts to build a cure, creating The Cure and The Failure cards
/// </summary>
public class IlleanaExe : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "IlleanaEXE", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/EXE.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        Random rng = new Random();
        int roll = rng.Next(1000);
        return upgrade switch
        {
            Upgrade.B => 
            [
                new ACardOffering
                {
                    amount = 3,
                    limitDeck = roll == 0? ModEntry.Instance.DecrepitCraigDeck.Deck : ModEntry.Instance.IlleanaDeck.Deck,
                    makeAllCardsTemporary = true,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -1,
                    dialogueSelector = ".summonIlleana"
                }
            ],
            _ => 
            [
                new ACardOffering
                {
                    amount = 2,
                    limitDeck = roll == 0? ModEntry.Instance.DecrepitCraigDeck.Deck : ModEntry.Instance.IlleanaDeck.Deck,
                    makeAllCardsTemporary = true,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -1,
                    dialogueSelector = ".summonIlleana"
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
                cost = 1,
                description = ColorlessLoc.GetDesc(state, 3, ModEntry.Instance.IlleanaDeck.Deck),
                artTint = "45e260"
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                description = ColorlessLoc.GetDesc(state, 2, ModEntry.Instance.IlleanaDeck.Deck),
                artTint = "45e260"
            },
            _ => new CardData
            {
                cost = 1,
                description = ColorlessLoc.GetDesc(state, 2, ModEntry.Instance.IlleanaDeck.Deck),
                artTint = "45e260"
            }
        };
    }
}