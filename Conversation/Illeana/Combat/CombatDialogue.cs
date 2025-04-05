using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static partial class CombatDialogue
{
    internal static void Inject()
    {
        Replies();
        ModdedInject();
        MainExtensions();
        IlleanaCombat();
    }


    private static void MainExtensions()
    {
        DB.story.all["ThatsALotOfDamageToUs_Illeana_0"] = new()
        {
            type = NodeType.combat,
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 3,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Too much damage! Too much damage!",
                    loopTag = "panic".Check()
                }
            }
        };
        DB.story.all["ThatsALotOfDamageToUs_Illeana_1"] = new()
        {
            type = NodeType.combat,
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 3,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "That's too big of a hole to patch, even for me.",
                    loopTag = "shocked".Check()
                }
            }
        };        
        DB.story.all["ThatsALotOfDamageToUs_Illeana_2"] = new()
        {
            type = NodeType.combat,
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 3,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "I can fix it... I can fix it...",
                    loopTag = "intense".Check()
                }
            }
        };
        DB.story.all["ThatsALotOfDamageToThem_Illeana_0"] = new()
        {
            type = NodeType.combat,
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 10,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "That's a lot of damage!",
                    loopTag = "shocked".Check()
                }
            }
        };        
        DB.story.all["ThatsALotOfDamageToThem_Illeana_1"] = new()
        {
            type = NodeType.combat,
            playerShotJustHit = true,
            minDamageDealtToEnemyThisTurn = 10,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Booyah!",
                    loopTag = "silly".Check()
                }
            }
        };
        DB.story.all["WeGotShotButTookNoDamage_Illeana_0"] = new()
        {
            type = NodeType.combat,
            enemyShotJustHit = true,
            maxDamageDealtToPlayerThisTurn = 0,
            lastTurnPlayerStatuses = [Status.perfectShield],
            
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "The results of my constant experimentions. Behold, perfection.",
                    loopTag = "explain".Check()
                }
            }
        };
        DB.story.all["WeGotShotButTookNoDamage_Illeana_1"] = new()
        {
            type = NodeType.combat,
            enemyShotJustHit = true,
            maxDamageDealtToPlayerThisTurn = 0,
            lastTurnPlayerStatuses = [Status.perfectShield],
            
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "See? All that hull perforation wasn't in vain.",
                    loopTag = "explain".Check()
                }
            }
        };
        DB.story.all["WeGotShotButTookNoDamage_Illeana_2"] = new()
        {
            type = NodeType.combat,
            enemyShotJustHit = true,
            maxDamageDealtToPlayerThisTurn = 0,
            lastTurnPlayerStatuses = [Status.perfectShield],
            
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "That could've been really bad... if you didn't believe in my research.",
                    loopTag = "explain".Check()
                }
            }
        };
        DB.story.all["WeAreMovingAroundALot_Illeana_0"] = new()
        {
            type = NodeType.combat,
            minMovesThisTurn = 3,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Dodge and weave! Dodge and weave!",
                    loopTag = "shocked".Check()
                }
            }
        };        
        DB.story.all["WeAreMovingAroundALot_Illeana_1"] = new()
        {
            type = NodeType.combat,
            minMovesThisTurn = 3,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "The best form of defence is running away... wait no I meant movement.",
                    loopTag = "explain".Check()
                }
            }
        };
        DB.story.all["ShopFightBackOut_Yes_Illeana_0"] = new()
        {
            type = NodeType.combat,
            minMovesThisTurn = 1,
            turnStart = true,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "shopFightBackOut_Yes" ],
            oncePerRunTags = [ "illeanaDidNotWantThisFight" ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Who said yes? WHO SAID YES?!",
                    loopTag = "panic".Check()
                }
            }
        };
        DB.story.all["ShopFightBackOut_Yes_Illeana_1"] = new()
        {
            type = NodeType.combat,
            minMovesThisTurn = 1,
            turnStart = true,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "shopFightBackOut_Yes" ],
            oncePerRunTags = [ "illeanaDidNotWantThisFight" ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "I'm so sorry, my crewmates are idiots! Please forgive us!",
                    loopTag = "panic".Check()
                },
                new CustomSay()
                {
                    who = AmShopkeeper,
                    what = "Nope.",
                }
            }
        };
        DB.story.all["HandOnlyHasTrashCards_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            handFullOfTrash = true,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "The trash is overflowing into my workspace!"
                }
            }
        };
        DB.story.all["HandOnlyHasUnplayableCards_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            handFullOfUnplayableCards = true,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "I can't do anything with this."
                }
            }
        };
        DB.story.all["BooksWentMissing_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["booksWentMissing"],
            lastTurnPlayerStatuses = [Status.missingBooks],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "Hey, where'd Books go?"
                }
            }
        };
        DB.story.all["CatWentMissing_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["CatWentMissing"],
            lastTurnPlayerStatuses = [Status.missingCat],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "Uhh maybe if I upload myself to the computer..."
                }
            }
        };
        DB.story.all["DizzyWentMissing_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["dizzyWentMissing"],
            lastTurnPlayerStatuses = [Status.missingDizzy],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "intense".Check(),
                    what = "Oh no."
                }
            }
        };
        DB.story.all["DrakeWentMissing_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["drakeWentMissing"],
            lastTurnPlayerStatuses = [Status.missingDrake],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "intense".Check(),
                    what = "Why does it suddenly feel so empty?"
                }
            }
        };
        DB.story.all["IssacWentMissing_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["issacWentMissing"],
            lastTurnPlayerStatuses = [Status.missingIsaac],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "Ah."
                }
            }
        };
        DB.story.all["MaxWentMissing_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["maxWentMissing"],
            lastTurnPlayerStatuses = [Status.missingMax],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "The computer guy!"
                }
            }
        };
        DB.story.all["PeriWentMissing_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["periWentMissing"],
            lastTurnPlayerStatuses = [Status.missingPeri],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "Wait no I already miss her!"
                }
            }
        };
        DB.story.all["RiggsWentMissing_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["riggsWentMissing"],
            lastTurnPlayerStatuses = [Status.missingRiggs],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "BUDDY NO!!"
                }
            }
        };
        DB.story.all["WeDontOverlapWithEnemyAtAll_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            priority = true,
            shipsDontOverlapAtAll = true,
            nonePresent = [ "crab", "scrap" ],
            oncePerRun = true,
            oncePerRunTags = [ "NoOverlapBetweenShips" ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "neutral".Check(),
                    what = "Gone. Goodbye!"
                }
            }
        };
        DB.story.all["WeDontOverlapWithEnemyAtAllButWeDoHaveASeekerToDealWith_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            priority = true,
            shipsDontOverlapAtAll = true,
            oncePerCombatTags = [ "NoOverlapBetweenShipsSeeker"],
            anyDronesHostile = [ "missile_seeker" ],
            nonePresent = [ "crab" ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "What's the point of evasive maneuvers if we're gonna get hit anyways?"
                }
            }
        };
        DB.story.all["BlockedALotOfAttackWithArmor_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            enemyShotJustHit = true,
            minDamageBlockedByPlayerArmorThisTurn = 3,
            oncePerCombatTags = ["YowzaThatWasALOTofArmorBlock"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "It would've been better if we just avoided getting hit in the first place."
                }
            }
        };
        DB.story.all["BlockedAnEnemyAttackWithArmor_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            enemyShotJustHit = true,
            minDamageBlockedByPlayerArmorThisTurn = 1,
            oncePerCombatTags = ["WowArmorISPrettyCoolHuh"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Hey, less work for me!"
                }
            }
        };
        DB.story.all["CheapCardPlayed_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            maxCostOfCardJustPlayed = 0,
            oncePerCombatTags = ["CheapCardPlayed"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "Nothing lost, many gained."
                }
            }
        };
        DB.story.all["Drone_numberone_Destroyed_Illeana_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            priority = true,
            lastNamedDroneDestroyed = "numberone",
            allPresent = [ AmIlleana, AmIssac ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "I think you jinxed it."
                }
            }
        };
        DB.story.all["Duo_AboutToDieAndLoop_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmDizzy ],
            enemyShotJustHit = true,
            maxHull = 2,
            oncePerCombatTags = ["aboutToDie"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmDizzy,
                    loopTag = "frown",
                    what = "Time loop?"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "solemn".Check(),
                    what = "Despite everything."
                }
            }
        };
        DB.story.all["Duo_AboutToDieAndLoop_Illeana_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmPeri ],
            enemyShotJustHit = true,
            maxHull = 2,
            oncePerCombatTags = ["aboutToDie"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmPeri,
                    loopTag = "mad",
                    what = "Is that it?"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "I hope not."
                }
            }
        };
        DB.story.all["Duo_AboutToDieAndLoop_Illeana_2"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmRiggs ],
            enemyShotJustHit = true,
            maxHull = 2,
            oncePerCombatTags = ["aboutToDie"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "Next time, I'm taking the wheel."
                },
                new CustomSay()
                {
                    who = AmRiggs,
                    what = "No."
                }
            }
        };
        DB.story.all["Duo_AboutToDieAndLoop_Illeana_3"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmDrake ],
            enemyShotJustHit = true,
            maxHull = 2,
            oncePerCombatTags = ["aboutToDie"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmDrake,
                    what = "This is all your fault."
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "sickofyoshit".Check(),
                    what = "..."
                }
            }
        };
        DB.story.all["Duo_AboutToDieAndLoop_Illeana_4"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmIssac ],
            enemyShotJustHit = true,
            maxHull = 2,
            oncePerCombatTags = ["aboutToDie"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIssac,
                    what = "Dang."
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "Oh well."
                }
            }
        };
        DB.story.all["Duo_AboutToDieAndLoop_Illeana_5"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmBooks ],
            enemyShotJustHit = true,
            maxHull = 2,
            oncePerCombatTags = ["aboutToDie"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmBooks,
                    what = "Failure!"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "sickofyoshit".Check(),
                    what = "..."
                }
            }
        };
        DB.story.all["Duo_AboutToDieAndLoop_Illeana_6"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmMax ],
            enemyShotJustHit = true,
            maxHull = 2,
            oncePerCombatTags = ["aboutToDie"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmMax,
                    loopTag = "frown",
                    what = "We've lost!"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "Not yet we haven't."
                }
            }
        };
        DB.story.all["Duo_AboutToDieAndLoop_Illeana_7"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmCat ],
            enemyShotJustHit = true,
            maxHull = 2,
            oncePerCombatTags = ["aboutToDie"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmCat,
                    loopTag = "grumpy",
                    what = "Reset incoming."
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "Not yet!"
                }
            }
        };
        DB.story.all["EmptyHandWithEnergy_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            handEmpty = true,
            minEnergy = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "curious".Check(),
                    what = "That it?"
                }
            }
        };
        DB.story.all["EmptyHandWithEnergy_Illeana_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmDrake ],
            handEmpty = true,
            minEnergy = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "The one time my hands are free, there's nothing to do."
                },
                new CustomSay()
                {
                    who = AmDrake,
                    loopTag = "sly",
                    what = "You don't have hands."
                }
            }
        };
        DB.story.all["EnemyArmorHitLots_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustHit = true,
            minDamageBlockedByEnemyArmorThisTurn = 3,
            oncePerCombat = true,
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "tired".Check(),
                    what = "You know, if you're gonna be wasting resources doing dumb schenanigans, might as well give them to me for my experiments."
                }
            }
        };
        DB.story.all["EnemyArmorHit_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustHit = true,
            minDamageBlockedByEnemyArmorThisTurn = 1,
            oncePerCombat = true,
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "I thought I gave you enough fuel."
                }
            }
        };
        DB.story.all["EnemyHasBrittle_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            enemyHasBrittlePart = true,
            oncePerRunTags = ["yelledAboutBrittle"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "neutral".Check(),
                    what = "Break them apart!"
                }
            }
        };
        DB.story.all["EnemyHasBrittle_Illeana_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            enemyHasBrittlePart = true,
            oncePerRunTags = ["yelledAboutBrittle"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "If only they were also tarnished. That'd mean double DOUBLE damage. Four times!"
                }
            }
        };
        DB.story.all["EnemyHasWeakness_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            enemyHasWeakPart = true,
            oncePerRunTags = ["yelledAboutWeakness"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "neutral".Check(),
                    what = "Hit the weak point!"
                }
            }
        };
        DB.story.all["ExpensiveCardPlayed_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            minCostOfCardJustPlayed = 4,
            oncePerCombatTags = ["ExpensiveCardPlayed"],
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "intense".Check(),
                    what = "Did anyone else's lights just flicker?"
                }
            }
        };
        DB.story.all["FreezeIsMaxSize_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, "crystal" ],
            turnStart = true,
            enemyIntent = "biggestCrystal",
            oncePerCombatTags = ["biggestCrystalShout"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "Okay, who let the death crystal get this big?"
                }
            }
        };
        DB.story.all["JustHitGeneric_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "That's a hit!"
                }
            }
        };
        DB.story.all["JustHitGeneric_Illeana_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "You got 'em."
                }
            }
        };
        DB.story.all["JustHitGeneric_Illeana_2"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Bam."
                }
            }
        };
        DB.story.all["JustPlayedADraculaCard_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            whoDidThat = Deck.dracula,
            nonePresent = [ "dracula" ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "Now that's utility I strive for."
                }
            }
        };
        DB.story.all["JustPlayedASashaCard_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            nonePresent = [ "sasha" ],
            whoDidThat = Deck.sasha,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "sad".Check(),
                    what = "If only I too could sports."
                }
            }
        };
        DB.story.all["JustPlayedAnEphemeralCard_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            whoDidThat = Deck.ephemeral,
            priority = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "stareatcamera".Check(),
                    what = "Was it worth it?"
                }
            }
        };
        DB.story.all["LookOutMissile_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, AmPeri ],
            priority = true,
            once = true,
            oncePerRunTags = ["goodMissileAdvice"],
            anyDronesHostile = ["missile_normal", "missile_heavy", "missile_corrode", "missile_breacher"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmPeri,
                    loopTag = "mad",
                    what = "Shoot it down!"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "neutral".Check(),
                    what = "No! Full throttle!"
                }
            }
        };
        DB.story.all["ManyFlips_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            minTimesYouFlippedACardThisTurn = 4,
            oncePerCombat = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "mad".Check(),
                    what = "Oh my word. Can you pick a side already?!"
                }
            }
        };
        DB.story.all["ManyTurns_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            minTurnsThisCombat = 9,
            oncePerCombatTags = ["manyTurns"],

            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "Slow and steady wins the race."
                }
            }
        };
        DB.story.all["ManyTurns_Illeana_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            minTurnsThisCombat = 9,
            oncePerCombatTags = ["manyTurns"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "tired".Check(),
                    what = "What time is it?"
                }
            }
        };
        DB.story.all["OldSpikeChattyPostRenameGeorge_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, "spike" ],
            oncePerCombatTags = ["OldSpikeNewName"],
            maxTurnsThisCombat = 1,
            spikeName = "george",
            lines = new()
            {
                new CustomSay()
                {
                    who = "spike",
                    what = "George time!"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Hmm, would've sounded better if you were Spike"
                }
            }
        };
        DB.story.all["OldSpikeChattyPostRenameSpikeTwo_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana, "spike" ],
            oncePerCombatTags = ["OldSpikeNewName"],
            maxTurnsThisCombat = 1,
            spikeName = "spiketwo",
            lines = new()
            {
                new CustomSay()
                {
                    who = "spike",
                    what = "Get ready! Spike Two is here!"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "What kind of name is Spike Two? Are you a sequel or something?"
                }
            }
        };
        DB.story.all["OneHitPointThisIsFine_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            oncePerCombatTags = ["aboutToDie"],
            oncePerRun = true,
            enemyShotJustHit = true,
            maxHull = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "We're losing hull faster than I can patch them!"
                }
            }
        };
        DB.story.all["OneHitPointThisIsFine_Illeana_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            oncePerCombatTags = ["aboutToDie"],
            oncePerRun = true,
            enemyShotJustHit = true,
            maxHull = 1,
            lastTurnPlayerStatuses = [Status.corrode],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "Uhhh... maybe I shouldn't have experimented this much."
                }
            }
        };
        DB.story.all["OverheatGeneric_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            goingToOverheat = true,
            oncePerCombatTags = ["OverheatGeneric"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "neutral".Check(),
                    what = "My corrosive solution has boiled away."
                }
            }
        };
        DB.story.all["PlayedManyCards_Illeana_0"] = new()
        {
            type = NodeType.combat,
            handEmpty = true,
            minCardsPlayedThisTurn = 6,
            allPresent = [ AmIlleana ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Got many things done! Good job."
                }
            }
        };
        DB.story.all["StrafeHit_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            playerShotWasFromStrafe = true,
            oncePerCombat = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "You know, I might invest in this strafe tech."
                }
            }
        };
        DB.story.all["StrafeMissedGood_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustMissed = true,
            playerShotWasFromStrafe = true,
            hasArtifacts = [ "Recalibrator", "GrazerBeam"],
            oncePerCombat = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Nothing wasted."
                }
            }
        };
        DB.story.all["TookZeroDamageAtLowHealth_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            enemyShotJustHit = true,
            maxHull = 2,
            maxDamageDealtToPlayerThisTurn = 0,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Keep them at bay while I work my magic."
                }
            }
        };
        DB.story.all["VeryManyTurns_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            minTurnsThisCombat = 20,
            oncePerCombatTags = ["veryManyTurns"],
            oncePerRun = true,
            turnStart = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "tired".Check(),
                    what = "Okay this is getting ridiculous."
                }
            }
        };
        DB.story.all["WeGotHurtButNotTooBad_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 1,
            maxDamageDealtToPlayerThisTurn = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Totally fixable."
                }
            }
        };
        DB.story.all["WeMissedOopsie_Illeana_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustMissed = true,
            oncePerCombat = true,
            doesNotHaveArtifacts = ["Recalibrator", "GrazerBeam"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "Good thing I'm not the one shooting."
                }
            }
        };
        DB.story.all["WeMissedOopsie_Illeana_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustMissed = true,
            oncePerCombat = true,
            doesNotHaveArtifacts = ["Recalibrator", "GrazerBeam"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Realign and try again."
                }
            }
        };
    }
}

