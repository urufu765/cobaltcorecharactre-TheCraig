using System;
using System.Collections.Generic;
using System.Reflection;
using Illeana.Actions;
using Illeana.Features;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A card that's obtained from Build-A-Cure, Applies corrode upon draw, removes upon play
/// </summary>
public class CreditCard : YellowCardTrash, IRegisterable, IHasCustomCardTraits
{
    //public int Interest {get; set;} = 0;
    public int BaseBlood {get; set;} = 0;
    //public int FlipBlood {get; set;} = 0;
    public const int DefaultBlood = 4;

    private static Spr frontSprite;
    private static Spr backSprite;
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = Deck.trash,
                rarity = Rarity.common,
                dontOffer = true,
                unreleased = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "BloodCard", "name"]).Localize,
            Art = StableSpr.cards_colorless,
        });
        frontSprite = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/CreditCardFront.png").Sprite;
        backSprite = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/CreditCardBack.png").Sprite;
    }

    // public override void OnDraw(State s, Combat c)
    // {
    //     if(FlipBlood == 0 && MaxBlood(s) > 0) FlipBlood = BaseBlood;
    // }


    // public override void OnFlip(G g)
    // {
    //     FlipBlood+=BaseBlood;
    // }

    private int MaxBlood(State state)
    {
        if (BaseBlood == 0)
        {
            return 0;
        }
        if (state.ship.hullMax < BaseBlood)
        {
            return 0;
        }
        return state.ship.hullMax - 1 - ((state.ship.hullMax - 1) % BaseBlood);
    }

    public int GetBlood(State state)
    {
        if (MaxBlood(state) < DefaultBlood && MaxBlood(state) >= BaseBlood && BaseBlood != 0)
        {
            return MaxBlood(state) - (MaxBlood(state) % BaseBlood);
        }
        if(MaxBlood(state) == 0)
        {
            return 0;
        }
        return DefaultBlood;
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return [
            new ADoesLiterallyNothingButItsForACreditCard()
        ];
    }

    public override CardData GetData(State state)
    {
        return new CardData
        {
            cost = 0,
            flippable = true,
            infinite = true,
            temporary = true,
            description = BaseBlood != 0? 
                string.Format(
                    ModEntry.Instance.Localizations.Localize(["card", "Token", "BloodCard", flipped? "descFlip" : "desc"]), 
                    GetBlood(state),
                    BaseBlood,
                    1) : 
                ModEntry.Instance.Localizations.Localize(["card", "Token", "BloodCard", "blank"]),
            artTint = "ffffff",
            artOverlay = this.flipped? backSprite : frontSprite,
        };
    }

    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state)
    {
        return new HashSet<ICardTraitEntry>{ModEntry.Instance.KokoroApi.V2.Fleeting.Trait};
    }
}