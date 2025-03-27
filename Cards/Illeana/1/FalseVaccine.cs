using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Give an enemy a lolipop... THEN proceed to murder them.
/// </summary>
public class FalseVaccine : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "FalseVaccine", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/FalseVaccine.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return upgrade switch
        {
            Upgrade.B => 
            [
                new AHeal
                {
                    healAmount = 1,
                    targetPlayer = false
                },
                new ASpawn
                {
                    thing = new Missile
                    {
                        yAnimation = 0.0,
                        missileType = MissileType.corrode
                    }
                    
                }
            ],
            _ => 
            [
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = -1,
                    targetPlayer = false
                },
                new ASpawn
                {
                    thing = new Missile
                    {
                        yAnimation = 0.0,
                        missileType = MissileType.corrode
                    }
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
                cost = 3
            },
            Upgrade.A => new CardData
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