using System.Collections.Generic;
using System.Reflection;
using Illeana.Features;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A secret Craig card
/// </summary>
public class Coalescent : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.DecrepitCraigDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B],
                dontOffer = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "Coalescent", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Dead/0/Coalescent.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        int x = upgrade switch
        {
            Upgrade.B => c.otherShip.hullMax,
            _ => s.ship.hullMax
        };
        return [
            new AStatus
            {
                status = ModEntry.Instance.TarnishStatus.Status,
                targetPlayer = false,
                statusAmount = x
            },
            new AStatus
            {
                status = ModEntry.Instance.TarnishStatus.Status,
                targetPlayer = true,
                statusAmount = x
            },
        ];
    }


    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            Upgrade.A => new CardData
            {
                cost = 3,
                exhaust = true,
                description = ModEntry.Instance.Localizations.Localize(["card", "Token", "Coalescent", "desc"])
            },
            Upgrade.B => new CardData
            {
                cost = 4,
                exhaust = true,
                description = ModEntry.Instance.Localizations.Localize(["card", "Token", "Coalescent", "descB"])
            },
            _ => new CardData
            {
                cost = 4,
                exhaust = true,
                description = ModEntry.Instance.Localizations.Localize(["card", "Token", "Coalescent", "desc"])
            },
        };
    }
}