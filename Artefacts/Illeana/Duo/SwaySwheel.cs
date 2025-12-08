using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// First move action each turn gives equal amount of temp shield
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "Mezz.TwosCompany::Mezz.TwosCompany.IsabelleDeck")]
public class SwaySwheel : Artifact
{
    public bool ShieldGiven {get;set;}

    public override void OnTurnStart(State state, Combat combat)
    {
        ShieldGiven = false;
    }

    public override void OnCombatEnd(State state)
    {
        ShieldGiven = false;
    }

    public override Spr GetSprite()
    {
        return ShieldGiven? ModEntry.Instance.SprSwaySwheelDepleted:base.GetSprite();
    }

    public override List<Tooltip> GetExtraTooltips()
    {
        return
        [
            new TTGlossary("status.tempShieldAlt")
        ];
    }
}

// Code for checking movements is actually in LooseStick.