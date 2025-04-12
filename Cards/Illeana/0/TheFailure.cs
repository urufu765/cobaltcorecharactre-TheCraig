using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// A card that's obtained from Build-A-Cure, Applies corrode upon draw, removes upon play
/// </summary>
public class TheFailure : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Token", "TheFailure", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/0/TheFailure.png").Sprite,

        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        return
        [
            new AStatus
            {
                targetPlayer = true,
                status = Status.corrode,
                statusAmount = -1,
                omitFromTooltips = true
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
                status = Status.corrode,
                statusAmount = 1
            }
        );
        if (upgrade == Upgrade.B)
        {
            c.QueueImmediate(
                new AStatus
                {
                    targetPlayer = false,
                    status = Status.corrode,
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
            description = ModEntry.Instance.Localizations.Localize(["card", "Token", "TheFailure", upgrade switch { Upgrade.B => "descB", _ => "desc" }])
        };
    }
}