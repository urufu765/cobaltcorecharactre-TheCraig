using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A card that's obtained from Build-A-Cure or Find-A-Cure, reduces the corrosion stack of player when played
/// </summary>
public class TheSolution : Card, IRegisterable
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
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "TheSolution", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/TheCure.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.A => 
            [
                ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeResourceCost(
                        ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeStatusResource(ModEntry.Instance.TarnishStatus.Status),
                        1
                    ),
                    new AHeal
                    {
                        healAmount = 2,
                        targetPlayer = true
                    }
                ).AsCardAction
            ],
            _ => 
            [
                ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeResourceCost(
                        ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeStatusResource(ModEntry.Instance.TarnishStatus.Status),
                        1
                    ),
                    new AHeal
                    {
                        healAmount = 1,
                        targetPlayer = true,
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
                cost = 0,
                temporary = true,
                retain = true,
                recycle = true,
                artTint = "a43fff"

            },
            _ => new CardData
            {
                cost = 0,
                temporary = true,
                retain = true,
                artTint = "a43fff"
            }
        };
    }
}