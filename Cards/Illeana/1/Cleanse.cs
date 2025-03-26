using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Clear corrode, turn it into lubricant
/// </summary>
public class Cleanse : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "Cleanse", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/Cleanse.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.B => 
            [
                new AStatus
                {
                    status = Status.corrode,
                    targetPlayer = true,
                    statusAmount = -2
                },
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.tempShield,
                    targetPlayer = true,
                    statusAmount = 1
                }
            ],
            _ => 
            [
                new AStatus
                {
                    status = Status.evade,
                    statusAmount = 1,
                    targetPlayer = true
                },
                ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeResourceCost(
                        ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeStatusResource(Status.corrode),
                        1
                    ),
                    new AStatus
                    {
                        status = Status.tempShield,
                        statusAmount = 3,
                        targetPlayer = true
                    }
                ).AsCardAction
            ],
        };
    }


    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.A or Upgrade.B => new CardData
            {
                cost = 0
            },
            _ => new CardData
            {
                cost = 1
            }
        };
    }
}