using System.Collections.Generic;
using System.Reflection;
using DemoMod.Actions;
using Nanoray.PluginManager;
using Nickel;

namespace DemoMod.Cards;

public class LessonPlan : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.DemoDeck.Deck,
                rarity = Rarity.common,
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "LessonPlan", "name"]).Localize,
        });
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        return
        [
            new AGainPonder
            {
                Destination = CardDestination.Deck,
                Count = upgrade == Upgrade.B ? 5 : 3
            },
            new ADrawCard
            {
                count = upgrade == Upgrade.B ? 2 : 1
            }
        ];
    }

    public override CardData GetData(State state)
    {
        return new CardData
        {
            cost = 1,
            exhaust = true,
            buoyant = upgrade == Upgrade.A
        };
    }
}