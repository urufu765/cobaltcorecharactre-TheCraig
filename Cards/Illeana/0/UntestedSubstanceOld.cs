using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Gives corrode and evade
/// </summary>
public class UntestedSubstanceOld : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "UntestedSubstance", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/UntestedSubstance.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.B => 
            [
                ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeResourceCost(
                        ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeStatusResource(ModEntry.Instance.TarnishStatus.Status),
                        2
                    ),
                    new AStatus
                    {
                        status = Status.evade,
                        statusAmount = 5,
                        targetPlayer = true
                    }
                ).AsCardAction,
                new AStatus
                {
                    targetPlayer = true,
                    status = ModEntry.Instance.TarnishStatus.Status,
                    statusAmount = 4,
                }
            ],
            _ => 
            [
                ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeResourceCost(
                        ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeStatusResource(ModEntry.Instance.TarnishStatus.Status),
                        1
                    ),
                    new AStatus
                    {
                        status = Status.evade,
                        statusAmount = 2,
                        targetPlayer = true
                    }
                ).AsCardAction,
                new AStatus
                {
                    targetPlayer = true,
                    status = ModEntry.Instance.TarnishStatus.Status,
                    statusAmount = 1,
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
                cost = 1,
                exhaust = true,
                artTint = "a43fff"
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                artTint = "a43fff"
            },
            _ => new CardData
            {
                cost = 1,
                artTint = "a43fff"
            }
        };
    }
}