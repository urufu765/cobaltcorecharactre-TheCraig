using System;
using Illeana.Artifacts;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static class ArtifactDialogue
{
    internal static void Inject()
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
                            who = AmIssac,
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
        DB.story.all["ArtifactWarpMastery_Illeana"] = new()
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
                    what = "Not what I would've done, but good enough."
                }
            }
        };
        DB.story.all["ArtifactNanofiberHull1_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "NanofiberHull" ],
            oncePerRunTags = [ "NanofiberHull1" ],
            minDamageDealtToPlayerThisTurn = 1,
            maxDamageDealtToPlayerThisTurn = 1,
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "No worries, the nanofiber'll do my job for me.",
                }
            }
        };
        DB.story.all["ArtifactNanofiberHull2_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "NanofiberHull" ],
            oncePerRunTags = [ "NanofiberHull2" ],
            minDamageDealtToPlayerThisTurn = 2,
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    what = "Uh, back to work I suppose.",
                }
            }
        };
        DB.story.all["ArtifactArmoredBay_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "ArmoredBay" ],
            oncePerRunTags = [ "ArmoredBae" ],
            minDamageBlockedByPlayerArmorThisTurn = 1,
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    what = "This is brilliant, I get more time to spend on procrast- I mean other projects.",
                }
            }
        };
        DB.story.all["ArtifactDirtyEngines_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "DirtyEngines" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "sly".Check(),
                    what = "Mo powah baby.",
                }
            }
        };
        DB.story.all["ArtifactCockpitTargetIsNotRelevant_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            turnStart = true,
            maxTurnsThisCombat = 1,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "CockpitTarget" ],
            enemyDoesNotHavePart = "cockpit",
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "solemn".Check(),
                    what = "I mean it's not the end of the world. But what a waste.",
                }
            }
        };
        DB.story.all["ArtifactAresCannonV2_Illeana_0"] = new()
        {
            type = NodeType.combat,
            turnStart = true,
            maxTurnsThisCombat = 1,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "AresCannonV2" ],
            oncePerRunTags = [ "AresCannonV2" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    what = "Not gonna lie, this ship is starting to win me over.",
                }
            }
        };
        DB.story.all["ArtifactAresCannon_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "AresCannon" ],
            oncePerRunTags = [ "AresCannon" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    what = "Hmph, smaller ship... I think I can work with this.",
                }
            }
        };
        DB.story.all["ArtifactBrokenGlasses_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            turnStart = true,
            maxTurnsThisCombat = 1,
            hasArtifacts = [ "BrokenGlasses" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "Tired".Check(),
                    what = "What have we done?",
                }
            }
        };
        DB.story.all["ArtifactCrosslink_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "Crosslink" ],
            oncePerRunTags = [ "CrosslinkTriggerTag" ],
            lookup = [ "CrosslinkTrigger" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "More the merrier.",
                }
            }
        };
        DB.story.all["ArtifactDemonThrusters_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmRiggs ],
            turnStart = true,
            hasArtifacts = [ "DemonThrusters" ],
            oncePerRunTags = [ "ArtifactDemonThrusters" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "I hate the new restrictive engines you put in. Can we revert?",
                },
                new CustomSay
                {
                    who = AmRiggs,
                    what = "Too late."
                }
            }
        };
        DB.story.all["ArtifactEnergyPrep_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana, AmPeri ],
            hasArtifacts = [ "EnergyPrep" ],
            turnStart = true,
            maxTurnsThisCombat = 1,
            lines = new()
            {
                new CustomSay
                {
                    who = AmPeri,
                    what = "Backup energy ready to go."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    what = "Just in time to power up my thingamajig.",
                }
            }
        };
        DB.story.all["ArtifactEnergyRefund_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "EnergyRefund" ],
            minCostOfCardJustPlayed = 3,
            oncePerRunTags = [ "EnergyRefund" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "curious".Check(),
                    what = "Is this what rebate feels like?",
                }
            }
        };
        DB.story.all["ArtifactFractureDetection_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "FractureDetection" ],
            oncePerRunTags = [ "FractureDetectionBarks" ],
            turnStart = true,
            maxTurnsThisCombat = 1,
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "I think I see the brittle point... no that's just a smudge.",
                }
            }
        };
        DB.story.all["ArtifactGeminiCoreBooster_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "GeminiCoreBooster" ],
            oncePerRunTags = [ "GeminiCoreBooster" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "This does nothing for me.",
                }
            }
        };
        DB.story.all["ArtifactGeminiCore_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "GeminiCore" ],
            oncePerRunTags = [ "GeminiCore" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "solemn".Check(),
                    what = "I hate this ship. I can't tell what I'm working on with all this blue red nonsense.",
                }
            }
        };
        DB.story.all["ArtifactHullPlating_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "HullPlating" ],
            turnStart = true,
            maxTurnsThisCombat = 1,
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "Having legroom for corrosive experiments is always a nice bonus.",
                }
            }
        };
        DB.story.all["ArtifactIonConverter_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "IonConverter" ],
            oncePerRunTags = [ "IonConverterTag" ],
            lookup = [ "IonConverterTrigger" ],
            priority = true,
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    what = "Nothing wasted, the way things should be.",
                }
            }
        };
        DB.story.all["ArtifactJetThrustersNoRiggs_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "JetThrusters" ],
            oncePerRunTags = [ "OncePerRunThrusterJokesAboutRiggsICanMakeTheseTagsStupidlyLongIfIWant" ],
            nonePresent = [ AmRiggs ],
            maxTurnsThisCombat = 1,
            turnStart = true,
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "Saves me the burden of taking the wheel.",
                }
            }
        };
        DB.story.all["ArtifactJumperCablesUseless_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "JumperCables" ],
            oncePerRunTags = [ "ArtifactJumperCablesUnneeded" ],
            maxTurnsThisCombat = 1,
            minHullPercent = 1,
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "holdcable".Check(),
                    what = "We're clearly doing something wrong if we don't have a use for thes cables.",
                }
            }
        };
        DB.story.all["ArtifactRecalibrator_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "Recalibrator" ],
            playerShotJustMissed = true,
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    what = "I love it when things are not wasted.",
                }
            }
        };
        DB.story.all["ArtifactRevengeDriveCorrode_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana, AmPeri ],
            minDamageDealtToPlayerThisTurn = 1,
            lastTurnPlayerStatuses = [ Status.corrode ],
            hasArtifacts = [ "RevengeDrive" ],
            oncePerRunTags = [ "RevengeDriveShouts" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    what = "Boosting damage with new solution. Take it away, Peri!",
                },
                new CustomSay
                {
                    who = AmPeri,
                    loopTag = "mad",
                    what = "Illeana, that's sweet and all but can you stop disintegrating our ship."
                }
            }
        };
        DB.story.all["ArtifactSharpEdges_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "SharpEdges" ],
            playerJustShuffledDiscardIntoDrawPile = true,
            oncePerCombat = true,
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "If I ever had to go on the offense, this is the strat I'd have to use.",
                }
            }
        };
        DB.story.all["ArtifactSimplicity_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            hasArtifacts = [ "Simplicity" ],
            oncePerRunTags = [ "SimplicityShouts" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "Hey, where did all my stuff go?",
                }
            }
        };
        // DB.story.all["Artifact_Illeana_0"] = new()
        // {
        //     type = NodeType.combat,
        //     oncePerRun = true,
        //     allPresent = [ AmIlleana ],
        //     hasArtifacts = [ "" ],
        //     oncePerRunTags = [ "" ],
        //     lines = new()
        //     {
        //         new CustomSay
        //         {
        //             who = AmIlleana,
        //             what = "",
        //         }
        //     }
        // };

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
            DB.story.all["ArtifactShieldPrepIsGone_Multi_0"].doesNotHaveArtifacts?.Add(
                "WarpPrototype".F()
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone0");
        }
        try
        {
            DB.story.all["ArtifactShieldPrepIsGone_Multi_1"].doesNotHaveArtifacts?.Add(
                "WarpPrototype".F()
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone1");
        }
        try
        {
            DB.story.all["ArtifactShieldPrepIsGone_Multi_2"].doesNotHaveArtifacts?.Add(
                "WarpPrototype".F()
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone2");
        }
        try
        {
            DB.story.all["ArtifactShieldPrepIsGone_Multi_3"].doesNotHaveArtifacts?.Add(
                "WarpPrototype".F()
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone3");
        }
        DB.story.all["ArtifactShieldPrepIsGone_Illeana"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            turnStart = true,
            maxTurnsThisCombat = 1,
            oncePerRunTags = [ "ShieldPrepIsGoneYouFool" ],
            allPresent = [ AmIlleana ],
            doesNotHaveArtifacts = [ "ShieldPrep", "WarpMastery", "WarpPrototype".F() ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "Did someone misplace the warp prep? I was doing something with it."
                }
            }
        };
    }
}