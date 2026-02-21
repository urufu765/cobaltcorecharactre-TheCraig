using System;
using System.Collections.Generic;
using System.Reflection;
using daisyowl.text;
using Illeana.Actions;
using Illeana.External;
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
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Uncommon", "IlleanaEXE", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/EXE.png").Sprite
        });
        ModEntry.Instance.KokoroApi.V2.CardRendering.RegisterHook(new Hook());
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
                    amount = 5,
                    limitDeck = roll == 0? ModEntry.Instance.DecrepitCraigDeck.Deck : ModEntry.Instance.IlleanaDeck.Deck,
                    makeAllCardsTemporary = true,
                    overrideUpgradeChances = false,
                    canSkip = false,
                    inCombat = true,
                    discount = -1,
                    dialogueSelector = ".summonIlleana"
                },
                new ASelectAPotentialFix()
            ],
            _ => 
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
                },
                new ASelectAPotentialFix()
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
                description = ModEntry.Instance.Localizations.Localize(["card", "Uncommon", "IlleanaEXE", "desc"], new{
                    num = 5
                }),
                artTint = "45e260"
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                exhaust = true,
                description = ModEntry.Instance.Localizations.Localize(["card", "Uncommon", "IlleanaEXE", "desc"], new{
                    num = 3
                }),
                artTint = "45e260"
            },
            _ => new CardData
            {
                cost = 1,
                exhaust = true,
                description = ModEntry.Instance.Localizations.Localize(["card", "Uncommon", "IlleanaEXE", "desc"], new{
                    num = 3
                }),
                artTint = "45e260"
            }
        };
    }
    private sealed class Hook : IKokoroApi.IV2.ICardRenderingApi.IHook
    {
        public Font? ReplaceTextCardFont(IKokoroApi.IV2.ICardRenderingApi.IHook.IReplaceTextCardFontArgs args)
        {
            if (args.Card is IlleanaExe)
            {
                return ModEntry.Instance.KokoroApi.V2.Assets.PinchCompactFont;
            }
            return null;
        }
    }

}
