using System.Collections.Generic;
using System.Reflection;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Cards;

/// <summary>
/// What a bitch.
/// </summary>
public class WeaponisedPatchkit : Card, IRegisterable
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
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Rare", "WeaponisedPatchkit", "name"]).Localize,
            Art = ModEntry.RegisterSprite(package, "assets/Card/Illeana/3/WeaponisedPatchkit.png").Sprite
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
                    statusAmount = 3,
                    targetPlayer = true
                },
                new AAttack
                {
                    weaken = true,
                    damage = GetDmg(s, 0)
                }
            ],
            Upgrade.A =>
            [
                new AStatus
                {
                    status = Status.corrode,
                    statusAmount = 1,
                    targetPlayer = false
                },
                new AAttack
                {
                    armorize = true,
                    damage = GetDmg(s, 0)
                }
            ],
            _ => 
            [
                new AAttack
                {
                    status = Status.corrode,
                    statusAmount = 1,
                    damage = GetDmg(s, 0)
                },
                new AAttack
                {
                    armorize = true,
                    damage = GetDmg(s, 0)
                }
            ],
        };
    }


    public override CardData GetData(State state)
    {
        return upgrade switch
        {
            _ => new CardData
            {
                cost = 2,
            },
        };
    }
}