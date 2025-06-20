using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using Illeana.Artifacts;
using Illeana.External;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal class NewArtifactDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>(){
            {"ArtifactShieldPrepIsGone_Illeana", new(){
                type = NodeType.combat,
                oncePerRun = true,
                turnStart = true,
                maxTurnsThisCombat = 1,
                oncePerRunTags = [ "ShieldPrepIsGoneYouFool" ],
                allPresent = [ AmIlleana ],
                doesNotHaveArtifacts = [ "ShieldPrep", "WarpMastery"],
                doesNotHaveArtifactTypes = [ typeof(WarpPrototype) ],
                dialogue = [
                    new(AmIlleana, "squint", "Did someone misplace the warp prep? I was doing something with it.")
                ]
            }},
            {"ArtifactWarpMastery_Illeana", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                oncePerRunTags = [ "WarpMastery" ],
                hasArtifacts = [ "WarpMastery" ],
                dialogue = [
                    new(AmIlleana, "explain", "Not what I would've done, but good enough.")
                ]
            }},
            {"ArtifactNanofiberHull1_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "NanofiberHull" ],
                oncePerRunTags = [ "NanofiberHull1" ],
                minDamageDealtToPlayerThisTurn = 1,
                maxDamageDealtToPlayerThisTurn = 1,
                dialogue = [
                    new(AmIlleana, "explain", "No worries, the nanofiber'll do my job for me.")
                ]
            }},
            {"ArtifactNanofiberHull2_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "NanofiberHull" ],
                oncePerRunTags = [ "NanofiberHull2" ],
                minDamageDealtToPlayerThisTurn = 2,
                dialogue = [
                    new(AmIlleana, "Uh, back to work I suppose.")
                ]
            }},
            {"ArtifactArmoredBay_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "ArmoredBay" ],
                oncePerRunTags = [ "ArmoredBae" ],
                minDamageBlockedByPlayerArmorThisTurn = 1,
                dialogue = [
                    new(AmIlleana, "This is brilliant, I get more time to spend on procrast- I mean other projects.")
                ]
            }},
            {"ArtifactDirtyEngines_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "DirtyEngines" ],
                dialogue = [
                    new(AmIlleana, "sly", "Mo powah baby.")
                ]
            }},
            {"ArtifactCockpitTargetIsNotRelevant_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                turnStart = true,
                maxTurnsThisCombat = 1,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "CockpitTarget" ],
                enemyDoesNotHavePart = "cockpit",
                dialogue = [
                    new(AmIlleana, "solemn", "I mean it's not the end of the world. But what a waste.")
                ]
            }},
            {"ArtifactAresCannonV2_Illeana_0", new(){
                type = NodeType.combat,
                turnStart = true,
                maxTurnsThisCombat = 1,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "AresCannonV2" ],
                oncePerRunTags = [ "AresCannonV2" ],
                dialogue = [
                    new(AmIlleana, "Not gonna lie, this ship is starting to win me over.")
                ]
            }},
            {"ArtifactAresCannon_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "AresCannon" ],
                oncePerRunTags = [ "AresCannon" ],
                dialogue = [
                    new(AmIlleana, "Hmph, smaller ship... I think I can work with this.")
                ]
            }},
            {"ArtifactBrokenGlasses_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                turnStart = true,
                maxTurnsThisCombat = 1,
                hasArtifacts = [ "BrokenGlasses" ],
                dialogue = [
                    new(AmIlleana, "shocked", "What have we done?")
                ]   
            }},
            {"ArtifactCrosslink_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "Crosslink" ],
                oncePerRunTags = [ "CrosslinkTriggerTag" ],
                lookup = [ "CrosslinkTrigger" ],
                dialogue = [
                    new(AmIlleana, "More the merrier.")
                ]
            }},
            {"ArtifactDemonThrusters_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmRiggs ],
                turnStart = true,
                hasArtifacts = [ "DemonThrusters" ],
                oncePerRunTags = [ "ArtifactDemonThrusters" ],
                dialogue = [
                    new(AmIlleana, "squint", "I hate the new restrictive engines you put in. Can we revert?"),
                    new(AmRiggs, "Too late.")
                ]
            }},
            {"ArtifactEnergyPrep_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana, AmPeri ],
                hasArtifacts = [ "EnergyPrep" ],
                turnStart = true,
                maxTurnsThisCombat = 1,
                dialogue = [
                    new(AmPeri, "Backup energy ready to go."),
                    new(AmIlleana, "Just in time to power up my thingamajig.")
                ]
            }},
            {"ArtifactEnergyRefund_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "EnergyRefund" ],
                minCostOfCardJustPlayed = 3,
                oncePerRunTags = [ "EnergyRefund" ],
                dialogue = [
                    new(AmIlleana, "curious", "Is this what rebate feels like?")
                ]
            }},
            {"ArtifactFractureDetection_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "FractureDetection" ],
                oncePerRunTags = [ "FractureDetectionBarks" ],
                turnStart = true,
                maxTurnsThisCombat = 1,
                dialogue = [
                    new(AmIlleana, "squint", "I think I see the brittle point... no that's just a smudge...")
                ]
            }},
            {"ArtifactGeminiCoreBooster_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "GeminiCoreBooster" ],
                oncePerRunTags = [ "GeminiCoreBooster" ],
                dialogue = [
                    new(AmIlleana, "squint", "This does nothing for me.")
                ]
            }},
            {"ArtifactGeminiCore_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "GeminiCore" ],
                oncePerRunTags = [ "GeminiCore" ],
                dialogue = [
                    new(AmIlleana, "tired", "I hate this ship. I can't tell what I'm working on with all this blue red light nonsense.")
                ]
            }},
            {"ArtifactGeminiCore_Multi_4", new(){
                edit = [
                    new("af738a7e", AmIlleana, "squint", "Neither. I hate them both equally.")
                ]
            }},
            {"ArtifactHullPlating_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "HullPlating" ],
                turnStart = true,
                maxTurnsThisCombat = 1,
                dialogue = [
                    new(AmIlleana, "explain", "Having legroom for corrosive experiments is always a nice bonus.")
                ]
            }},
            {"ArtifactIonConverter_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "IonConverter" ],
                oncePerRunTags = [ "IonConverterTag" ],
                lookup = [ "IonConverterTrigger" ],
                priority = true,
                dialogue = [
                    new(AmIlleana, "Nothing wasted, the way things should be.")
                ]
            }},
            {"ArtifactJetThrustersNoRiggs_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "JetThrusters" ],
                oncePerRunTags = [ "OncePerRunThrusterJokesAboutRiggsICanMakeTheseTagsStupidlyLongIfIWant" ],
                nonePresent = [ AmRiggs ],
                maxTurnsThisCombat = 1,
                turnStart = true,
                dialogue = [
                    new(AmIlleana, "nap", "Saves me the burden of taking the wheel.")
                ]
            }},
            {"ArtifactJumperCablesUseless_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "JumperCables" ],
                oncePerRunTags = [ "ArtifactJumperCablesUnneeded" ],
                maxTurnsThisCombat = 1,
                minHullPercent = 1,
                dialogue = [
                    new(AmIlleana, "holdcable", "We're clearly doing something wrong if we don't have a use for thes cables.")
                ]
            }},
            {"ArtifactJumperCablesUseless_Illeana_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "JumperCables" ],
                oncePerRunTags = [ "ArtifactJumperCablesUnneeded" ],
                maxTurnsThisCombat = 1,
                minHullPercent = 1,
                dialogue = [
                    new(AmIlleana, "holdcable", "Why did we even get these?")
                ]
            }},
            {"ArtifactRecalibrator_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "Recalibrator" ],
                playerShotJustMissed = true,
                dialogue = [
                    new(AmIlleana, "explain", "I love it when things are not wasted.")
                ]
            }},
            {"ArtifactRevengeDriveCorrode_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana, AmPeri ],
                minDamageDealtToPlayerThisTurn = 1,
                lastTurnPlayerStatuses = [ Status.corrode ],
                hasArtifacts = [ "RevengeDrive" ],
                oncePerRunTags = [ "RevengeDriveShouts" ],
                dialogue = [
                    new(AmIlleana, "silly", "Boosting damage with new solution. Take it away, Peri!"),
                    new(AmPeri, "mad", "Illeana, that's sweet and all but can you stop disintegrating our ship.")
                ]
            }},
            {"ArtifactSharpEdges_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "SharpEdges" ],
                playerJustShuffledDiscardIntoDrawPile = true,
                oncePerCombat = true,
                dialogue = [
                    new(AmIlleana, "silly", "If I had a ship of my own, I'm definitely having one of these installed.")
                ]
            }},
            {"ArtifactSimplicity_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "Simplicity" ],
                oncePerRunTags = [ "SimplicityShouts" ],
                dialogue = [
                    new(AmIlleana, "squint", "Hey, where did all my stuff go?")
                ]
            }},
            {"ArtifactSimplicity_Illeana_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "Simplicity" ],
                oncePerRunTags = [ "SimplicityShouts" ],
                dialogue = [
                    new(AmIlleana, "squint", "Has anyone seen my stuff?")
                ]
            }},
            {"ArtifactTiderunner_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                turnStart = true,
                maxTurnsThisCombat = 1,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "TideRunner" ],
                oncePerRunTags = [ "TideRunner" ],
                dialogue = [
                    new(AmIlleana, "silly", "Now this, is my kind of ship!")
                ]
            }},
            {"ArtifactTiderunner_Illeana_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                turnStart = true,
                maxTurnsThisCombat = 1,
                allPresent = [ AmIlleana, AmRiggs ],
                hasArtifacts = [ "TideRunner" ],
                oncePerRunTags = [ "TideRunner" ],
                dialogue = [
                    new(AmIlleana, "Move over, I want to try piloting this one."),
                    new(AmRiggs, "squint", "Go right ahead. I'll be hurling up into a bucket if you need me.")
                ]
            }},
            {"ArtifactTiderunner_Illeana_2", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                turnStart = true,
                maxTurnsThisCombat = 1,
                hasArtifacts = [ "TideRunner" ],
                oncePerRunTags = [ "TideRunner" ],
                dialogue = [
                    new(AmIlleana, "I like this ship, but I wish it came in a metal finish. Wood just isn't as good of a material to test my stuff with.")
                ]
            }},
            {"ArtifactTridimensionalCockpit_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "TridimensionalCockpit" ],
                oncePerRunTags = [ "TridimensionalCockpit" ],
                dialogue = [
                    new(AmIlleana, "shocked", "Alright, real funny guys. I fit in the scaffolding just fine. Now please let me back in the cockpit.")
                ]
            }},
            {"ArtifactForgedCertificate_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "ForgedCertificate" ],
                allPresent = [ AmIlleana ],
                hasArtifactTypes = [ typeof(ForgedCertificate) ],
                dialogue = [
                    new(AmIlleana, "Oh hey, it's my old engineer certificate."),
                    new([
                        new(AmPeri, "squint", "Is that written in crayon?"),
                        new(AmDizzy, "squint", "I could show you mi- hey wait that doesn't look real."),
                        new(AmIsaac, "squint", "I'm never letting you touch my drones."),
                        new(AmMax, "You need a certificate to be an engineer?")
                    ])
                ]
            }},
            {"ArtifactByproductProcessor_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "ByproductProcessor" ],
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "ByproductProcessor".F() ],
                dialogue = [
                    new(AmIlleana, "intense", "Hey, I finally figured out a way to get something out of my failures. So please stop locking me in the airlock.")
                ]
            }},
            {"ArtifactCausticArmor_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "CausticArmor" ],
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "CausticArmor".F() ],

                dialogue = [
                    new(AmIlleana, "explain", "Y'all keep complaining I'm ruining the integrity of the ship. So I developed a temporary fix.")
                ]
            }},
            {"ArtifactExperimentalLubricant_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "ExperimentalLubricant" ],
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "ExperimentalLubricant".F() ],
                dialogue = [
                    new(AmIlleana, "explain", "Who knew such a dangerous substance would work so well at lubricating the engines?")
                ]
            }},
            {"ArtifactExternalFuelSource_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                lookup = [ "gotTemp" ],
                oncePerRunTags = [ "ExternalFuelSource" ],
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "ExternalFuelSource".F() ],
                dialogue = [
                    new(AmIlleana, "All this new material is providing an excellent source of fuel.")
                ]
            }},
            {"ArtifactWarpPrototype_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                turnStart = true,
                maxTurnsThisCombat = 1,
                oncePerRunTags = [ "WarpPrototype" ],
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "WarpPrototype".F() ],
                dialogue = [
                    new(AmIlleana, "silly", "Now THIS is how you make ships go vroom!")
                ]
            }},
            {"ArtifactWarpPrototype_Multi_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                turnStart = true,
                maxTurnsThisCombat = 1,
                oncePerRunTags = [ "WarpPrototype" ],
                allPresent = [ AmDizzy ],
                hasArtifacts = [ "WarpPrototype".F() ],
                dialogue = [    
                    new(AmDizzy, "squint", "I liked our old warp prep better.")
                ]
            }},
            {"ArtifactLightenedLoad_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "LightenedLoad".F() ],
                lastTurnPlayerStatuses = [ Tarnished ],
                dialogue = [
                    new(AmIlleana, "explain", "Good news is, we probably maybe won't get hit.")
                ]
            }},
            {"ArtifactLightenedLoad_Illeana_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                hasArtifacts = [ "LightenedLoad".F() ],
                lastTurnPlayerStatuses = [ Tarnished ],
                dialogue = [
                    new(AmIlleana, "intense", "Bad news is, if we do get hit, it's gonna hurt a lot.")
                ]
            }},
            {"ArtifactLightenedLoadWeDodge_Illeana", new(){
                type = NodeType.combat,
                oncePerRun = true,
                enemyShotJustMissed = true,
                allPresent = [ AmIlleana ],
                lastTurnPlayerStatuses = [ Tarnished ],
                hasArtifacts = [ "LightenedLoad".F() ],
                dialogue = [
                    new(AmIlleana, "sly", "See? Believe.")
                ]
            }},
            {"ArtifactShieldPrepIsGone_Multi_0", new(){
                doesNotHaveArtifactTypes = [typeof(WarpPrototype)]
            }},
            {"ArtifactShieldPrepIsGone_Multi_1", new(){
                doesNotHaveArtifactTypes = [typeof(WarpPrototype)]
            }},
            {"ArtifactShieldPrepIsGone_Multi_2", new(){
                doesNotHaveArtifactTypes = [typeof(WarpPrototype)]
            }},
            {"ArtifactShieldPrepIsGone_Multi_3", new(){
                doesNotHaveArtifactTypes = [typeof(WarpPrototype)]
            }}
            
        });

        LocalDB.DumpStoryToLocalLocale("en", "Shockah.DuoArtifacts", new Dictionary<string, DialogueMachine>(){
            {"ArtifactReusableScrap_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "ReusableScrapButWeAlsoHaveShieldBurst" ],
                allPresent = [ AmIlleana, AmDizzy],
                hasArtifacts = [ "ReusableScrap".F(), "ShieldBurst" ],
                dialogue = [
                    new(AmIlleana, "Hey, I made something new!"),
                    new(AmDizzy, "squint", "That's plagiarism.")
                ]
            }},
            {"ArtifactReusableScrap_Illeana_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "ReusableScrapButWeAlsoHaveShieldBurst" ],
                allPresent = [ AmIlleana, AmDizzy],
                hasArtifacts = [ "ReusableScrap".F(), "ShieldBurst" ],
                dialogue = [
                    new(AmIlleana, "Fresh from the labs."),
                    new(AmDizzy, "squint", "Do we need both?")
                ]
            }},
            {"ArtifactReusableScrap_Illeana_2", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "ReusableScrapButWeAlsoHaveShieldBurst" ],
                allPresent = [ AmIlleana, AmDizzy],
                hasArtifacts = [ "ReusableScrap".F(), "ShieldBurst"],
                dialogue = [
                    new(AmDizzy, "squint", "Did you take that from my desk?"),
                    new(AmIlleana, "silly", "Noooooo?")
                ]
            }},
            {"ArtifactReusableScrap_Illeana_3", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "ReusableScrapButIlleanaWasFirst" ],
                allPresent = [ AmIlleana, AmDizzy],
                hasArtifacts = [ "ReusableScrap".F()],
                doesNotHaveArtifacts = ["ShieldBurst"],
                dialogue = [
                    new(AmIlleana, "Hey Dizzy, look what I've got!"),
                    new(AmDizzy, "squint", "Why does that look so familiar?")
                ]
            }},
            {"ArtifactReusableScrap_Illeana_4", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "ReusableScrapButIlleanaWasFirst" ],
                allPresent = [ AmIlleana, AmDizzy],
                hasArtifacts = [ "ReusableScrap".F()],
                doesNotHaveArtifacts = ["ShieldBurst"],
                dialogue = [
                    new(AmDizzy, "Hey, that's something I might've come up with!"),
                    new(AmIlleana, "explain", "And that's why you need me here.")
                ]
            }},
            {"ArtifactThurstThursters_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "ThurstThrusters" ],
                allPresent = [ AmIlleana, AmRiggs ],
                hasArtifactTypes = [ typeof(ThrustThursters) ],
                dialogue = [
                    new(AmRiggs, "squint", "Do we even need this much thrust?"),
                    new([
                        new(AmIlleana, "sly", "Trust me bro."),
                        new(AmIlleana, "Yes ma'am!"),
                        new(AmIlleana, "Yup!"),
                        new(AmIlleana, "Definitely."),
                        new(AmIlleana, "Without a doubt.")
                    ])
                ]
            }},
            {"ArtifactThurstThursters_Illeana_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "ThurstThrusters" ],
                allPresent = [ AmIlleana, AmRiggs ],
                hasArtifactTypes = [ typeof(ThrustThursters) ],
                dialogue = [
                    new(AmRiggs, "Where's all this thrust coming from?"),
                    new(AmIlleana, "giggle", "Hehe.")
                ]
            }},
            {"ArtifactAirlock_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "AirlockedSnek" ],
                allPresent = [ AmIlleana, AmPeri ],
                hasArtifactTypes = [ typeof(AirlockSnek) ],
                dialogue = [
                    new(AmIlleana, "panic", "Wait Peri! No! Don't lock me in the airlock again!"),
                    new(AmPeri, "mad", "Quiet, I'm concentrating.")
                ]
            }},
            {"ArtifactAirlock_Illeana_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "AirlockedSnek" ],
                allPresent = [ AmIlleana, AmPeri ],
                hasArtifactTypes = [ typeof(AirlockSnek) ],
                dialogue = [
                    new(AmIlleana, "shocked", "Peri! I'm sorry! I won't do it again!"),
                    new(AmPeri, "mad", "I'm not falling for that again.")
                ]
            }},
            {"ArtifactAirlock_Illeana_2", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "IlleanaGotAirlocked" ],
                priority = true,
                allPresent = [ AmPeri ],
                hasArtifactTypes = [ typeof(AirlockSnek) ],
                dialogue = [
                    new(AmPeri, "nap", "Ah, peace and quiet.")
                ]
            }},
            {"ArtifactAirlock_Illeana_3", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "IlleanaGotAirlocked" ],
                priority = true,
                allPresent = [ AmPeri ],
                hasArtifactTypes = [ typeof(AirlockSnek) ],
                dialogue = [
                    new(AmPeri, "vengeful", "Well that big snake was good for something I guess.")
                ]
            }},
            {"ArtifactAirlock_Illeana_4", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "IlleanaGotAirlocked" ],
                priority = true,
                allPresent = [ AmPeri ],
                hasArtifactTypes = [ typeof(AirlockSnek) ],
                dialogue = [
                    new(AmPeri, "vengeful", "I should keep her locked up in the airlock more often.")
                ]
            }},
            {"ArtifactSuperInjection_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "AcidAcidBaby" ],
                allPresent = [ AmIlleana, AmIsaac ],
                hasArtifactTypes = [ typeof(SuperInjection) ],
                dialogue = [
                    new(AmIlleana, "Check this out! I made the acid more potent!"),
                    new(AmIsaac, "squint", "That's not gonna melt through the container, is it?")
                ]
            }},
            {"ArtifactSuperInjection_Illeana_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "AcidAcidBaby" ],
                allPresent = [ AmIlleana, AmIsaac ],
                hasArtifactTypes = [ typeof(SuperInjection) ],
                dialogue = [
                    new(AmIsaac, "Umm, I don't even launch this type of missile?"),
                    new(AmIlleana, "sly", "I got you, bro.")
                ]
            }},
            {"ArtifactSuperInjection_Illeana_2", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "AcidAcidBaby" ],
                allPresent = [ AmIlleana, AmIsaac ],
                hasArtifactTypes = [ typeof(SuperInjection) ],
                dialogue = [
                    new(AmIsaac, "Normally, I'm all for missiles, but keep that one away from me please."),
                    new(AmIlleana, "silly", "Aww, scared of getting a bit of face-melting acid on yourself?")
                ]
            }},
            {"ArtifactLubedHeatpump_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "WondersOfLubrication" ],
                allPresent = [ AmIlleana, AmDrake ],
                hasArtifactTypes = [ typeof(LubricatedHeatpump) ],
                dialogue = [
                    new(AmDrake, "Woah! Did someone improve the cooling system?"),
                    new([
                        new(AmIlleana, "explain", "You're welcome."),
                        new(AmIlleana, "Guess who?"),
                        new(AmIlleana, "squint", "That still doesn't mean you can start a fire by the way.")
                    ])
                ]
            }},
            {"ArtifactExtraSlip_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "SlipNSlideOnAcid" ],
                allPresent = [ AmIlleana, AmMax ],
                hasArtifactTypes = [ typeof(ExtraSlip) ],
                dialogue = [
                    new(AmMax, "intense", "Why is the floor so slippery?"),
                    new(AmIlleana, "intense", "Uhh whoops.")
                ]
            }},
            {"ArtifactPerfectPerfect_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "GotDaPerfect" ],
                allPresent = [ AmIlleana, AmCat ],
                hasArtifactTypes = [ typeof(PerfectedProtection) ],
                dialogue = [
                    new(AmIlleana, "Hey computer! Look what I got you!"),
                    new(AmCat, "squint", "That's good. Now get back to work.")
                ]
            }},
            {"ArtifactPerfectPerfect_Illeana_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                oncePerRunTags = [ "GotDaPerfect" ],
                allPresent = [ AmIlleana, AmCat ],
                hasArtifactTypes = [ typeof(PerfectedProtection) ],
                dialogue = [
                    new(AmIlleana, "explain", "Behold, my masterpiece!"),
                    new(AmCat, "mad", "Is that all you've got to show for destroying half of the ship?!")
                ]
            }},
            {"ArtifactHullHarvestor_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                lookup = ["hullHarvesterHarvested"],
                oncePerCombatTags = ["hullHarvestoreded"],
                allPresent = [ AmIlleana, AmWeth ],
                hasArtifactTypes = [ typeof(HullHarvester) ],
                dialogue = [
                    new(AmIlleana, "Way to go!"),
                    new(AmWeth, "happy", "Thanks!")
                ]
            }},
            {"ArtifactHullHarvestor_Illeana_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                lookup = ["hullHarvesterHarvested"],
                oncePerCombatTags = ["hullHarvestoreded"],
                allPresent = [ AmIlleana, AmWeth ],
                hasArtifactTypes = [ typeof(HullHarvester) ],
                dialogue = [
                    new(AmIlleana, "explain", "And that's enough to make one good hull."),
                    new(AmWeth, "isthisa", "Happy to help!")
                ]
            }},
            {"ArtifactHullHarvestor_Illeana_2", new(){
                type = NodeType.combat,
                oncePerRun = true,
                lookup = ["hullHarvesterHarvested"],
                oncePerCombatTags = ["hullHarvestoreded"],
                allPresent = [ AmIlleana, AmWeth ],
                hasArtifactTypes = [ typeof(HullHarvester) ],
                dialogue = [
                    new(AmIlleana, "Who knew broken scrap could be so useful?"),
                    new(AmWeth, "dontcare", "You've only scratched the surface.")
                ]
            }},
            {"ArtifactHullHarvestor_Illeana_3", new(){
                type = NodeType.combat,
                oncePerRun = true,
                lookup = ["hullHarvesterHarvested"],
                oncePerCombatTags = ["hullHarvestoreded"],
                allPresent = [ AmIlleana, AmWeth ],
                hasArtifactTypes = [ typeof(HullHarvester) ],
                dialogue = [
                    new(AmIlleana, "I think I understand why you're so insistent on collecting debris."),
                    new(AmWeth, "bringiton", "Join me!")
                ]
            }},
        });
    }
}