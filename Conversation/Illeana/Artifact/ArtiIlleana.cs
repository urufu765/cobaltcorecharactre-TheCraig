using System;
using Illeana.Artifacts;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static partial class ArtifactDialogue
{
    private static void IlleanaInjects()
    {
        DB.story.all["ArtifactForgedCertificate_Illeana"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            oncePerRunTags = [ "ForgedCertificate".F() ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "ForgedCertificate".F() ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Oh hey, it's my old engineer certificate.",
                    loopTag = "neutral".Check()
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = AmPeri,
                            what = "Is that written in crayon?",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = AmIsaac,
                            what = "I'm never letting you touch my drones.",
                            loopTag = "squint"
                        }
                    }
                }
            }
        };
        DB.story.all["ArtifactByproductProcessor_Illeana"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            oncePerRunTags = [ "ByproductProcessor".F() ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "ByproductProcessor".F() ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Hey, I finally figured out a way to get something out of my failures. So please stop locking me in the airlock.",
                    loopTag = "intense".Check()
                }
            }
        };
        DB.story.all["ArtifactCausticArmor_Illeana"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            oncePerRunTags = [ "CausticArmor".F() ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "CausticArmor".F() ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Y'all keep complaining I'm ruining the integrity of the ship. So I developed a temporary fix.",
                    loopTag = "explain".Check()
                }
            }
        };
        DB.story.all["ArtifactExperimentalLubricant_Illeana"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            oncePerRunTags = [ "ExperimentalLubricant".F() ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "ExperimentalLubricant".F() ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "Hey, I discovered a new use for this metal-eating substance, and it's got to do with speed."
                }
            }
        };
        DB.story.all["ArtifactExternalFuelSource_Illeana"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            lookup = [ "gotTemp" ],
            oncePerRunTags = [ "ExternalFuelSource".F() ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "ExternalFuelSource".F() ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "neutral".Check(),
                    what = "All this new material is providing a good source of fuel."
                }
            }
        };
        DB.story.all["ArtifactWarpPrototype_Illeana"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            turnStart = true,
            maxTurnsThisCombat = 1,
            oncePerRunTags = [ "WarpPrototype".F() ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "WarpPrototype".F() ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "silly".Check(),
                    what = "Now THIS is how you make ships go vroom!"
                }
            }
        };
        DB.story.all["ArtifactWarpPrototype_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            turnStart = true,
            maxTurnsThisCombat = 1,
            oncePerRunTags = [ "WarpPrototype".F() ],
            allPresent = [ AmDizzy ],
            hasArtifacts = [ "WarpPrototype".F() ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "squint",
                    what = "I liked our old warp prep better."
                }
            }
        };
        DB.story.all["ArtifactLightenedLoad_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "LightenedLoad".F() ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "neutral".Check(),
                    what = "Good news is, we probably maybe won't get hit..."
                }
            }
        };
        DB.story.all["ArtifactLightenedLoad_Illeana_1"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "LightenedLoad".F() ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "intense".Check(),
                    what = "Bad news is, if we do get hit, it's gonna hurt a lot."
                }
            }
        };
        DB.story.all["ArtifactLightenedLoadWeDodge_Illeana"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            turnStart = true,
            allPresent = [ AmIlleana ],
            lastTurnPlayerStatuses = [ Status.autododgeRight, Tarnished ],
            hasArtifacts = [ "LightenedLoad".F() ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "sly".Check(),
                    what = "See? Believe."
                }
            }
        };
    }
}