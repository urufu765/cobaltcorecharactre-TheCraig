using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A card that's obtained from the artifact "Illeana's Personal Stereo", gives energy next turn
/// </summary>
public class SnekTunezChill : Card, IRegisterable
{
    private static Spr shoeSprite;
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "SnekTunezChill", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/ChillSnekTunez.png").Sprite
        });

        shoeSprite = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/ChillShoeTunez.png").Sprite;
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        int x = 0;
        x = c.energy;
        return upgrade switch
        {
            Upgrade.B => // TODO: Spoofed action
            [
                ModEntry.Instance.KokoroApi.V2.EnergyAsStatus.MakeVariableHint().AsCardAction,
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.energyNextTurn,
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
            Upgrade.A => 
            [
                ModEntry.Instance.KokoroApi.V2.EnergyAsStatus.MakeVariableHint().AsCardAction,
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.energyNextTurn,
                    statusAmount = x * 2,
                    xHint = 2
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
            _ => 
            [
                ModEntry.Instance.KokoroApi.V2.EnergyAsStatus.MakeVariableHint().AsCardAction,
                new AStatus
                {
                    targetPlayer = true,
                    status = Status.energyNextTurn,
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
            _ => new CardData
            {
                cost = 0,
                singleUse = true,
                artTint = "204ab7",
                art = ModEntry.Instance.shoeanaMode? shoeSprite : null
            }
        };
    }
}