using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace DemoMod.Cards;

/*
 * Cards are a character's bread and butter.
 * Most characters contain 9 commons, 7 uncommons, and 5 rares, but nothing requires this distribution.
 * In addition, some cards have additional cards, which aren't offered but can still be obtained.
 * Ensure that after you create a card, that you add its type to the ModEntry's list of types to register.
 */
public class Ponder : Card, IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        helper.Content.Cards.RegisterCard(new CardConfiguration
        {
            /*
             * This is a trick to obtain the type housing the current method, to reduce the effort of making a new card.
             */
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new CardMeta
            {
                deck = ModEntry.Instance.DemoDeck.Deck,
                rarity = Rarity.common,
                dontOffer = true,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Ponder", "name"]).Localize,
            // Art = ModEntry.RegisterSprite(package, "assets/Cards/Ponder.png").Sprite
        });
    }

    /*
     * Each card has a list of actions, which decides both what is rendered on the card, and what actually happens on play.
     * There are many ways to construct the actions a card contains.
     * The following is a simple approach, good for if the actions on the card only vary in their numbers.
     */
    public override List<CardAction> GetActions(State s, Combat c)
    {
        return
        [
            new AStatus
            {
                /*
                 * When registering a status, you receive an IStatusEntry, which itself contains the Status.
                 * It is helpful to retain the IStatusEntry, in the event other metadata for it must be obtained.
                 */
                status = ModEntry.Instance.KnowledgeStatus.Status,
                statusAmount = 1,
                targetPlayer = true
            },
            new ADrawCard
            {
                /*
                 * The Ternary operator (q ? t : f) is handy for simple, in-line if-else calculations.
                 * This one states that if the upgrade is Upgrade.B (q), then the count is 2 (t), else it's 1 (f).
                 */
                count = upgrade == Upgrade.B ? 2 : 1
            }
        ];
    }

    /*
     * Each card also has CardData, which is things such as its cost, if it's playable, if it has exhaust, recycle, infinite, etc.
     * In addition, different arts and art tints can be enforced.
     */
    public override CardData GetData(State state)
    {
        return new CardData
        {
            cost = 0,
            exhaust = true,
            temporary = true,
            retain = upgrade == Upgrade.A
        };
    }
}