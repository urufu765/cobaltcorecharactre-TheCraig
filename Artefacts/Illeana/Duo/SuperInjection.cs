using System.Collections.Generic;
using Illeana.Midrow;
using Nickel;

namespace Illeana.Artifacts;

/// <summary>
/// Launch corrode 2 missiles
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.goat)]
public class SuperInjection : Artifact
{
    public override StuffBase ReplaceSpawnedThing(State state, Combat combat, StuffBase thing, bool spawnedByPlayer)
    {
        if (thing is not BigCorrode and Missile missile && missile.missileType is MissileType.corrode)
        {
            return ConvertFromMissile(missile);
        }
        return base.ReplaceSpawnedThing(state, combat, thing, spawnedByPlayer);
    }

    public override List<Tooltip> GetExtraTooltips()
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


    private static BigCorrode ConvertFromMissile(Missile missile)
    {
        BigCorrode bigCorrode = new()
        {
            missileType = missile.missileType,
            skin = missile.skin,
            xDir = missile.xDir,
            x = missile.x,
            age = missile.age,
            xLerped = missile.xLerped,
            yAnimation = missile.yAnimation,
            isHitting = missile.isHitting,
            pulse = missile.pulse,
            hilight = missile.hilight,
            targetPlayer = missile.targetPlayer,
            fromPlayer = missile.fromPlayer,
            bubbleShield = missile.bubbleShield,
            droneNameAccordingToIsaac = missile.droneNameAccordingToIsaac,
        };
        return bigCorrode;
    }
}