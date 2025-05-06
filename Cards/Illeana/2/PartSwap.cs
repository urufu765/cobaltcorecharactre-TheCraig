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
    private static Spr altSprite;

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
        altSprite = ModEntry.RegisterSprite(package, "assets/Card/Illeana/2/PartSwapAlt.png").Sprite;
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.B => 
            [
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
                ).AsCardAction,
                ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeResourceCost(
                        ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeStatusResource(Status.corrode),
                        1
                    ),
                    new AStatus
                    {
                        status = Status.ace,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                ).AsCardAction,
                ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeResourceCost(
                        ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeStatusResource(ModEntry.Instance.TarnishStatus.Status),
                        2
                    ),
                    new AStatus
                    {
                        status = Status.perfectShield,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                ).AsCardAction,
            ],
            _ => 
            [
                ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeResourceCost(
                        ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeStatusResource(Status.corrode),
                        2
                    ),
                    new AStatus
                    {
                        status = Status.ace,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                ).AsCardAction,
                ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeResourceCost(
                        ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeStatusResource(ModEntry.Instance.TarnishStatus.Status),
                        2
                    ),
                    new AStatus
                    {
                        status = Status.perfectShield,
                        statusAmount = 1,
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
            Upgrade.B => new CardData
            {
                cost = 2,
                artTint = "f5e030",
                art = altSprite,
                retain = true
            },
            Upgrade.A => new CardData
            {
                cost = 1,
                artTint = "f5e030"
            },
            _ => new CardData
            {
                cost = 2,
                artTint = "f5e030"
            },
        };
    }
}