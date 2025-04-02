using System;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static class ArtifactDialogue
{
    internal static void Inject()
    {
        DB.story.all[$"ArtifactForgedCertificate_{AmIlleana}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            oncePerRunTags = [ "ForgedCertificate" ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "ForgedCertificate" ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    Text = "Oh hey, it's my old engineer certificate.",
                    loopTag = "neutral".Check()
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = AmPeri,
                            Text = "Is that written in crayon?",
                            loopTag = "squint"
                        },
                        new CustomSay()
                        {
                            who = AmIssac,
                            Text = "I'm never letting you touch my drones.",
                            loopTag = "squint"
                        }
                    }
                }
            }
        };
        DB.story.all[$"ArtifactByproductProcessor_{AmIlleana}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            oncePerRunTags = [ "ByproductProcessor" ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "ByproductProcessor" ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    Text = "Hey, I finally figured out a way to get something out of my failures. So please stop locking me in the airlock.",
                    loopTag = "intense".Check()
                }
            }
        };
        DB.story.all[$"ArtifactCausticArmor_{AmIlleana}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            oncePerRunTags = [ "CausticArmor" ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "CausticArmor" ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    Text = "Y'all keep complaining I'm ruining the integrity of the ship. So I developed a temporary fix.",
                    loopTag = "explain".Check()
                }
            }
        };
        DB.story.all[$"ArtifactExperimentalLubricant_{AmIlleana}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            oncePerRunTags = [ "ExperimentalLubricant" ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "ExperimentalLubricant" ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    Text = "Hey, I discovered a new use for this metal-eating substance, and it's got to do with speed."
                }
            }
        };
        DB.story.all[$"ArtifactExternalFuelSource_{AmIlleana}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            turnStart = true,
            maxTurnsThisCombat = 1,
            oncePerRunTags = [ "ExternalFuelSource" ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "ExternalFuelSource" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "neutral".Check(),
                    Text = "All this new material is providing a good source of fuel."
                }
            }
        };

        DB.story.all[$"ArtifactWarpPrototype_{AmIlleana}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            turnStart = true,
            maxTurnsThisCombat = 1,
            oncePerRunTags = [ "WarpPrototype" ],
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "WarpPrototype" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "silly".Check(),
                    Text = "Now THIS is how you make ships go vroom!"
                }
            }
        };
        DB.story.all[$"ArtifactWarpPrototype_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            turnStart = true,
            maxTurnsThisCombat = 1,
            oncePerRunTags = [ "WarpPrototype" ],
            allPresent = [ AmDizzy ],
            hasArtifacts = [ "WarpPrototype" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "squint",
                    Text = "I liked our old warp prep better."
                }
            }
        };
        DB.story.all[$"ArtifactLightenedLoad_{AmIlleana}_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "LightenedLoad" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "neutral".Check(),
                    Text = "Good news is, we probably maybe won't get hit..."
                }
            }
        };
        DB.story.all[$"ArtifactLightenedLoad_{AmIlleana}_1"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "LightenedLoad" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "intense".Check(),
                    Text = "Bad news is, if we do get hit, it's gonna hurt a lot."
                }
            }
        };
        DB.story.all[$"ArtifactWarpMastery_{AmIlleana}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            oncePerRunTags = [ "WarpMastery" ],
            hasArtifacts = [ "WarpMastery" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    Text = "Not what I would've done, but good enough."
                }
            }
        };


        // DB.story.all[$"ArtifactPersonalStereo_{AmIlleana}"] = new()
        // {
        //     type = NodeType.combat,
        //     oncePerRun = true,
        //     allPresent = [ AmIlleana ],
        //     hasArtifacts = [ "PersonalStereo" ],
        //     lines = new()
        //     {

        //     }
        // };
        // DB.story.all[$"ArtifactTempoboosters_{AmIlleana}"] = new()
        // {
        //     type = NodeType.combat,
        //     oncePerRun = true,
        //     allPresent = [ AmIlleana ],
        //     hasArtifacts = [ "Tempoboosters" ],
        //     lines = new()
        //     {

        //     }
        // };
        try
        {
            DB.story.all[$"ArtifactShieldPrepIsGone_Multi_0"].doesNotHaveArtifacts?.Add(
                "WarpPrototype"
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone0");
        }
        try
        {
            DB.story.all[$"ArtifactShieldPrepIsGone_Multi_1"].doesNotHaveArtifacts?.Add(
                "WarpPrototype"
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone1");
        }
        try
        {
            DB.story.all[$"ArtifactShieldPrepIsGone_Multi_2"].doesNotHaveArtifacts?.Add(
                "WarpPrototype"
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone2");
        }
        try
        {
            DB.story.all[$"ArtifactShieldPrepIsGone_Multi_3"].doesNotHaveArtifacts?.Add(
                "WarpPrototype"
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone3");
        }
        DB.story.all[$"ArtifactShieldPrepIsGone_{AmIlleana}"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            turnStart = true,
            maxTurnsThisCombat = 1,
            oncePerRunTags = [ "ShieldPrepIsGoneYouFool" ],
            allPresent = [ AmIlleana ],
            doesNotHaveArtifacts = [ "ShieldPrep", "WarpMastery", "WarpPrototype" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    Text = "Did someone misplace the warp prep? I was doing something with it."
                }
            }
        };
    }
}