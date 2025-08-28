using System;
using System.Collections.Generic;
using System.Linq;
using Illeana.Cards;
using Microsoft.Extensions.Logging;

namespace Illeana.Artifacts;

/// <summary>
/// Digitalized Slitherman, SNEKTUNEZ!!!
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common })]
public class DigitalizedStereo : PersonalStereo
{
    public override Spr GetSprite()
    {
        return SongNumber switch
        {
            SnekTunez.Chill => ModEntry.Instance.SprDigitalChill,
            SnekTunez.Hype => ModEntry.Instance.SprDigitalHype,
            SnekTunez.Sad => ModEntry.Instance.SprDigitalSad,
            SnekTunez.Groovy => ModEntry.Instance.SprDigitalGroovy,
            _ => ModEntry.Instance.SprDigitalOn
        };
    }


    public override void SongSelect(Combat combat, SnekTunez song)
    {
        switch (song)
        {
            case SnekTunez.Chill:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezChill
                    {
                        upgrade = Upgrade.B,
                        discount = 1
                    },
                    destination = CardDestination.Hand,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Hype:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezHype
                    {
                        upgrade = Upgrade.B,
                        discount = 1
                    },
                    destination = CardDestination.Hand,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Sad:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezSad
                    {
                        upgrade = Upgrade.B,
                        discount = 1
                    },
                    destination = CardDestination.Hand,
                    artifactPulse = Key()
                });
                break;
            case SnekTunez.Groovy:
                combat.Queue(new AAddCard
                {
                    amount = 1,
                    card = new SnekTunezGroovy
                    {
                        upgrade = Upgrade.B,
                        discount = 1
                    },
                    destination = CardDestination.Hand,
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
                    upgrade = Upgrade.B,
                    discount = 1
                }
            },
            new TTGlossary("cardtrait.singleUse"),
            new TTGlossary("cardtrait.expensive", ["1"])],
            SnekTunez.Hype => [ new TTCard
            {
                card = new SnekTunezHype
                {
                    upgrade = Upgrade.B,
                    discount = 1
                }
            },
            new TTGlossary("cardtrait.singleUse"),
            new TTGlossary("cardtrait.expensive", ["1"])],
            SnekTunez.Sad => [ new TTCard
            {
                card = new SnekTunezSad
                {
                    upgrade = Upgrade.B,
                    discount = 1
                }
            },
            new TTGlossary("cardtrait.singleUse"),
            new TTGlossary("cardtrait.expensive", ["1"])],
            SnekTunez.Groovy => [ new TTCard
            {
                card = new SnekTunezGroovy
                {
                    upgrade = Upgrade.B,
                    discount = 1
                }
            },
            new TTGlossary("cardtrait.singleUse"),
            new TTGlossary("cardtrait.expensive", ["1"])],
            _ => [ new TTCard
            {
                card = new SnekTunezPlaceholder
                {
                    upgrade = Upgrade.B,
                    discount = 1
                },
                showCardTraitTooltips = false
            },
            new TTGlossary("cardtrait.singleUse"),
            new TTGlossary("cardtrait.expensive", ["1"])]
        };
    }
}