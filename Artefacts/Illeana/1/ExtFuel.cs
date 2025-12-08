using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// Gives 1 evade per temporary card gained
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common })]
public class ExternalFuelSource : Artifact
{
    private const int LIMIT = 2;
    public int Limiter {get; set;}
    public bool InCombat { get; set; } = false;  // Visual purposes

    public override void OnCombatEnd(State state)
    {
        InCombat = false;
        Limiter = 0;
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        InCombat = true;
    }

    public override int? GetDisplayNumber(State s)
    {
        return InCombat? Limiter : null;
    }

    public override Spr GetSprite()
    {  // TODO: sprites
        return Limiter switch {
            >= LIMIT => ModEntry.Instance.SprEFLdepleted,
            _ => ModEntry.Instance.SprEFLavailable
        };
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        Limiter = 0;
    }

    public override void OnPlayerRecieveCardMidCombat(State state, Combat combat, Card card)
    {
        if (Limiter < LIMIT && card.GetDataWithOverrides(state).temporary)
        {
            combat.QueueImmediate(new AStatus
            {
                status = Status.evade,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key(),
                dialogueSelector = ".gotTemp"
            });
            Limiter++;
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