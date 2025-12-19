using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using Illeana.Artifacts;
using Illeana.External;
using static Illeana.Conversation.CommonDefinitions;

namespace Illeana.Conversation;

internal class NewCombatDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>(){
            {"ThatsALotOfDamageToUs_Illeana_0", new(){
                type = NodeType.combat,
                enemyShotJustHit = true,
                minDamageDealtToPlayerThisTurn = 3,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "panic", "Too much damage! Too much damage!")
                ]
            }},
            {"ThatsALotOfDamageToUs_Illeana_1", new(){
                type = NodeType.combat,
                enemyShotJustHit = true,
                minDamageDealtToPlayerThisTurn = 3,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "shocked", "That's too big of a hole to patch, even for me.")
                ]
            }},
            {"ThatsALotOfDamageToUs_Illeana_2", new(){
                type = NodeType.combat,
                enemyShotJustHit = true,
                minDamageDealtToPlayerThisTurn = 3,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "intense", "I can fix it... I can fix it...")
                ]
            }},
            {"ThatsALotOfDamageToThem_Illeana_0", new(){
                type = NodeType.combat,
                playerShotJustHit = true,
                minDamageDealtToEnemyThisTurn = 10,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "shocked", "That's a lot of damage!")
                ]
            }},
            {"ThatsALotOfDamageToThem_Illeana_1", new(){
                type = NodeType.combat,
                playerShotJustHit = true,
                minDamageDealtToEnemyThisTurn = 10,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "silly", "Booyah!")
                ]
            }},
            {"WeGotShotButTookNoDamage_Illeana_0", new(){
                type = NodeType.combat,
                enemyShotJustHit = true,
                maxDamageDealtToPlayerThisTurn = 0,
                lastTurnPlayerStatuses = [Status.perfectShield],

                oncePerRun = true,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "explain", "The results of my constant experimentions. Behold, perfection.")
                ]
            }},
            {"WeGotShotButTookNoDamage_Illeana_1", new(){
                type = NodeType.combat,
                enemyShotJustHit = true,
                maxDamageDealtToPlayerThisTurn = 0,
                lastTurnPlayerStatuses = [Status.perfectShield],

                oncePerRun = true,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "explain", "See? All that hull perforation wasn't in vain.")
                ]
            }},
            {"WeGotShotButTookNoDamage_Illeana_2", new(){
                type = NodeType.combat,
                enemyShotJustHit = true,
                maxDamageDealtToPlayerThisTurn = 0,
                lastTurnPlayerStatuses = [Status.perfectShield],

                oncePerRun = true,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "explain", "That could've been really bad... if you didn't believe in my research.")
                ]
            }},
            {"WeAreMovingAroundALot_Illeana_0", new(){
                type = NodeType.combat,
                minMovesThisTurn = 3,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "mad", "Dodge and weave! Dodge and weave!")
                ]
            }},
            {"WeAreMovingAroundALot_Illeana_1", new(){
                type = NodeType.combat,
                minMovesThisTurn = 3,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "explain", "The best form of defence is running away... wait no I meant movement.")
                ]
            }},
            {"ShopKeepBattleInsult", new(){
                edit = [
                    new("66ea84d6", AmIlleana, "panic", "Who said yes? WHO SAID YES?!"),
                    new("66ea84d6", AmIlleana, "shocked", "I'm so sorry, my crewmates are idiots! Please forgive us!")
                ]
            }},
            {"HandOnlyHasTrashCards_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                handFullOfTrash = true,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "intense", "The trash is overflowing into my workspace!")
                ]
            }},
            {"HandOnlyHasUnplayableCards_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                handFullOfUnplayableCards = true,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "squint", "I can't do anything with this.")
                ]
            }},
            {"BooksWentMissing_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["booksWentMissing"],
                lastTurnPlayerStatuses = [Status.missingBooks],
                dialogue = [
                    new(AmIlleana, "shocked", "Hey, where'd Books go?")
                ]
            }},
            {"CatWentMissing_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["CatWentMissing"],
                lastTurnPlayerStatuses = [Status.missingCat],
                dialogue = [
                    new(who: AmIlleana,
                        loopTag: "panic".Check(),
                        what: "Uhh maybe if I upload myself to the computer...")
                ]
            }},
            {"DizzyWentMissing_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["dizzyWentMissing"],
                lastTurnPlayerStatuses = [Status.missingDizzy],
                dialogue = [
                    new(AmIlleana, "intense", "Oh no.")
                ]
            }},
            {"DrakeWentMissing_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["drakeWentMissing"],
                lastTurnPlayerStatuses = [Status.missingDrake],
                dialogue = [
                    new(AmIlleana, "intense", "Why does it suddenly feel so... lonely?")
                ]
            }},
            {"IsaacWentMissing_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["isaacWentMissing"],
                lastTurnPlayerStatuses = [Status.missingIsaac],
                dialogue = [
                    new(AmIlleana, "panic", "Ah!")
                ]
            }},
            {"MaxWentMissing_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["maxWentMissing"],
                lastTurnPlayerStatuses = [Status.missingMax],
                dialogue = [
                    new(AmIlleana, "intense", "Now who's gonna fix my broken equipment?")
                ]
            }},
            {"PeriWentMissing_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["periWentMissing"],
                lastTurnPlayerStatuses = [Status.missingPeri],
                dialogue = [
                    new(AmIlleana, "shocked", "Wait no I already miss her!")
                ]
            }},
            {"RiggsWentMissing_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["riggsWentMissing"],
                lastTurnPlayerStatuses = [Status.missingRiggs],
                dialogue = [
                    new(AmIlleana, "panic", "BUDDY NOOOOOO!!")
                ]
            }},
            {"WeDontOverlapWithEnemyAtAll_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                priority = true,
                shipsDontOverlapAtAll = true,
                nonePresent = [ "crab", "scrap" ],
                oncePerRun = true,
                oncePerRunTags = [ "NoOverlapBetweenShips" ],
                dialogue = [
                    new(AmIlleana, "silly", "Gone. Goodbye!")
                ]
            }},
            {"WeDontOverlapWithEnemyAtAllButWeDoHaveASeekerToDealWith_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                priority = true,
                shipsDontOverlapAtAll = true,
                oncePerCombatTags = [ "NoOverlapBetweenShipsSeeker"],
                anyDronesHostile = [ "missile_seeker" ],
                nonePresent = [ "crab" ],
                dialogue = [
                    new(AmIlleana, "squint", "What's the point of evasive maneuvers if we're gonna get hit anyways?")
                ]
            }},
            {"BlockedALotOfAttackWithArmor_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                enemyShotJustHit = true,
                minDamageBlockedByPlayerArmorThisTurn = 3,
                oncePerCombatTags = ["YowzaThatWasALOTofArmorBlock"],
                oncePerRun = true,
                dialogue = [
                    new(AmIlleana, "squint", "It would've been better if we just avoided getting hit in the first place.")
                ]
            }},
            {"BlockedAnEnemyAttackWithArmor_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                enemyShotJustHit = true,
                minDamageBlockedByPlayerArmorThisTurn = 1,
                oncePerCombatTags = ["WowArmorISPrettyCoolHuh"],
                oncePerRun = true,
                dialogue = [
                    new(AmIlleana, "Hey, less work for me!")
                ]
            }},
            {"CheapCardPlayed_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                maxCostOfCardJustPlayed = 0,
                oncePerCombatTags = ["CheapCardPlayed"],
                oncePerRun = true,
                dialogue = [
                    new(AmIlleana, "explain", "Nothing lost, many gained.")
                ]
            }},
            {"Drone_numberone_Destroyed_Illeana_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                priority = true,
                lastNamedDroneDestroyed = "numberone",
                allPresent = [ AmIlleana, AmIsaac ],
                dialogue = [
                    new(AmIlleana, "squint", "I think you jinxed it.")
                ]
            }},
            {"Duo_AboutToDieAndLoop_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmDizzy ],
                enemyShotJustHit = true,
                maxHull = 2,
                oncePerCombatTags = ["aboutToDie"],
                oncePerRun = true,
                dialogue = [
                    new(AmDizzy, "frown", "Time loop?"),
                    new(AmIlleana, "solemn", "Despite everything.")
                ]
            }},
            {"Duo_AboutToDieAndLoop_Illeana_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmPeri ],
                enemyShotJustHit = true,
                maxHull = 2,
                oncePerCombatTags = ["aboutToDie"],
                oncePerRun = true,
                dialogue = [
                    new(AmPeri, "mad", "Is that it?"),
                    new(AmIlleana, "mad", "I hope not.")
                ]
            }},
            {"Duo_AboutToDieAndLoop_Illeana_2", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmRiggs ],
                enemyShotJustHit = true,
                maxHull = 2,
                oncePerCombatTags = ["aboutToDie"],
                oncePerRun = true,
                dialogue = [
                    new(AmIlleana, "squint", "Next time, I'm taking the wheel."),
                    new(AmRiggs, "No.")
                ]
            }},
            {"Duo_AboutToDieAndLoop_Illeana_3", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmDrake ],
                enemyShotJustHit = true,
                maxHull = 2,
                oncePerCombatTags = ["aboutToDie"],
                oncePerRun = true,
                dialogue = [
                    new(AmDrake, "This is all your fault."),
                    new(AmIlleana, "tired", "...")
                ]
            }},
            {"Duo_AboutToDieAndLoop_Illeana_4", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmIsaac ],
                enemyShotJustHit = true,
                maxHull = 2,
                oncePerCombatTags = ["aboutToDie"],
                oncePerRun = true,
                dialogue = [
                    new(AmIsaac, "Dang."),
                    new(AmIlleana, "explain", "Oh well.")
                ]
            }},
            {"Duo_AboutToDieAndLoop_Illeana_5", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmBooks ],
                enemyShotJustHit = true,
                maxHull = 2,
                oncePerCombatTags = ["aboutToDie"],
                oncePerRun = true,
                dialogue = [
                    new(AmBooks, "Failure!"),
                    new(AmIlleana, "sad", "Noooooooooowuh!")
                ]
            }},
            {"Duo_AboutToDieAndLoop_Illeana_6", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmMax ],
                enemyShotJustHit = true,
                maxHull = 2,
                oncePerCombatTags = ["aboutToDie"],
                oncePerRun = true,
                dialogue = [
                    new(AmMax, "mad", "We've lost!"),
                    new(AmIlleana, "squint", "Not yet we haven't.")
                ]
            }},
            {"Duo_AboutToDieAndLoop_Illeana_7", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmCat ],
                enemyShotJustHit = true,
                maxHull = 2,
                oncePerCombatTags = ["aboutToDie"],
                oncePerRun = true,
                dialogue = [
                    new(AmCat, "grumpy", "Reset incoming."),
                    new(AmIlleana, "panic", "Not yet!")
                ]
            }},
            {"EmptyHandWithEnergy_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                handEmpty = true,
                minEnergy = 1,
                dialogue = [
                    new(AmIlleana, "curious", "That it?")
                ]
            }},
            {"EmptyHandWithEnergy_Illeana_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmDrake ],
                handEmpty = true,
                minEnergy = 1,
                dialogue = [
                    new(AmIlleana, "squint", "The one time my hands are free, there's nothing to do."),
                    new(AmDrake, "sly", "You don't have hands.")
                ]
            }},
            {"EnemyArmorHitLots_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustHit = true,
                minDamageBlockedByEnemyArmorThisTurn = 3,
                oncePerCombat = true,
                oncePerRun = true,
                dialogue = [
                    new(AmIlleana, "tired", "You know, if you're gonna be wasting resources doing dumb schenanigans, might as well give them to me for my experiments.")
                ]
            }},
            {"EnemyArmorHit_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustHit = true,
                minDamageBlockedByEnemyArmorThisTurn = 1,
                oncePerCombat = true,
                oncePerRun = true,
                dialogue = [
                    new(AmIlleana, "squint", "I thought I gave you enough fuel.")
                ]
            }},
            {"EnemyHasBrittle_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                enemyHasBrittlePart = true,
                oncePerRunTags = ["yelledAboutBrittle"],
                dialogue = [
                    new(AmIlleana, "Break them apart!")
                ]
            }},
            {"EnemyHasBrittle_Illeana_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                enemyHasBrittlePart = true,
                oncePerRunTags = ["yelledAboutBrittle"],
                dialogue = [
                    new(AmIlleana, "explain", "If only they were also tarnished. That'd mean double DOUBLE damage. Four times!")
                ]
            }},
            {"EnemyHasWeakness_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                enemyHasWeakPart = true,
                oncePerRunTags = ["yelledAboutWeakness"],
                dialogue = [
                    new(AmIlleana, "Hit the weak point!")
                ]
            }},
            {"ExpensiveCardPlayed_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                minCostOfCardJustPlayed = 4,
                oncePerCombatTags = ["ExpensiveCardPlayed"],
                oncePerRun = true,
                dialogue = [
                    new(AmIlleana, "intense", "Did anyone else's lights just flicker?")
                ]
            }},
            {"FreezeIsMaxSize_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, "crystal" ],
                turnStart = true,
                enemyIntent = "biggestCrystal",
                oncePerCombatTags = ["biggestCrystalShout"],
                dialogue = [
                    new(AmIlleana, "panic", "Okay, who let the death crystal get this big?")
                ]
            }},
            {"JustHitGeneric_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustHit = true,
                minDamageDealtToEnemyThisAction = 1,
                dialogue = [
                    new(AmIlleana, "That's a hit!")
                ]
            }},
            {"JustHitGeneric_Illeana_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustHit = true,
                minDamageDealtToEnemyThisAction = 1,
                dialogue = [
                    new(AmIlleana, "You got 'em.")
                ]
            }},
            {"JustHitGeneric_Illeana_2", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustHit = true,
                minDamageDealtToEnemyThisAction = 1,
                dialogue = [
                    new(AmIlleana, "Blam!")
                ]
            }},
            {"JustPlayedADraculaCard_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                whoDidThat = Deck.dracula,
                nonePresent = [ "dracula" ],
                dialogue = [
                    new(AmIlleana, "explain", "Now that's utility I strive for.")
                ]
            }},
            {"JustPlayedASashaCard_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                nonePresent = [ "sasha" ],
                whoDidThat = Deck.sasha,
                dialogue = [
                    new(AmIlleana, "sad", "If only I too could sports.")
                ]
            }},
            {"JustPlayedAnEphemeralCard_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                whoDidThat = Deck.ephemeral,
                priority = true,
                dialogue = [
                    new(AmIlleana, "stareatcamera", "Was it worth it?")
                ]
            }},
            {"LookOutMissile_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, AmPeri ],
                priority = true,
                once = true,
                oncePerRunTags = ["goodMissileAdvice"],
                anyDronesHostile = ["missile_normal", "missile_heavy", "missile_corrode", "missile_breacher"],
                dialogue = [
                    new(AmPeri, "mad", "Shoot it down!"),
                    new(AmIlleana, "No! Full throttle!")
                ]
            }},
            {"ManyFlips_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                minTimesYouFlippedACardThisTurn = 4,
                oncePerCombat = true,
                dialogue = [
                    new(AmIlleana, "mad", "Oh my word. Can you pick a side already?!")
                ]
            }},
            {"ManyTurns_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                minTurnsThisCombat = 9,
                oncePerCombatTags = ["manyTurns"],
                dialogue = [
                    new(AmIlleana, "explain", "Slow and steady wins the race.")
                ]
            }},
            {"ManyTurns_Illeana_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                minTurnsThisCombat = 9,
                oncePerCombatTags = ["manyTurns"],
                dialogue = [
                    new(AmIlleana, "tired", "What time is it?")
                ]
            }},
            {"OldSpikeChattyPostRenameGeorge_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, "spike" ],
                oncePerCombatTags = ["OldSpikeNewName"],
                maxTurnsThisCombat = 1,
                spikeName = "george",
                dialogue = [
                    new("spike", "George time!"),
                    new(AmIlleana, "Would've sounded better if you were Spike.")
                ]
            }},
            {"OldSpikeChattyPostRenameSpikeTwo_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana, "spike" ],
                oncePerCombatTags = ["OldSpikeNewName"],
                maxTurnsThisCombat = 1,
                spikeName = "spiketwo",
                dialogue = [
                    new("spike", "Get ready! Spike Two is here!"),
                    new(AmIlleana, "squint", "What kind of name is Spike Two? Are you a sequel or something?")
                ]
            }},
            {"OneHitPointThisIsFine_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                oncePerCombatTags = ["aboutToDie"],
                oncePerRun = true,
                enemyShotJustHit = true,
                maxHull = 1,
                dialogue = [
                    new(AmIlleana, "panic",  "We're losing hull faster than I can patch them!")
                ]
            }},
            {"OneHitPointThisIsFine_Illeana_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                oncePerCombatTags = ["aboutToDie"],
                oncePerRun = true,
                enemyShotJustHit = true,
                maxHull = 1,
                lastTurnPlayerStatuses = [Status.corrode],
                dialogue = [
                    new(AmIlleana, "panic", "Uhhh... maybe I shouldn't have experimented this much.")
                ]
            }},
            {"OverheatGeneric_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                goingToOverheat = true,
                oncePerCombatTags = ["OverheatGeneric"],
                dialogue = [
                    new(AmIlleana, "My corrosive solution has boiled away.")
                ]
            }},
            {"PlayedManyCards_Illeana_0", new(){
                type = NodeType.combat,
                handEmpty = true,
                minCardsPlayedThisTurn = 6,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(AmIlleana, "Wow! Many things done! Good job.")
                ]
            }},
            {"StrafeHit_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustHit = true,
                minDamageDealtToEnemyThisAction = 1,
                playerShotWasFromStrafe = true,
                oncePerCombat = true,
                dialogue = [
                    new(AmIlleana, "explain", "You know, I might invest in this strafe tech.")
                ]
            }},
            {"StrafeMissedGood_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustMissed = true,
                playerShotWasFromStrafe = true,
                hasArtifacts = [ "Recalibrator", "GrazerBeam"],
                oncePerCombat = true,
                dialogue = [
                    new(AmIlleana, "Nothing wasted.")
                ]
            }},
            {"TookZeroDamageAtLowHealth_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                enemyShotJustHit = true,
                maxHull = 2,
                maxDamageDealtToPlayerThisTurn = 0,
                dialogue = [
                    new(AmIlleana, "Keep 'em busy! I'm working my magic.")
                ]
            }},
            {"VeryManyTurns_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                minTurnsThisCombat = 20,
                oncePerCombatTags = ["veryManyTurns"],
                oncePerRun = true,
                turnStart = true,
                dialogue = [
                    new(AmIlleana, "tired", "Okay this is getting ridiculous.")
                ]
            }},
            {"WeGotHurtButNotTooBad_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                enemyShotJustHit = true,
                minDamageDealtToPlayerThisTurn = 1,
                maxDamageDealtToPlayerThisTurn = 1,
                dialogue = [
                    new(AmIlleana, "Totally fixable.")
                ]
            }},
            {"WeMissedOopsie_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustMissed = true,
                oncePerCombat = true,
                doesNotHaveArtifacts = ["Recalibrator", "GrazerBeam"],
                dialogue = [
                    new(AmIlleana, "explain", "Good thing I'm not the one shooting.")
                ]
            }},
            {"WeMissedOopsie_Illeana_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustMissed = true,
                oncePerCombat = true,
                doesNotHaveArtifacts = ["Recalibrator", "GrazerBeam"],
                dialogue = [
                    new(AmIlleana, "Realign and try again.")
                ]
            }},
            {"WeAreCorroded_Multi_0", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "intense", "No wait, stay! I got it.")
                ]
            }},
            {"WeAreCorroded_Multi_1", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "Hold on, I got it under control!")
                ]
            }},
            {"WeAreCorroded_Multi_2", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "sly", "We can totally fix that in the middle of a fight.")
                ]
            }},
            {"WeAreCorroded_Multi_3", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "mad", "Nuh uh.")
                ]
            }},
            {"WeAreCorroded_Multi_4", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "squint", "Hush, I'm concentrating.")
                ]
            }},
            {"WeAreCorroded_Multi_5", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "explain", "It's all part of the plan.")
                ]
            }},
            {"WeAreCorroded_Multi_6", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "mad", "I'm working on it!")
                ]
            }},
            {"WeAreCorroded_Multi_7", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "solemn", "Computer, snooze.")
                ]
            }},
            {"WeAreCorroded_Multi_8", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "curious", "Uh yes?")
                ]
            }},
            {"TheyGotCorroded_Multi_5", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "sly", "Did I do that?")
                ]
            }},
            {"ChunkThreats_Multi_3", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "mad", "It's you, the one who's living in my head rent free!")
                ]
            }},
            {"BanditThreats_Multi_0", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "panic", "Uhh I didn't order that.")
                ]
            }},
            {"CrabFacts1_Multi_0", new(){
                edit = [
                    new(EMod.countFromStart, 2, AmIlleana, "And I haven't had my breakfast today.")
                ]
            }},
            {"CrabFacts2_Multi_0", new(){
                edit = [
                    new(EMod.countFromStart, 2, AmIlleana, "salavating", "You look very delicious.")
                ]
            }},
            {"CrabFactsAreOverNow_Multi_0", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "readytoeat", "...")
                ]
            }},
            {"DillianShouts", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "solemn", "The feeling's not mutual.")
                ]
            }},
            {"DualNotEnoughDronesShouts_Multi_2", new(){
                edit = [
                    new("9b0ce906", AmIlleana, "panic", "How did you know I was a robot?")
                ]
            }},
            {"OverheatDrakeFix_Multi_6", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "squint", "Good job. Don't ever do that again."),
                    new(EMod.countFromStart, 1, AmIlleana, "solemn", "You know, I had the patchkit ready.")
                ]
            }},
            {"OverheatDrakesFault_Multi_9", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "tired", "I'll get the fire extinguisher.")
                ]
            }},
            {"RiderAvast", new(){
                edit = [
                    new(EMod.countFromStart, 2, AmIlleana, "curious", "A vest?")
                ]
            }},
            {"RiderTiderunnerShouts", new(){
                edit = [
                    new(EMod.countFromStart, 2, AmIlleana, "squint", "You're not allowed to have it.")
                ]
            }},
            {"SkunkFirstTurnShouts_Multi_0", new(){
                edit = [
                    new(EMod.countFromStart, 2, AmIlleana, "I'm not an errosion engineer you know.")
                ]
            }},
            {"SogginsEscapeIntent_1", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "tired", "Just get out of here.")
                ]
            }},
            {"SogginsEscapeIntent_3", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "giggle", "Hee hee heeeeeee.")
                ]
            }},
            {"Soggins_Missile_Shout_1", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "mad", "Shoot you with what?")
                ]
            }},
            {"SpikeGetsChatty_Multi_0", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "Here I come.")
                ]
            }},
            {"TookDamageHave2HP_Multi_1", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "mad", "I'm on it!")
                ]
            }},
            {"WeJustGainedHeatAndDrakeIsHere_Multi_0", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "mad", "You're messing up my experiments!")
                ]
            }},
            {"WeAreTarnished_Multi_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIsaac ],
                lastTurnPlayerStatuses = [ Tarnished ],
                dialogue = [
                    new(AmIsaac, "panic", "That's not good..."),
                    new(AmIlleana, "sly", "Oh relax, just don't get hit.")
                ]
            }},
            {"WeAreTarnished_Multi_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmPeri, AmIlleana ],
                lastTurnPlayerStatuses = [ Tarnished ],
                dialogue = [
                    new(AmPeri, "mad", "What do you think you're doing?!"),
                    new(AmIlleana, "silly", "My best!")
                ]
            }},
            {"WeAreTarnished_Multi_2", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmPeri ],
                lastTurnPlayerStatuses = [ Tarnished ],
                dialogue = [
                    new(AmPeri, "mad", "We can't afford to get hit now."),
                    new([
                        new(AmIlleana, "intense", "I'll throw the useless things out the airlock!"),
                        new(AmIlleana, "sly", "Then don't."),
                        new(AmIlleana, "Nah, I bet I can patch it right back up.")
                    ])
                ]
            }},
            {"WeAreTarnished_Multi_3", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmDrake ],
                lastTurnPlayerStatuses = [ Tarnished ],
                dialogue = [
                    new(AmDrake, "panic", "The heat isn't doing anything.")
                ]
            }},
            {"WeAreTarnished_Multi_4", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmDizzy ],
                lastTurnPlayerStatuses = [ Tarnished ],
                dialogue = [
                    new(AmDizzy, "squint", "The ship is falling apart.")
                ]
            }},
            {"WeAreTarnished_Multi_5", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmCat ],
                lastTurnPlayerStatuses = [ Tarnished ],
                dialogue = [
                    new(AmCat, "squint", "We need to get away NOW.")
                ]
            }},
            {"TheyGotTarnished_Multi_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                lastTurnEnemyStatuses = [ Tarnished ],
                dialogue = [
                    new(AmIlleana, "They're not taking enough damage! Get some headshots!")
                ]
            }},
            {"TheyGotTarnished_Multi_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                lastTurnEnemyStatuses = [ Tarnished ],
                dialogue = [
                    new(AmIlleana, "Their hull is weakened, blast 'em!")
                ]
            }},
            {"TheyGotTarnished_Multi_2", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmPeri ],
                lastTurnEnemyStatuses = [ Tarnished ],
                dialogue = [
                    new(AmPeri, "My turn!")
                ]
            }},
            {"IlleanaHatesChunk_Multi_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana, "chunk" ],
                lastTurnEnemyStatuses = [ Status.corrode ],
                minTurnsThisCombat = 8,
                dialogue = [
                    new(AmIlleana, "solemn", "Good riddance.")
                ]
            }},
            {"IlleanaWentMissing_Multi_0", new(){
                type = NodeType.combat,
                allPresent = [ AmPeri ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["illeanaWentMissing"],
                lastTurnPlayerStatuses = [MissingIlleana],
                dialogue = [
                    new(AmPeri, "mad", "Hey, give us back our crew!")
                ]
            }},
            {"IlleanaWentMissing_Multi_1", new(){
                type = NodeType.combat,
                allPresent = [ AmRiggs ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["illeanaWentMissing"],
                lastTurnPlayerStatuses = [MissingIlleana],
                dialogue = [
                    new(AmRiggs, "nervous", "Where did the space snake go?")
                ]
            }},
            {"IlleanaWentMissing_Multi_2", new(){
                type = NodeType.combat,
                allPresent = [ AmDizzy ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["illeanaWentMissing"],
                lastTurnPlayerStatuses = [MissingIlleana],
                dialogue = [
                    new(AmDizzy, "intense", "Illeana!")
                ]
            }},
            {"IlleanaWentMissing_Multi_3", new(){
                type = NodeType.combat,
                allPresent = [ AmCat ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["illeanaWentMissing"],
                lastTurnPlayerStatuses = [MissingIlleana],
                dialogue = [
                    new(AmCat, "That's not normal.")
                ]
            }},
            {"IlleanaWentMissing_Multi_4", new(){
                type = NodeType.combat,
                allPresent = [ AmIsaac ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["illeanaWentMissing"],
                lastTurnPlayerStatuses = [MissingIlleana],
                dialogue = [
                    new(AmIsaac, "Ummm...")
                ]
            }},
            {"IlleanaWentMissing_Multi_5", new(){
                type = NodeType.combat,
                allPresent = [ AmDrake ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["illeanaWentMissing"],
                lastTurnPlayerStatuses = [MissingIlleana],
                dialogue = [
                    new(AmDrake, "Hey, I was kidding about turning you into wine. Illeana?")
                ]
            }},
            {"IlleanaWentMissing_Multi_6", new(){
                type = NodeType.combat,
                allPresent = [ AmMax ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["illeanaWentMissing"],
                lastTurnPlayerStatuses = [MissingIlleana],
                dialogue = [
                    new(AmMax, "Woah.")
                ]
            }},
            {"IlleanaWentMissing_Multi_7", new(){
                type = NodeType.combat,
                allPresent = [ AmBooks ],
                priority = true,
                oncePerRun = true,
                oncePerCombatTags = ["illeanaWentMissing"],
                lastTurnPlayerStatuses = [MissingIlleana],
                dialogue = [
                    new(AmBooks, "Snake lady?")
                ]
            }},
            {"IlleanaJustHit_Multi_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustHit = true,
                minDamageDealtToEnemyThisAction = 1,
                whoDidThat = AmIlleanaDeck,
                oncePerCombatTags = [ "IlleanaShotAGuy"],
                dialogue = [
                    new(AmIlleana, "shocked", "Woah! I didn't know I had it in me!")
                ]
            }},
            {"IlleanaJustHit_Multi_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustHit = true,
                minDamageDealtToEnemyThisAction = 1,
                whoDidThat = AmIlleanaDeck,
                oncePerCombatTags = [ "IlleanaShotAGuy"],
                dialogue = [
                    new(AmIlleana, "solemn", "Aw man, I'm probably getting my certification revoked for this.")
                ]
            }},
            {"IlleanaJustHit_Multi_2", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                playerShotJustHit = true,
                minDamageDealtToEnemyThisAction = 1,
                whoDidThat = AmIlleanaDeck,
                oncePerCombatTags = [ "IlleanaShotAGuy"],
                dialogue = [
                    new(AmIlleana, "explain", "At least I'm not a doctor. Imagine signing a hypocratic oath.")
                ]
            }},
            {"IlleanaGotPerfect_Multi_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                oncePerRun = true,
                lastTurnPlayerStatuses = [Status.perfectShield],
                dialogue = [
                    new(AmIlleana, "explain", "Thanks to this new thing, we can safely do reckless behavior."),
                    new([
                        new(AmDizzy, "squint", "I don't think you were the one to come up with this."),
                        new(AmPeri, "mad", "Don't."),
                        new(AmDrake, "Turning up the heat! Don't complain!")
                    ])
                ]
            }},
            {"IlleanaGotBoots_Multi_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                oncePerRun = true,
                lastTurnPlayerStatuses = [Status.hermes],
                dialogue = [
                    new(AmIlleana, "Boosters boosted!")
                ]
            }},
            {"IlleanaGotBoots_Multi_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                oncePerRun = true,
                lastTurnPlayerStatuses = [Status.hermes],
                dialogue = [
                    new(AmIlleana, "Engines boosted, full throttle!")
                ]
            }},

            // {"", new(){

            //     dialogue = [

            //     ]
            // }},
        });
        LocalDB.DumpStoryToLocalLocale("en", "TheJazMaster.EnemyPack", new Dictionary<string, DialogueMachine>(){
            {"EnemyPack_GooseEscape_Illeana_0", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                enemyIntent = "gooseEscape",
                turnStart = true,
                dialogue = [
                    new("Goose", "Honk!"),
                    new(AmIlleana, "mad", "It's getting away!")
                ]
            }},
            {"EnemyPack_GooseEscape_Illeana_1", new(){
                type = NodeType.combat,
                allPresent = [ AmIlleana ],
                enemyIntent = "gooseEscape",
                turnStart = true,
                dialogue = [
                    new("Goose", "Honk!"),
                    new(AmIlleana, "sad", "No... I wanted turkey for dinner...")
                ]
            }},
        });

        LocalDB.DumpStoryToLocalLocale("en", "urufudoggo.Weth", new Dictionary<string, DialogueMachine>()
        {
            {"JustPlayedASashaCard_Weth_0", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "mad", "In front of me?!")
                ]
            }},
        });
    }
}