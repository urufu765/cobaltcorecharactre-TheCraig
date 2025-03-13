using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Craig.Cards;

/// <summary>
/// A card that's obtained from Build-A-Cure or Find-A-Cure, reduces the corrosion stack of player when played
/// </summary>
public class TheCure : Card, IRegisterable
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
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "TheCure", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/TheCure.png").Sprite
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
                    statusAmount = -1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.overdrive,
                    statusAmount = 1,
                    targetPlayer = true
                }
            ],
            Upgrade.A or Upgrade.None or _ => 
            [
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.corrode,
                    statusAmount = -1,
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
                temporary = true,
                retain = true,
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                temporary = true,
                retain = true,
                recycle = true,
            },
            _ => new CardData
            {
                cost = 0,
                temporary = true,
                retain = true,
            }
        };
    }
}