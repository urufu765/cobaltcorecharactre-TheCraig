using System.Collections.Generic;
using System.Reflection;
using Illeana.Features;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A card that's obtained from Build-A-Cure, Applies corrode upon draw, removes upon play
/// </summary>
public class TheAccident : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "TheAccident", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/TheAccident.png").Sprite,

        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return
        [
            new AStatus
            {
                targetPlayer = false,
                status = ModEntry.Instance.TarnishStatus.Status,
                statusAmount = 1
            }
        ];
    }


    public override void OnDraw(State s, Combat c)
    {
        base.OnDraw(s, c);
        c.QueueImmediate(
            new AStatus
            {
                targetPlayer = true,
                status = ModEntry.Instance.TarnishStatus.Status,
                statusAmount = 1
            }
        );
        if (upgrade == Upgrade.B)
        {
            c.QueueImmediate(
                new AStatus
                {
                    targetPlayer = false,
                    status = ModEntry.Instance.TarnishStatus.Status,
                    statusAmount = 1
                }
            );
        }
    }


    public override CardData GetData(State state)
    {
        return new CardData
        {
            cost = upgrade == Upgrade.A ? 0 : 1,
            temporary = true,
            exhaust = true,
            description = ModEntry.Instance.Localizations.Localize(["card", "Token", "TheAccident", upgrade switch { Upgrade.B => "descB", _ => "desc" }]),
            artTint = "a43fff"
        };
    }
}