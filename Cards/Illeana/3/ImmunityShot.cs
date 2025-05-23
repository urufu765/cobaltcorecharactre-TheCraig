using System;
using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Get your vaccine here!
/// </summary>
public class ImmunityShot : Card, IRegisterable
{
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Rare", "ImmunityShot", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/3/ImmunityShot.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        int x = s.ship.Get(Status.corrode);
        return upgrade switch
        {
            Upgrade.B => 
            [
                new AVariableHint
                {
                    status = Status.corrode
                },
                new AStatus
                {
                    status = Status.perfectShield,
                    statusAmount = x * 3,
                    mode = AStatusMode.Set,
                    targetPlayer = true,
                    xHint = new int?(3)
                }
            ],
            Upgrade.A => 
            [
                new AVariableHint
                {
                    status = Status.corrode
                },
                new AStatus
                {
                    status = Status.perfectShield,
                    statusAmount = x * 2,
                    mode = AStatusMode.Set,
                    targetPlayer = true,
                    xHint = new int?(2)
                },
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = x,
                    targetPlayer = true,
                    xHint = new int?(1)
                }
            ],
            _ => 
            [
                new AVariableHint
                {
                    status = Status.corrode
                },
                new AStatus
                {
                    status = Status.perfectShield,
                    statusAmount = x * 2,
                    mode = AStatusMode.Set,
                    targetPlayer = true,
                    xHint = new int?(2)
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
                cost = 3,
                exhaust = true,
                artTint = "f5e030"
            },
            _ => new CardData
            {
                cost = 2,
                exhaust = true,
                artTint = "f5e030"
            },
        };
    }
}