using System;
using System.Collections.Generic;
using System.ComponentModel;
using Nickel;
using Illeana.Actions;

namespace Illeana.Midrow;

public class BigCorrode : Missile
{
    // public override Spr? GetIcon()
    // {
    //     return ModEntry.Instance.SprGiantAsteroidIcon;
    // }

    // public override List<Tooltip> GetTooltips()
    // {
    //     List<Tooltip> tooltips =
    //     [
    //         new GlossaryTooltip("objecttooltip.giantasteroid")
    //         {
    //             Icon = ModEntry.Instance.SprGiantAsteroidIcon,
    //             Title = ModEntry.Instance.Localizations.Localize(["object", "GiantAsteroid", "name"]),
    //             TitleColor = Colors.midrow,
    //             Description = ModEntry.Instance.Localizations.Localize(["object", "GiantAsteroid", "desc"]),
    //         },
    //         .. base.GetTooltips(),
    //     ];
    //     return tooltips;
    // }

    // public override void Render(G g, Vec v)
    // {
    //     DrawWithHilight(g, ModEntry.Instance.SprGiantAsteroid, v + GetOffset(g, false), Mutil.Rand(x + 0.1) > 0.5, Mutil.Rand(x + 0.2) > 0.5);
    // }

    public override List<CardAction>? GetActions(State s, Combat c)
    {
        return [
            new AMissileHit
            {
                worldX = x,
                outgoingDamage = missileData[MissileType.corrode].baseDamage,
                targetPlayer = targetPlayer,
                status = Status.corrode,
                statusAmount = 2
            }
        ];
    }

    public override List<Tooltip> GetTooltips()
    {
        return [
            new GlossaryTooltip("objecttooltip.bigcorrodemissile")
            {
                Icon = StableSpr.icons_missile_corrode,
                Title = ModEntry.Instance.Localizations.Localize(["midrow", "bigCorrode", "name"]),
                TitleColor = Colors.midrow,
                Description = ModEntry.Instance.Localizations.Localize(["midrow", "bigCorrode", "desc"]),
            }
        ];
    }
}