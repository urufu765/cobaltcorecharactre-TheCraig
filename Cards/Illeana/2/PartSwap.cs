using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Swap destroyed parts for new ones.
/// </summary>
public class PartSwap : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Uncommon", "PartSwap", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/2/PartSwap.png").Sprite
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
                    status = new Status?(Status.corrode)
                },
                new AStatus
                {
                    status = Status.autododgeRight,
                    statusAmount = x * 2,
                    targetPlayer = true,
                    xHint = new int?(2)
                },
                new AHurt
                {
                    hurtAmount = x,
                    targetPlayer = true,
                    xHint = new int?(1)
                },
                new AStatus
                {
                    status = Status.perfectShield,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AEndTurn()
            ],
            Upgrade.A => 
            [
                new AVariableHint
                {
                    status = new Status?(Status.corrode)
                },
                new AHurt
                {
                    hurtAmount = x,
                    targetPlayer = true,
                    hurtShieldsFirst = true,
                    xHint = new int?(1)
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = -2,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.perfectShield,
                    statusAmount = 1,
                    targetPlayer = true
                },
            ],
            _ => 
            [
                new AVariableHint
                {
                    status = new Status?(Status.corrode)
                },
                new AHurt
                {
                    hurtAmount = x,
                    targetPlayer = true,
                    xHint = new int?(1)
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = -3,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.perfectShield,
                    statusAmount = 1,
                    targetPlayer = true
                },
            ],
        };
    }


    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.A => new CardData
            {
                cost = 2
            },
            _ => new CardData
            {
                cost = 1
            },
        };
    }
}