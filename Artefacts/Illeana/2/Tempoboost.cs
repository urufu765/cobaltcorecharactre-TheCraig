using System;
using System.Collections.Generic;
using System.Linq;
using Illeana.Cards;

namespace Illeana.Artifacts;


public enum TBoosters
{
    Off,
    Elite,
    Boss
}

public enum TBoostType
{
    Off,
    Corrode,
    Tarnish
}

/// <summary>
/// Wings count as empty, empty parts take 1 damage when a shot passes through
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Boss })]
public class Tempoboosters : Artifact
{
    public TBoostType BoostType {get; set;}

    public bool BoostGiven {get; set;}

    public override void OnCombatStart(State state, Combat combat)
    {
        if (state?.map?.markers[state.map.currentLocation]?.contents is MapBattle mb)
        {
            BoostGiven = false;
            if (mb.battleType == BattleType.Elite || mb.battleType == BattleType.Boss)
            {
                if (state.EnumerateAllArtifacts().Any(a => a is LightenedLoad))
                {
                    BoostType = TBoostType.Tarnish;
                }
                else
                {
                    BoostType = TBoostType.Corrode;
                }
            }
            else
            {
                BoostType = TBoostType.Off;
            }
        }
    }

    public override void OnCombatEnd(State state)
    {
        BoostType = TBoostType.Off;
    }

    /// <summary>
    /// Due to how Tarnish works, the actions need to be queued at this point or else the Tarnish stack goes away.
    /// </summary>
    public override void OnTurnStart(State state, Combat combat)
    {
        if (!BoostGiven)
        {
            if (BoostType == TBoostType.Corrode)
            {
                BoostGiven = true;
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
            else if (BoostType == TBoostType.Tarnish)
            {
                BoostGiven = true;
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
                        status = ModEntry.Instance.TarnishStatus.Status,
                        statusAmount = 3,
                        targetPlayer = true,
                        artifactPulse = Key()
                    }
                ]);
            }
        }
    }

    public override Spr GetSprite()
    {
        return BoostType switch
        {
            TBoostType.Corrode => ModEntry.Instance.SprBoostrE,
            TBoostType.Tarnish => ModEntry.Instance.SprBoostrB,
            _ => ModEntry.Instance.SprBoostrO
        };
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        List<Tooltip> l = StatusMeta.GetTooltips(ModEntry.Instance.TarnishStatus.Status, 4);
        l.Insert(0, new TTGlossary("status.corrode", ["1"]));
        l.Insert(0, new TTGlossary("status.ace", ["1"]));
        return l;
    }
}