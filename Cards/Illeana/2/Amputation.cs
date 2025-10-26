using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Break off parts to save the rest.
/// </summary>
public class Amputation : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Uncommon", "Amputation", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/2/Amputation.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        int x = s.ship.hullMax - s.ship.hull;
        return upgrade switch
        {
            Upgrade.B => 
            [
                new AHeal
                {
                    healAmount = x,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.ace,
                    statusAmount = x/2,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = -x,
                    targetPlayer = true
                },
                new AHullMax
                {
                    amount = -3,
                    targetPlayer = true
                }
            ],
            Upgrade.A => 
            [
                new AHeal
                {
                    healAmount = x,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = -x,
                    targetPlayer = true
                },
                new AHullMax
                {
                    amount = -1,
                    targetPlayer = true
                }
            ],
            _ => 
            [
                new AHeal
                {
                    healAmount = x,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = -x,
                    targetPlayer = true
                },
                new AHullMax
                {
                    amount = -2,
                    targetPlayer = true
                }
            ],
        };
    }


    public override CardData GetData(State state)
    {
        int x = 0;
        try
        {
            x = state.ship.hullMax - state.ship.hull;
        }
        catch (Exception e)
        {
            ModEntry.Instance.Logger.LogError(e, $"Error getting hull");
        }
        return upgrade switch
        {
            Upgrade.A => new CardData
            {
                cost = 0,
                exhaust = true,
                retain = true,
                description = ModEntry.Instance.Localizations.Localize(["card", "Uncommon", "Amputation", "descA"])
            },
            Upgrade.B => new CardData
            {
                cost = 0,
                exhaust = true,
                retain = true,
                description = string.Format(ModEntry.Instance.Localizations.Localize(["card", "Uncommon", "Amputation", "descB"]), (x/2).ToString())
            },
            _ => new CardData
            {
                cost = 0,
                exhaust = true,
                retain = true,
                description = ModEntry.Instance.Localizations.Localize(["card", "Uncommon", "Amputation", "desc"])
            },
        };
    }
}