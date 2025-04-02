using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Wait, Illeana isn't biologically capable of-
/// </summary>
public class Autotomy : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.IlleanaDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "Autotomy", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/Autotomy.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.A => 
            [
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 0,
                    mode = AStatusMode.Set,
                    targetPlayer = true,
                    dialogueSelector = ".autotomySnek"
                },
                new AHurt
                {
                    hurtAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 5,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.autododgeRight,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            Upgrade.B => 
            [
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 0,
                    mode = AStatusMode.Set,
                    targetPlayer = true,
                    dialogueSelector = ".autotomySnek"
                },
                new AHurt
                {
                    hurtAmount = 2,
                    hurtShieldsFirst = true,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 5,
                    targetPlayer = true
                }
            ],
            _ => 
            [                
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 0,
                    mode = AStatusMode.Set,
                    targetPlayer = true,
                    dialogueSelector = ".autotomySnek"
                },
                new AHurt
                {
                    hurtAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 5,
                    targetPlayer = true
                }
            ],
        };
    }


    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            _ => new CardData
            {
                cost = 2,
                exhaust = true,
            }
        };
    }
}