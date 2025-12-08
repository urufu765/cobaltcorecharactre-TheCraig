using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// Missiles deal one damage less, but apply oxidation. Gotta find how to make missiles apply oxidation. TODO: Who the fuck is fred?
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "")]
public class Oxyboom : Artifact
{
    public override int ModifyBaseMissileDamage(State state, Combat? combat, bool targetPlayer)
    {
        return -1;
    }
}

// Insert transpiler for the AMissileHit to make the missile also give the status effect on top of everything.