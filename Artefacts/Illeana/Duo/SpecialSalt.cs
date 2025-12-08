using System.Collections.Generic;
using Microsoft.Extensions.Logging;
// using Twos = TwosCompany.Artifacts;

namespace Illeana.Artifacts;

/// <summary>
/// Hitting tarnished ship with chain lightning increases tarnish stack
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "Mezz.TwosCompany::Mezz.TwosCompany.GaussDeck")]
public class SpecialSalt : Artifact//, Twos.IChainLightningArtifact
{
    private Status Tarnishing => ModEntry.Instance.TarnishStatus.Status;

    /// <summary>
    /// May not check which ship is being hit
    /// </summary>
    /// <param name="s"></param>
    /// <param name="chainCount"></param>
    public void OnChainLightning(State s, int chainCount)
    {
        ModEntry.Instance.Logger.LogInformation("AaaaChainLightning!!");
        if (s.route is Combat c && c.otherShip is not null && c.otherShip.hull > 0 && c.otherShip.Get(Tarnishing) > 0)
        {
            ModEntry.Instance.Logger.LogInformation("ChainLightninged");
            c.QueueImmediate(
                new AStatus
                {
                    status = Tarnishing,
                    statusAmount = 1,
                    targetPlayer = false,
                    artifactPulse = Key()
                }
            );
        }
    }
}