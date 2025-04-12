using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Here, have a Patchkit.
/// </summary>
public class ScrapPatchkit : Card, IRegisterable
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
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "ScrapPatchkit", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/ScrapPatchkit.png").Sprite
        });
        altSprite = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/ScrapPatchkitAlt.png").Sprite;
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
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AHeal
                {
                    healAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.tempShield,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            _ => 
            [
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AHeal
                {
                    healAmount = 1,
                    targetPlayer = true
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
                retain = true,
                art = altSprite,
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                retain = true
            },
            _ => new CardData
            {
                cost = 1,
                retain = true
            }
        };
    }
}