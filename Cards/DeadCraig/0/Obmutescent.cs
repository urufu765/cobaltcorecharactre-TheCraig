using System.Collections.Generic;
using System.Reflection;
using Illeana.Features;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A secret Craig card
/// </summary>
public class Obmutescent : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.DecrepitCraigDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B],
                dontOffer = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "Obmutescent", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Dead/0/Obmutescent.png").Sprite
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
                    status = Status.autododgeLeft,
                    targetPlayer = false,
                    statusAmount = 1
                },
                new AStatus
                {
                    status = Status.autododgeRight,
                    targetPlayer = true,
                    statusAmount = 1,
                    dialogueSelector = ".obmutesceCraig"
                },
            ],
            _ => 
            [
                new AStatus
                {
                    status = Status.autododgeRight,
                    targetPlayer = false,
                    statusAmount = 1
                },
                new AStatus
                {
                    status = Status.autododgeLeft,
                    targetPlayer = true,
                    statusAmount = 1,
                    dialogueSelector = ".obmutesceCraig"
                },
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
            },
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