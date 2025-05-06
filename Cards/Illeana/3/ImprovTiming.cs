using System;
using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Nothing works as best as homemade spaceship hulls... or was it the other way around?
/// </summary>
public class ImprovisedTiming : Card, IRegisterable
{
    public int Drawn{get; set;}
    public static List<int> Costs {get;} = [3, 6, 0];
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Rare", "ImprovisedTiming", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/3/ImprovisedHull.png").Sprite
        });
    }


    private int GetImprovCost()
    {
        return upgrade switch
        {
            Upgrade.B => Costs[2] + Drawn,
            Upgrade.A => Math.Max(0, Costs[1] - Drawn),
            _ => Math.Max(0, Costs[0] - Drawn)
        };
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> Actionz = [];
        if(GetImprovCost() > 0)
        {
            Actionz.Add(new AHullMax
            {
                amount = GetImprovCost(),
                targetPlayer = true
            });
        }
        switch (upgrade)
        {
            case Upgrade.B:
                if(GetImprovCost() > 0)
                {
                    Actionz.Add(new AStatus
                    {
                        status = ModEntry.Instance.TarnishStatus.Status,
                        statusAmount = GetImprovCost(),
                        targetPlayer = true
                    });
                }
                break;
            case Upgrade.A:
                Actionz.Add(new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 1,
                    targetPlayer = true
                });
                break;
            default:
                if(GetImprovCost() > 0)
                {
                    Actionz.Add(new AStatus
                    {
                        status = Status.corrode,
                        statusAmount = GetImprovCost(),
                        targetPlayer = true
                    });
                }
                break;
        }
        return Actionz;
    }


    public override void OnDraw(State s, Combat c)
    {
        Drawn++;
    }

    public override void OnExitCombat(State s, Combat c)
    {
        Drawn = 0;
    }

    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.A => new CardData
            {
                cost = GetImprovCost(),
                exhaust = true,
                description = ModEntry.Instance.Localizations.Localize(["card", "Rare", "ImprovisedTiming", "descA"])
            },
            Upgrade.B => new CardData
            {
                cost = GetImprovCost(),
                exhaust = true,
                artTint = "a43fff",
                description = ModEntry.Instance.Localizations.Localize(["card", "Rare", "ImprovisedTiming", "descB"])
            },
            _ => new CardData
            {
                cost = GetImprovCost(),
                exhaust = true,
                description = ModEntry.Instance.Localizations.Localize(["card", "Rare", "ImprovisedTiming", "desc"])
            },
        };
    }
}