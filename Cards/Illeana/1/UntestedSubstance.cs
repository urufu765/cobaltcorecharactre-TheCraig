using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Craig.Cards;

/// <summary>
/// Gives corrode and overdrive/powerdrive. Use wisely
/// </summary>
public class UntestedSubstance : Card, IRegisterable
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
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 3,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.powerdrive,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.evade,
                    statusAmount = 1,
                }
            ],
            _ => 
            [
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.corrode,
                    statusAmount = 1,
                },
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.overdrive,
                    statusAmount = 1,
                },
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.evade,
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
                cost = 3,
            },
            Upgrade.A => new CardData
            {
                cost = 0,
            },
            _ => new CardData
            {
                cost = 1,
            }
        };
    }
}