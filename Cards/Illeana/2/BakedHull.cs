using System;
using System.Collections.Generic;
using System.Reflection;
using daisyowl.text;
using Illeana.Actions;
using Illeana.External;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using CRHook = Illeana.External.IKokoroApi.IV2.ICardRenderingApi.IHook;

namespace Illeana.Cards;

/// <summary>
/// Gain max hull at the cost of shield (temporarily)
/// </summary>
public class BakedHull : Card, IRegisterable, IHasCustomCardTraits
{
    public int Uses {get;set;} = 0;
    public int Limit
    {
        get
        {
            return upgrade switch
            {
                Upgrade.B => 10,
                Upgrade.A => 5,
                _ => 3
            };
        }
    }
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Uncommon", "BakedHull", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/2/AcidBackflow.png").Sprite
        });

        ModEntry.Instance.KokoroApi.V2.CardRendering.RegisterHook(new Hook());
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return [
            new ABakeACake
            {
                uuid = uuid,
                amount = upgrade == Upgrade.B? s.ship.hullMax : 1,
                constantReward = upgrade == Upgrade.A,
                destroyOnCompletion = upgrade == Upgrade.B,
                healToo = upgrade == Upgrade.None
            }
        ];
    }


    public override CardData GetData(State state)
    {
        return new CardData
        {
            cost = 2,
            exhaust = true,
            description = ModEntry.Instance.Localizations.Localize(
                [
                    "card", 
                    "Uncommon", 
                    "BakedHull", 
                    upgrade switch
                    {
                        Upgrade.B => "descBA",
                        Upgrade.A => "descA",
                        _ => "desc"
                    }
                ], tokens: new
                {
                    color = Uses==Limit?"faint":"textMain",
                    uses = Uses,
                    limit = Limit
                }
            )
        };
    }

    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state)
    {
        return new HashSet<ICardTraitEntry>{ModEntry.Instance.KokoroApi.V2.Fleeting.Trait};
    }

    private sealed class Hook : CRHook
    {
        public Font? ReplaceTextCardFont(CRHook.IReplaceTextCardFontArgs args)
        {
            if (args.Card is BakedHullOld)
            {
                return ModEntry.Instance.KokoroApi.V2.Assets.PinchCompactFont;
            }
            return null;
        }
    }
}