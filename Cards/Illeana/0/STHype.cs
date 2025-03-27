using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A card that's obtained from the artifact "Illeana's Personal Stereo", gives overdrive
/// </summary>
public class SnekTunezHype : Card, IRegisterable
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
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "SnekTunezHype", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/HypeSnekTunez.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        int x = 0;
        x = c.energy;
        return upgrade switch
        {
            Upgrade.B => 
            [
                ModEntry.Instance.KokoroApi.V2.EnergyAsStatus.MakeVariableHint().AsCardAction,
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.overdrive,
                    statusAmount = x,
                    xHint = 1
                },
                new AStunShip(),
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.corrode,
                    statusAmount = 1,
                }
            ],
            _ => 
            [
                ModEntry.Instance.KokoroApi.V2.EnergyAsStatus.MakeVariableHint().AsCardAction,
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.overdrive,
                    statusAmount = x,
                    xHint = 1
                },
                new AStunShip(),
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.corrode,
                    statusAmount = 1,
                },
                new AEndTurn()
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
                singleUse = true,
                artTint = "ff3838"
            },
            Upgrade.A => new CardData
            {
                cost = 0,
                singleUse = true,
                retain = true,
                artTint = "ff3838"
            },
            _ => new CardData
            {
                cost = 0,
                singleUse = true,
                artTint = "ff3838"
            }
        };
    }
}