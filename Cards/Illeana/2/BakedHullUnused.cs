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
public class BakedHullOld : Card, IRegisterable, IHasCustomCardTraits
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
                upgradesTo = [Upgrade.A, Upgrade.B],
                unreleased = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Uncommon", "BakedHull", "name"]).Localize,
            Art = StableSpr.cards_ExtraBattery
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        int x = s.ship.GetMaxShield();
        bool hasEnough = s.ship.shieldMaxBase > 1;
        return upgrade switch
        {
            Upgrade.B =>
            [
                new AExtraConditionMaxShield
                {
                    minimum = 2
                },
                new AHullMax
                {
                    amount = x * 2,
                    targetPlayer = true,
                    disabled = !hasEnough
                },
                new AShieldMax
                {
                    amount = -2,
                    targetPlayer = true,
                    disabled = !hasEnough
                }
            ],
            Upgrade.A =>
            [
                new AExtraConditionMaxTotalShield
                {
                    minimum = 2
                },
                new AHullMax
                {
                    amount = 1,
                    targetPlayer = true,
                    disabled = x < 2
                },
                new AStatus
                {
                    status = Status.maxShield,
                    statusAmount = -2,
                    targetPlayer = true,
                    disabled = x < 2
                }
            ],
            _ =>
            [
                new AExtraConditionMaxShield
                {
                    minimum = 1
                },
                new AHullMax
                {
                    amount = 3,
                    targetPlayer = true,
                    disabled = !hasEnough
                },
                new AShieldMax
                {
                    amount = -1,
                    targetPlayer = true,
                    disabled = !hasEnough
                },
            ],
        };
    }


    public override CardData GetData(State state)
    {
        int x = 0;
        int y = 0;
        try
        {
            x = state.ship.GetMaxShield() * 2;
            y = state.ship.shieldMaxBase;
        }
        catch (Exception e)
        {
            ModEntry.Instance.Logger.LogError(e, $"Error getting shield");
        }
        return upgrade switch
        {
            Upgrade.B => new CardData
            {
                cost = 1,
                exhaust = true,
                description = y>1?ModEntry.Instance.Localizations.Localize(["card", "Uncommon", "BakedHull", "descB"], tokens: new
                {
                    amount = x
                }):ModEntry.Instance.Localizations.Localize(["card", "Uncommon", "BakedHull", "descB2"])
            },
            Upgrade.A => new CardData
            {
                cost = 1,
                exhaust = true,
            },
            _ => new CardData
            {
                cost = 1,
                singleUse = true
            },
        };
    }

    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state)
    {
        return upgrade == Upgrade.A ? new HashSet<ICardTraitEntry>{ModEntry.Instance.KokoroApi.V2.Fleeting.Trait} : [];
        //return new HashSet<ICardTraitEntry>();
    }
}