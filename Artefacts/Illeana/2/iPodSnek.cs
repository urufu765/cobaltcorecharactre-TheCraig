using System;
using System.Collections.Generic;
using Craig.Cards;

namespace Craig.Artifacts;


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
    private SnekTunez _cardState = 0;
    public bool Repeat {private get; set;} = true;


    public override Spr GetSprite()
    {
        return _cardState switch
        {
            SnekTunez.Chill => ModEntry.Instance.SprTunezChill,
            SnekTunez.Hype => ModEntry.Instance.SprTunezHype,
            SnekTunez.Sad => ModEntry.Instance.SprTunezSad,
            SnekTunez.Groovy => ModEntry.Instance.SprTunezGroovy,
            _ => ModEntry.Instance.SprTunezOff
        };
    }


    public override void OnReceiveArtifact(State state)
    {
        if (_cardState == SnekTunez.Start) _cardState++;
    }


    public override void OnCombatStart(State state, Combat combat)
    {
        switch (_cardState)
        {
            case SnekTunez.Chill:
                combat.QueueImmediate(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezChill(),
                    destination = CardDestination.Hand
                });
                break;
            case SnekTunez.Hype:
                combat.QueueImmediate(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezHype(),
                    destination = CardDestination.Hand
                });
                break;
            case SnekTunez.Sad:
                combat.QueueImmediate(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezSad(),
                    destination = CardDestination.Hand
                });
                break;
            case SnekTunez.Groovy:
                combat.QueueImmediate(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezGroovy(),
                    destination = CardDestination.Hand
                });
                break;
            default:
                break;
        }
        _cardState++;
        if (_cardState == SnekTunez.End && Repeat)
        {
            _cardState = SnekTunez.Start + 1;
        }
    }


    /// <summary>
    /// Return the tooltip advanced by 1
    /// </summary>
    /// <returns></returns>
    public override List<Tooltip>? GetExtraTooltips()
    {
        return _cardState switch
        {
            SnekTunez.Start => base.GetExtraTooltips(),
            SnekTunez.Chill => [ new TTCard
            {
                card = new SnekTunezHype()
            },
            new TTGlossary("cardtrait.singleUse")],
            SnekTunez.Hype => [ new TTCard
            {
                card = new SnekTunezSad()
            },
            new TTGlossary("cardtrait.singleUse")],
            SnekTunez.Sad => [ new TTCard
            {
                card = new SnekTunezGroovy()
            },
            new TTGlossary("cardtrait.singleUse")],
            _ => [ new TTCard
            {
                card = new SnekTunezChill()
            },
            new TTGlossary("cardtrait.singleUse")]
        };
    }


    /// <summary>
    /// Returns the description advanced by 1
    /// </summary>
    /// <returns></returns>
    public override string Description()
    {
        return ModEntry.Instance.Localizations.Localize(["artifact", "Boss", "IlleanasPersonalStereo", _cardState switch {
            SnekTunez.Start => "desc",
            SnekTunez.Chill => "descHype",
            SnekTunez.Hype => "descSad",
            SnekTunez.Sad => "descGroovy",
            _ => "descChill"
        }]);
    }
}