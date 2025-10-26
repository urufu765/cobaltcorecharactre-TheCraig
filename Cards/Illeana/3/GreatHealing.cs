using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// I need healing
/// </summary>
public class GreatHealing : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Rare", "GreatHealing", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/3/GreatHealing.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        int x = s.ship.Get(Status.corrode) + 1;
        int y = s.ship.Get(ModEntry.Instance.TarnishStatus.Status);
        return upgrade switch
        {
            Upgrade.B => 
            [
                new AVariableHint
                {
                    status = ModEntry.Instance.TarnishStatus.Status
                },
                new AHeal
                {
                    healAmount = y * 3,
                    targetPlayer = true,
                    xHint = new int?(3)
                },
                new AStatus
                {
                    status = ModEntry.Instance.TarnishStatus.Status,
                    statusAmount = y * 2,
                    xHint = 2,
                    mode = AStatusMode.Set,
                    targetPlayer = true
                },
                new AEndTurn()
            ],
            Upgrade.A => 
            [
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 1,
                    targetPlayer = true
                },
                new AVariableHint
                {
                    status = new Status?(Status.corrode)
                },
                new AHeal
                {
                    healAmount = x * 3,
                    targetPlayer = true,
                    xHint = new int?(3)
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = -1,
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
                new AVariableHint
                {
                    status = new Status?(Status.corrode)
                },
                new AHeal
                {
                    healAmount = x * 3,
                    targetPlayer = true,
                    xHint = new int?(3)
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
                cost = 2,
                exhaust = true,
                artTint = "a43fff"
            },
            Upgrade.A => new CardData
            {
                cost = 1,
                exhaust = true
            },
            _ => new CardData
            {
                cost = 2,
                exhaust = true
            },
        };
    }
}