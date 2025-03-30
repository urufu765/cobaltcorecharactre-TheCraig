using System.Collections.Generic;
using System.Reflection;
using Illeana.Features;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A secret Craig card
/// </summary>
public class Reminiscent : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.DecrepitCraigDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B],
                dontOffer = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "Reminiscent", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Dead/0/Reminiscent.png").Sprite
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
                    status = ModEntry.Instance.TarnishStatus.Status,
                    targetPlayer = false,
                    statusAmount = 4
                }
            ],
            _ => 
            [
                new AStatus
                {
                    status = ModEntry.Instance.TarnishStatus.Status,
                    targetPlayer = false,
                    statusAmount = 2
                },
                new AAttack
                {
                    damage = 1,
                    piercing = true
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
                cost = 2,
            },
            _ => new CardData
            {
                cost = 3,
            }
        };
    }
}