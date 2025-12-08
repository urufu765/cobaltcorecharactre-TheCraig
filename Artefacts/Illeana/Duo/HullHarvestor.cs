using System.Collections.Generic;
using Illeana.Midrow;
using Nickel;

namespace Illeana.Artifacts;

/// <summary>
/// Launch corrode 2 missiles
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "urufudoggo.Weth::weth")]
public class HullHarvester : Artifact
{
    public int Hits{get;set;}
    public bool Depleted{get;set;}
    public const int HITLIMIT = 5;
    public bool InCombat { get; set; } = false;  // Visual purposes

    public override void OnCombatStart(State state, Combat combat)
    {
        InCombat = true;
    }
    public override Spr GetSprite()
    {
        return Depleted? ModEntry.Instance.SprHullHarvestDepleted : base.GetSprite();
    }
    public override int? GetDisplayNumber(State s)
    {
        return InCombat? Hits : null;
    }
    public override void OnTurnStart(State state, Combat combat)
    {
        Hits = 0;
        Depleted = false;
    }
    public override void OnCombatEnd(State state)
    {
        Hits = 0;
        Depleted = InCombat = false;
    }
    public override void OnEnemyGetHit(State state, Combat combat, Part? part)
    {
        if(Hits < HITLIMIT)
        {
            Hits++;
        }
        if(Hits == HITLIMIT && !Depleted)
        {
            combat.QueueImmediate(new AHeal
            {
                healAmount = 1,
                targetPlayer = true,
                artifactPulse = Key(),
                dialogueSelector = ".hullHarvesterHarvested"
            });
            Depleted = true;
        }
    }
}