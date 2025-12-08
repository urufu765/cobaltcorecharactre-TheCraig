using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// Ignores equal amount of corrode based on amount of fade present. May require transpiler on ACorrodeDamage
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "")]
public class WallSnek : Artifact
{
}