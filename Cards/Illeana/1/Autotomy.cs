using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// Wait, Illeana isn't biologically capable of-
/// </summary>
public class Autotomy : Card, IRegisterable, IHasCustomCardTraits
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
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Common", "Autotomy", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/1/Autotomy.png").Sprite
        });
    }


    public override List<CardAction> GetActions(State s, Combat c)
    {
        int x = s.ship.Get(Status.corrode);
        int y = s.ship.Get(ModEntry.Instance.TarnishStatus.Status);
        return upgrade switch
        {
            Upgrade.B => 
            [
                new AVariableHint
                {
                    status = ModEntry.Instance.TarnishStatus.Status
                },
                new AHurt
                {
                    hurtAmount = y,
                    xHint = 1,
                    hurtShieldsFirst = true,
                    targetPlayer = true
                },
                new AMove
                {
                    isRandom = true,
                    dir = y * 2,
                    xHint = 2,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = ModEntry.Instance.TarnishStatus.Status,
                    statusAmount = 0,
                    mode = AStatusMode.Set,
                    targetPlayer = true,
                    dialogueSelector = ".autotomySnek"
                }
            ],
            _ => 
            [
                new AVariableHint
                {
                    status = Status.corrode
                },
                new AHurt
                {
                    hurtAmount = x,
                    xHint = 1,
                    hurtShieldsFirst = true,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.autododgeRight,
                    statusAmount = x,
                    xHint = 1,
                    targetPlayer = true
                },
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 0,
                    mode = AStatusMode.Set,
                    targetPlayer = true,
                    dialogueSelector = ".autotomySnek"
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
                cost = 0,
                exhaust = true,
                artTint = "a43fff"
            },
            _ => new CardData
            {
                cost = 0,
                exhaust = true
            }
        };
    }

    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state)
    {
        return upgrade == Upgrade.None? [] : new HashSet<ICardTraitEntry>{ModEntry.Instance.KokoroApi.V2.Heavy.Trait};
    }
}