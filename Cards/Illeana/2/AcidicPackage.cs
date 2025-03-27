using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Shoot the corrode out of your ship!
/// </summary>
public class AcidicPackage : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Uncommon", "AcidicPackage", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/2/AcidicPackage.png").Sprite
        });
        altSprite = ModEntry.RegisterSprite(package, "assets/Card/Illeana/2/AcidicPackageAlt.png").Sprite;
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
                        status = Status.corrode,
                        statusAmount = 1,
                        targetPlayer = false
                    }
                ).AsCardAction
            ],
            _ => 
            [
                ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeCostAction(
                    ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeResourceCost(
                        ModEntry.Instance.KokoroApi.V2.ActionCosts.MakeStatusResource(Status.corrode),
                        1
                    ),
                    new ASpawn
                    {
                        thing = new Missile
                        {
                            yAnimation = 0.0,
                            missileType = MissileType.corrode
                        }
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
                cost = 1,
                exhaust = true,
                art = altSprite
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                exhaust = true
            },
            _ => new CardData
            {
                cost = 1,
                exhaust = true
            }
        };
    }
}