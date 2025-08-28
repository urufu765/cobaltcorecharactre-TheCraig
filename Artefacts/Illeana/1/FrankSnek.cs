using System;
using System.Collections.Generic;
using Illeana.Cards;
using Microsoft.Extensions.Logging;

namespace Illeana.Artifacts;

/// <summary>
/// Digitalized Slitherman, SNEKTUNEZ!!!
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common })]
public class SportsStereo : PersonalStereo
{
    public override Spr GetSprite()
    {
        return SongNumber switch
        {
            SnekTunez.Chill => ModEntry.Instance.SprSportsChill,
            SnekTunez.Hype => ModEntry.Instance.SprSportsHype,
            SnekTunez.Sad => ModEntry.Instance.SprSportsSad,
            SnekTunez.Groovy => ModEntry.Instance.SprSportsGroovy,
            _ => ModEntry.Instance.SprSportsOn
        };
    }

    public override void SongSelect(Combat combat, SnekTunez song)
    {
        // Overrides so no cards are added here
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        switch (SongNumber)
        {
            case SnekTunez.Chill:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezChill
                    {
                        upgrade = Upgrade.A,
                        temporaryOverride = true
                    },
                    destination = CardDestination.Deck,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Hype:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezHype
                    {
                        upgrade = Upgrade.A,
                        temporaryOverride = true
                    },
                    destination = CardDestination.Deck,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Sad:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezSad
                    {
                        upgrade = Upgrade.A,
                        temporaryOverride = true
                    },
                    destination = CardDestination.Deck,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Groovy:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezGroovy
                    {
                        upgrade = Upgrade.A,
                        temporaryOverride = true
                    },
                    destination = CardDestination.Deck,
                    artifactPulse = Key()
                });
                break;
            default:
                break;
        }
    }

    public override void AlsoDo(State state)
    {
        string artifactType = $"{ModEntry.Instance.UniqueName}::{typeof(PersonalStereo).Name}";
        foreach (Character character in state.characters)
        {
            if (character.deckType == ModEntry.Instance.IlleanaDeck.Deck)
            {
                foreach (Artifact artifact in character.artifacts)
                {
                    if (artifact.Key() == artifactType)
                    {
                        artifact.OnRemoveArtifact(state);
                    }
                }
                character.artifacts.RemoveAll(r => r.Key() == artifactType);
            }
        }
        //state.UpdateArtifactCache();
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        return SongNumber switch
        {
            SnekTunez.Chill => [ new TTCard
            {
                card = new SnekTunezChill
                {
                    upgrade = Upgrade.A,
                    temporaryOverride = true
                }
            },
            new TTGlossary("cardtrait.singleUse"),
            new TTGlossary("cardtrait.temporary")],
            SnekTunez.Hype => [ new TTCard
            {
                card = new SnekTunezHype
                {
                    upgrade = Upgrade.A,
                    temporaryOverride = true
                }
            },
            new TTGlossary("cardtrait.singleUse"),
            new TTGlossary("cardtrait.temporary")],
            SnekTunez.Sad => [ new TTCard
            {
                card = new SnekTunezSad
                {
                    upgrade = Upgrade.A,
                    temporaryOverride = true
                }
            },
            new TTGlossary("cardtrait.singleUse"),
            new TTGlossary("cardtrait.temporary")],
            SnekTunez.Groovy => [ new TTCard
            {
                card = new SnekTunezGroovy
                {
                    upgrade = Upgrade.A,
                    temporaryOverride = true
                }
            },
            new TTGlossary("cardtrait.singleUse"),
            new TTGlossary("cardtrait.temporary")],
            _ => [ new TTCard
            {
                card = new SnekTunezPlaceholder
                {
                    upgrade = Upgrade.A,
                    temporaryOverride = true
                },
                showCardTraitTooltips = false
            },
            new TTGlossary("cardtrait.singleUse"),
            new TTGlossary("cardtrait.temporary")]
        };
    }
}