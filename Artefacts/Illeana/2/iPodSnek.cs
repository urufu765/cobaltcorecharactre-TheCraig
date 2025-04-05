using System;
using System.Collections.Generic;
using Illeana.Cards;

namespace Illeana.Artifacts;


public enum SnekTunez
{
    Start,
    Chill,
    Hype,
    Sad,
    Groovy,
    End
}

/// <summary>
/// Regular Slitherman, SNEKTUNEZ!!!
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Boss })]
public class PersonalStereo : Artifact
{
    public SnekTunez SongNumber {get; set;}
    public bool Repeat {private get; set;} = true;


    public override Spr GetSprite()
    {
        return SongNumber switch
        {
            SnekTunez.Chill => ModEntry.Instance.SprStolenChill,
            SnekTunez.Hype => ModEntry.Instance.SprStolenHype,
            SnekTunez.Sad => ModEntry.Instance.SprStolenSad,
            SnekTunez.Groovy => ModEntry.Instance.SprStolenGroovy,
            _ => ModEntry.Instance.SprStolenOn
        };
    }


    public override void OnReceiveArtifact(State state)
    {
        if (SongNumber == SnekTunez.Start) SongNumber++;
        AlsoDo(state);
    }

    public virtual void AlsoDo(State state)
    {
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        if (combat.turn == 1)
        {
            SongSelect(combat, SongNumber);
            SongNumber++;
            if (SongNumber == SnekTunez.End && Repeat)
            {
                SongNumber = SnekTunez.Start + 1;
            }
        }
    }


    public virtual void SongSelect(Combat combat, SnekTunez song)
    {
        switch (song)
        {
            case SnekTunez.Chill:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezChill(),
                    destination = CardDestination.Hand,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Hype:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezHype(),
                    destination = CardDestination.Hand,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Sad:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezSad(),
                    destination = CardDestination.Hand,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Groovy:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezGroovy(),
                    destination = CardDestination.Hand,
                    artifactPulse = Key()
                });
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// Return the tooltip advanced by 1
    /// </summary>
    /// <returns></returns>
    public override List<Tooltip>? GetExtraTooltips()
    {
        return SongNumber switch
        {
            SnekTunez.Chill => [ new TTCard
            {
                card = new SnekTunezChill()
            },
            new TTGlossary("cardtrait.singleUse")],
            SnekTunez.Hype => [ new TTCard
            {
                card = new SnekTunezHype()
            },
            new TTGlossary("cardtrait.singleUse")],
            SnekTunez.Sad => [ new TTCard
            {
                card = new SnekTunezSad()
            },
            new TTGlossary("cardtrait.singleUse")],
            SnekTunez.Groovy => [ new TTCard
            {
                card = new SnekTunezGroovy()
            },
            new TTGlossary("cardtrait.singleUse")],
            _ => base.GetExtraTooltips()
        };
    }


    /// <summary>
    /// Returns the description advanced by 1
    /// </summary>
    /// <returns></returns>
    public override string Description()
    {
        return ModEntry.Instance.Localizations.Localize(["artifact", "Boss", "IlleanasPersonalStereo", SongNumber switch {
            SnekTunez.Start => "desc",
            SnekTunez.Chill => "descHype",
            SnekTunez.Hype => "descSad",
            SnekTunez.Sad => "descGroovy",
            _ => "descChill"
        }]);
    }
}