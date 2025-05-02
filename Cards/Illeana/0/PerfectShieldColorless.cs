using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A card that's obtained from Build-A-Cure or Find-A-Cure, reduces the corrosion stack of player when played
/// </summary>
public class PerfectShieldColourless : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = Deck.colorless,
                rarity = Rarity.common,
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "PerfectShield", "name"]).Localize,
            Art = StableSpr.cards_Shield,
            
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
                    status = Status.perfectShield,
                    statusAmount = 2,
                    targetPlayer = true
                }
            ],
            _ => 
            [
                new AStatus
                {
                    status = Status.perfectShield,
                    statusAmount = 1,
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
                cost = 3,
                artTint = "f5e030",
                exhaust = true,
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                artTint = "f5e030",
                exhaust = true,
            },
            _ => new CardData
            {
                cost = 1,
                artTint = "f5e030",
                exhaust = true,
            }
        };
    }
}