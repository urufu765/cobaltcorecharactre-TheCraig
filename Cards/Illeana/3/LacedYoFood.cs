using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// What a bitch.
/// </summary>
public class LacedYoFood : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.IlleanaDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Rare", "LacedYoFood", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/3/LacedYoFood.png").Sprite
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
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 1,
                    targetPlayer = false,
                    omitFromTooltips = true
                },
                new AStatus
                {
                    status = Status.lockdown,
                    statusAmount = 1,
                    targetPlayer = false
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
                new AStatus
                {
                    status = ModEntry.Instance.KokoroApi.V2.OxidationStatus.Status,
                    statusAmount = 2,
                    targetPlayer = false
                },
                new AStatus
                {
                    status = Status.lockdown,
                    statusAmount = 1,
                    targetPlayer = false
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
                cost = 1,
            },
            _ => new CardData
            {
                cost = 2,
            },
        };
    }
}