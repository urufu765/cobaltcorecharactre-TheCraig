using System.Collections.Generic;
using System.Reflection;
using Illeana.Features;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Expose the hull to harsh environments
/// </summary>
public class Exposure : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "Exposure", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/Exposure.png").Sprite
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
                    targetPlayer = false,
                    statusAmount = 1
                },
                new AStatus
                {
                    status = ModEntry.Instance.TarnishStatus.Status,
                    targetPlayer = false,
                    statusAmount = 1
                }
            ],
            Upgrade.A => 
            [
                new AStatus
                {
                    status = ModEntry.Instance.TarnishStatus.Status,
                    targetPlayer = false,
                    statusAmount = 1
                },
            ],
            _ => 
            [
                new AStatus
                {
                    status = ModEntry.Instance.TarnishStatus.Status,
                    targetPlayer = true,
                    statusAmount = 1
                },
                new AStatus
                {
                    status = ModEntry.Instance.TarnishStatus.Status,
                    targetPlayer = false,
                    statusAmount = 1
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
                exhaust = true
            },
            Upgrade.A => new CardData
            {
                cost = 2
            },
            _ => new CardData
            {
                cost = 1
            }
        };
    }
}