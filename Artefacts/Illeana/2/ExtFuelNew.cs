using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// Gives 1 evade per temporary card gained
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Boss })]
public class ExternalFuelSource : Artifact
{
    private const int LIMIT = 4;
    public int Charge {get; set;}
    public bool InCombat { get; set; } = false;  // Visual purposes

    public override void OnCombatEnd(State state)
    {
        InCombat = false;
        Charge = 0;
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        InCombat = true;
    }

    public override int? GetDisplayNumber(State s)
    {
        return InCombat? Charge : null;
    }

    public override Spr GetSprite()
    {
        if (!InCombat) return base.GetSprite();
        return Charge switch {
            0 => ModEntry.Instance.SprEFLdepleted,
            _ => ModEntry.Instance.SprEFLavailable
        };
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        if (Charge < LIMIT) Charge++;
    }

    public override void OnPlayerRecieveCardMidCombat(State state, Combat combat, Card card)
    {
        if (Charge > 0 && card.GetDataWithOverrides(state).temporary)
        {
            combat.QueueImmediate(new AStatus
            {
                status = Status.evade,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key(),
                dialogueSelector = ".gotTemp"
            });
            Charge--;
        }
    }

    public override List<Tooltip> GetExtraTooltips()
    {
        return
        [
            new TTGlossary("cardtrait.temporary"),
            new TTGlossary("status.evade", ["1"])
        ];
    }
}