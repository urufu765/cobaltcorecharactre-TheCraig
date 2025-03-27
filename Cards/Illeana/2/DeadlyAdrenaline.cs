using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Get ready.
/// </summary>
public class DeadlyAdrenaline : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Uncommon", "DeadlyAdrenaline", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/2/DeadlyAdrenaline.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.B => 
            [
                new AHullMax
                {
                    amount = -1,
                    targetPlayer = true
                },
                new AHurt
                {
                    hurtAmount = 1,
                    hurtShieldsFirst = true,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.ace,
                    statusAmount = 1,
                    targetPlayer = true
                },            
            ],
            Upgrade.A => 
            [
                new AHurt
                {
                    hurtAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.ace,
                    statusAmount = 1,
                    targetPlayer = true
                },            
            ],
            _ => 
            [
                new AHullMax
                {
                    amount = -1,
                    targetPlayer = true
                },
                new AHurt
                {
                    hurtAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.ace,
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
            Upgrade.B => new CardData
            {
                cost = 2,
                buoyant = true
            },
            _ => new CardData
            {
                cost = 2
            },
        };
    }
}