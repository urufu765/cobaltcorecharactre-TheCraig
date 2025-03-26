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
/// Wings count as empty, empty parts take 1 damage when a shot passes through
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
            SnekTunez.Chill => ModEntry.Instance.SprTunezChill,
            SnekTunez.Hype => ModEntry.Instance.SprTunezHype,
            SnekTunez.Sad => ModEntry.Instance.SprTunezSad,
            SnekTunez.Groovy => ModEntry.Instance.SprTunezGroovy,
            _ => ModEntry.Instance.SprTunezOn
        };
    }


    public override void OnReceiveArtifact(State state)
    {
        if (SongNumber == SnekTunez.Start) SongNumber++;
    }


    public override void OnCombatStart(State state, Combat combat)
    {
        switch (SongNumber)
        {
            case SnekTunez.Chill:
                combat.QueueImmediate(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezChill(),
                    destination = CardDestination.Hand,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Hype:
                combat.QueueImmediate(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezHype(),
                    destination = CardDestination.Hand,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Sad:
                combat.QueueImmediate(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezSad(),
                    destination = CardDestination.Hand,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Groovy:
                combat.QueueImmediate(new AAddCard
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
        SongNumber++;
        if (SongNumber == SnekTunez.End && Repeat)
        {
            SongNumber = SnekTunez.Start + 1;
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