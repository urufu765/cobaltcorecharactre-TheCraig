using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Nickel;

namespace Illeana.Artifacts;

public enum SurpriseStatus
{
    OutOfCombat,
    None,
    Corrode,
    Tarnish,
    Both
}

/// <summary>
/// When the Pew Pew go WHAT WHY
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.peri)]
public class ChemicalSurprise : Artifact
{
    public const int REQUIREMENT = 3;
    public int Count {get;set;} = 0;
    public const int TARNISH_SEND = 1;
    public const int OXIDATE_SEND = 3;
    public SurpriseStatus Surprise {get;set;} = SurpriseStatus.OutOfCombat;
    public static Status Tarnishing => ModEntry.Instance.TarnishStatus.Status;
    public static Status Oxyclean => ModEntry.Instance.KokoroApi.V2.OxidationStatus.Status;

    public override int? GetDisplayNumber(State s)
    {
        if (s.route is Combat)
        {
            if (s.ship.hull > 0)
            {
                Surprise = (s.ship.Get(Status.corrode), s.ship.Get(Tarnishing)) switch
                {
                    (>0, >0) => SurpriseStatus.Both,
                    (0, >0) => SurpriseStatus.Tarnish,
                    (>0, 0) => SurpriseStatus.Corrode,
                    _ => SurpriseStatus.None
                };
            }
        }
        else
        {
            Surprise = SurpriseStatus.OutOfCombat;
        }
        return Count;
    }
    public override void OnEnemyGetHit(State state, Combat combat, Part? part)
    {
        if (combat.otherShip is null || state.ship is null || combat.otherShip.hull <= 0 || state.ship.hull <= 0) return;

        if (
            combat.currentCardAction is AAttack attack && 
            (
                state.ship.Get(Status.corrode) > 0 || 
                state.ship.Get(Tarnishing) > 0
            ) &&
            (
                attack.fromDroneX is null || 
                (
                    ModEntry.exists_WethMod && 
                    WethSplitshot(attack)
                )
            )
        )
        {
            Count++;
            if (Count >= REQUIREMENT)
            {
                Count = 0;
                if (state.ship.Get(Tarnishing) > 0)
                {
                    combat.QueueImmediate(new AStatus
                    {
                        status = Tarnishing,
                        statusAmount = TARNISH_SEND,
                        targetPlayer = false,
                        artifactPulse = Key()
                    });
                }
                if (state.ship.Get(Status.corrode) > 0)
                {
                    combat.QueueImmediate(new AStatus
                    {
                        status = Oxyclean,
                        statusAmount = OXIDATE_SEND,
                        targetPlayer = false,
                        artifactPulse = Key()
                    });
                }
            }
        }
    }

    /// <summary>
    /// TODO: replace with an API once Weth is updated.
    /// </summary>
    /// <param name="aAttack"></param>
    /// <returns></returns>
    public static bool WethSplitshot(AAttack aAttack)
    {
        try
        {
            if (
                ModEntry.Instance.Helper.ModRegistry.LoadedMods.TryGetValue("urufudoggo.weth", out IModManifest? wethMod) && 
                wethMod is not null && 
                ModEntry.Instance.Helper.ModRegistry.GetModHelper(wethMod).ModData.TryGetModData(aAttack, "split", out bool s)
            )
            {
                return s;
            }
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to check for Weth!");
        }
        return false;
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        List<Tooltip> l = StatusMeta.GetTooltips(Oxyclean, OXIDATE_SEND);
        l.AddRange(StatusMeta.GetTooltips(Tarnishing, TARNISH_SEND));
        l.Insert(0, new TTGlossary("status.corrode", ["1"]));
        return l;
    }

    public override Spr GetSprite()
    {
        return Surprise switch
        {
            SurpriseStatus.None => ModEntry.Instance.SprChemSurpriseNone,
            SurpriseStatus.Corrode => ModEntry.Instance.SprChemSurpriseCor,
            SurpriseStatus.Tarnish => ModEntry.Instance.SprChemSurpriseTar,
            _ => base.GetSprite()
        };
    }

    
}