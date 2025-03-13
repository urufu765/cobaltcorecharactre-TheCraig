using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Craig.Cards;

/// <summary>
/// Here, have a Patchkit.
/// </summary>
public class ScrapPatchkit : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "ScrapPatchkit", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/ScrapPatchkit.png").Sprite
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
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AHeal
                {
                    healAmount = 4,
                    targetPlayer = true
                },
                new AEndTurn()
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
                    healAmount = 2,
                    targetPlayer = true
                }
            ],
        };
    }


    public override CardData GetData(State state)
    {
        return upgrade switch
        {
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