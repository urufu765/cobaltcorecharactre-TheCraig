using System;
using System.Collections.Generic;
using Craig.Cards;

namespace Craig.Artifacts;


public enum TBoosters
{
    Off,
    Elite,
    Boss
}

/// <summary>
/// Wings count as empty, empty parts take 1 damage when a shot passes through
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Boss })]
public class Tempoboosters : Artifact
{
    public TBoosters boost = TBoosters.Off;

    public override void OnCombatStart(State state, Combat combat)
    {
        if (state?.map?.markers[state.map.currentLocation]?.contents is MapBattle mb)
        {
            if (mb.battleType == BattleType.Elite)
            {
                boost = TBoosters.Elite;
                combat.Queue(
                [
                    new AStatus
                    {
                        status = Status.ace,
                        statusAmount = 1,
                        targetPlayer = true,
                        artifactPulse = Key()
                    },
                    new AStatus
                    {
                        status = Status.corrode,
                        statusAmount = 1,
                        targetPlayer = true,
                        artifactPulse = Key()
                    }
                ]);
            }
            else if (mb.battleType == BattleType.Boss)
            {
                boost = TBoosters.Boss;
                combat.Queue(
                [
                    new AStatus
                    {
                        status = Status.ace,
                        statusAmount = 1,
                        targetPlayer = true,
                        artifactPulse = Key()
                    },
                    new AStatus
                    {
                        status = Status.corrode,
                        statusAmount = 1,
                        targetPlayer = true,
                        artifactPulse = Key()
                    },
                    new AStatus
                    {
                        status = Status.ace,
                        statusAmount = 1,
                        targetPlayer = true,
                        artifactPulse = Key()
                    },
                    new AStatus
                    {
                        status = ModEntry.Instance.TarnishStatus.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        artifactPulse = Key()
                    }
                ]);
            }
            else
            {
                boost = TBoosters.Off;
            }
        }
    }

    public override void OnCombatEnd(State state)
    {
        boost = TBoosters.Off;
    }

    public override Spr GetSprite()
    {
        return boost switch
        {
            TBoosters.Elite => ModEntry.Instance.SprBoostrE,
            TBoosters.Boss => ModEntry.Instance.SprBoostrB,
            _ => ModEntry.Instance.SprBoostrO
        };
    }
}