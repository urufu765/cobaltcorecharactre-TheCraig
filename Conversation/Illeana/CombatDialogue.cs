using System;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static class CombatDialogue
{
    internal static void Inject()
    {
        try
        {
            DB.story.all["WeAreCorroded_Multi_0"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "No wait, stay! I got it.",
                loopTag = "intense".Check()
            });
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit WeAreCorroded_Multi_0");
        }
        try
        {
            DB.story.all["WeAreCorroded_Multi_1"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "Hold on, I got it under control!",
                loopTag = "neutral".Check()
            });
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit WeAreCorroded_Multi_1");
        }
        try
        {
            DB.story.all["WeAreCorroded_Multi_2"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "We can totally fix that in the middle of a fight.",
                loopTag = "sly".Check()
            });
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit WeAreCorroded_Multi_2");
        }
        try
        {
            DB.story.all["WeAreCorroded_Multi_3"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "Nuh uh.",
                loopTag = "mad".Check()
            });
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit WeAreCorroded_Multi_3");
        }
        try
        {
            DB.story.all["WeAreCorroded_Multi_4"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "Hush, I'm concentrating.",
                loopTag = "squint".Check()
            });
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit WeAreCorroded_Multi_4");
        }
        try
        {
            DB.story.all["WeAreCorroded_Multi_5"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "It's all part of the plan.",
                loopTag = "explain".Check()
            });
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit WeAreCorroded_Multi_5");
        }
        try
        {
            DB.story.all["WeAreCorroded_Multi_6"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "I'm working on it!",
                loopTag = "mad".Check()
            });
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit WeAreCorroded_Multi_6");
        }
        try
        {
            DB.story.all["WeAreCorroded_Multi_7"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "Computer, snooze.",
                loopTag = "solemn".Check()
            });        
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit WeAreCorroded_Multi_7");
        }
        try
        {
            DB.story.all["WeAreCorroded_Multi_8"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "Uh yes?",
                loopTag = "curious".Check()
            });
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit WeAreCorroded_Multi_8");
        }
        try
        {
            DB.story.all["TheyGotCorroded_Multi_5"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "Did I do that?",
                loopTag = "sly".Check()
            });
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit TheyGotCorroded_Multi_5");
        }
        try
        {
            DB.story.all["ChunkThreats_Multi_3"].lines.Add(new CustomSay()
            {
                who = AmIlleana,
                what = "It's you, the one who's living in my head rent free!",
                loopTag = "mad".Check()
            });
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to edit ChunkThreats_Multi_3");
        }
        try
        {
            foreach(Instruction i in DB.story.all["BanditThreats_Illeana_0"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "panic".Check(),
                            what = "Uhh I didn't order that."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to BanditThreats_Illeana_0");
        }
        try
        {
            bool skip1 = false;
            foreach(Instruction i in DB.story.all["CrabFacts1_Multi_0"].lines)
            {
                if (i is SaySwitch ss)
                {
                    if (skip1)
                    {
                        ss.lines.Add(
                            new CustomSay
                            {
                                who = AmIlleana,
                                loopTag = "neutral".Check(),
                                what = "And you look delicious."
                            }
                        );
                        break;
                    }
                    skip1 = true;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to CrabFacts1_Multi_0");
        }
        try
        {
            bool skip1 = false;
            foreach(Instruction i in DB.story.all["CrabFacts2_Multi_0"].lines)
            {
                if (i is SaySwitch ss)
                {
                    if (skip1)
                    {
                        ss.lines.Add(
                            new CustomSay
                            {
                                who = AmIlleana,
                                loopTag = "salavating".Check(),
                                what = "..."
                            }
                        );
                        break;
                    }
                    skip1 = true;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to CrabFacts2_Multi_0");
        }
        try
        {
            bool skip1 = false;
            foreach(Instruction i in DB.story.all["CrabFactsAreOverNow_Multi_0"].lines)
            {
                if (i is SaySwitch ss)
                {
                    if (skip1)
                    {
                        ss.lines.Add(
                            new CustomSay
                            {
                                who = AmIlleana,
                                loopTag = "readytoeat".Check(),
                                what = "..."
                            }
                        );
                        break;
                    }
                    skip1 = true;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to CrabFactsAreOverNow_Multi_0");
        }


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
                    loopTag = "desperate".Check()
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
        DB.story.all["WeAreTarnished_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIssac ],
            lastTurnPlayerStatuses = [ Tarnished ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIssac,
                    what = "That's not good...",
                    loopTag = "panic"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Oh relax, just don't get hit.",
                    loopTag = "sly".Check()
                }
            }
        };
        DB.story.all["WeAreTarnished_Multi_1"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmPeri, AmIlleana ],
            lastTurnPlayerStatuses = [ Tarnished ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmPeri,
                    what = "What do you think you're doing?!",
                    loopTag = "mad"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "My best!",
                    loopTag = "silly".Check()
                }
            }
        };
        DB.story.all["WeAreTarnished_Multi_2"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmPeri ],
            lastTurnPlayerStatuses = [ Tarnished ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmPeri,
                    what = "We can't afford to get hit now.",
                    loopTag = "mad"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "I'll throw the useless things out the airlock!",
                    loopTag = "intense".Check()
                }
            }
        };
        DB.story.all["WeAreTarnished_Multi_3"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmDrake ],
            lastTurnPlayerStatuses = [ Tarnished ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmDrake,
                    what = "The heat isn't doing anything.",
                    loopTag = "panic"
                }
            }
        };
        DB.story.all["WeAreTarnished_Multi_4"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmDizzy ],
            lastTurnPlayerStatuses = [ Tarnished ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmDizzy,
                    what = "The ship is falling apart.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all["WeAreTarnished_Multi_5"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmCat ],
            lastTurnPlayerStatuses = [ Tarnished ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmCat,
                    what = "We need to get away NOW.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all["TheyGotTarnished_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lastTurnEnemyStatuses = [ Tarnished ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "They're not taking enough damage! Get some headshots!",
                    loopTag = "neutral".Check()
                }
            }
        };
        DB.story.all["TheyGotTarnished_Multi_1"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lastTurnEnemyStatuses = [ Tarnished ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Their hull is weakened, blast them!",
                    loopTag = "neutral".Check()
                }
            }
        };
        DB.story.all["TheyGotTarnished_Multi_2"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmPeri ],
            lastTurnEnemyStatuses = [ Tarnished ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmPeri,
                    what = "It's my turn now.",
                    loopTag = "squint"
                }
            }
        };
        DB.story.all["TheyGotTarnished_Multi_3"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIssac ],
            lastTurnEnemyStatuses = [ Tarnished ],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIssac,
                    what = "My drones will take care of them now."
                }
            }
        };
        DB.story.all["IlleanaHatesChunk_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana, "chunk" ],
            lastTurnEnemyStatuses = [ Status.corrode ],
            minTurnsThisCombat = 8,
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "solemn".Check(),
                    what = "Good riddance."
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
        DB.story.all["IlleanaWentMissing_Multi_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmPeri ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["illeanaWentMissing"],
            lastTurnPlayerStatuses = [MissingIlleana],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmPeri,
                    loopTag = "mad",
                    what = "Hey, give us back our crew!"
                }
            }
        };
        DB.story.all["IlleanaWentMissing_Multi_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmRiggs ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["illeanaWentMissing"],
            lastTurnPlayerStatuses = [MissingIlleana],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmRiggs,
                    loopTag = "nervous",
                    what = "Snake lady?"
                }
            }
        };
        DB.story.all["IlleanaWentMissing_Multi_2"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmDizzy ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["illeanaWentMissing"],
            lastTurnPlayerStatuses = [MissingIlleana],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmDizzy,
                    loopTag = "intense",
                    what = "Illeana!"
                }
            }
        };
        DB.story.all["IlleanaWentMissing_Multi_3"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmCat ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["illeanaWentMissing"],
            lastTurnPlayerStatuses = [MissingIlleana],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmCat,
                    what = "That's not normal."
                }
            }
        };
        DB.story.all["IlleanaWentMissing_Multi_4"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIssac ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["illeanaWentMissing"],
            lastTurnPlayerStatuses = [MissingIlleana],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIssac,
                    what = "Um..."
                }
            }
        };
        DB.story.all["IlleanaWentMissing_Multi_5"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmDrake ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["illeanaWentMissing"],
            lastTurnPlayerStatuses = [MissingIlleana],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmDrake,
                    what = "Hey, I was kidding about turning you into wine. Illeana?"
                }
            }
        };
        DB.story.all["IlleanaWentMissing_Multi_6"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmMax ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["illeanaWentMissing"],
            lastTurnPlayerStatuses = [MissingIlleana],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmMax,
                    what = "Woah."
                }
            }
        };
        DB.story.all["IlleanaWentMissing_Multi_7"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmBooks ],
            priority = true,
            oncePerRun = true,
            oncePerCombatTags = ["illeanaWentMissing"],
            lastTurnPlayerStatuses = [MissingIlleana],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmBooks,
                    what = "Where did the space snake go?"
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
        DB.story.all["IlleanaJustHit_Multi_0"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            whoDidThat = AmIlleanaDeck,
            oncePerCombatTags = [ "IlleanaShotAGuy"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "shocked".Check(),
                    what = "Woah! I didn't know I had it in me!"
                }
            }
        };
        DB.story.all["IlleanaJustHit_Multi_1"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            whoDidThat = AmIlleanaDeck,
            oncePerCombatTags = [ "IlleanaShotAGuy"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "solemn".Check(),
                    what = "Aw man, I'm probably getting my certification revoked for this."
                }
            }
        };
        DB.story.all["IlleanaJustHit_Multi_2"] = new()
        {
            type = NodeType.combat,
            allPresent = [ AmIlleana ],
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 1,
            whoDidThat = AmIlleanaDeck,
            oncePerCombatTags = [ "IlleanaShotAGuy"],
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    loopTag = "explain".Check(),
                    what = "At least I'm not a doctor. Imagine signing a hypocratic oath."
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
        DB.story.all[""] = new()
        {
            type = NodeType.combat,
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
    }

    internal static void ModdedInject()
    {
        try
        {
            if (ModEntry.Patch_EnemyPack)
            {
                DB.story.all["EnemyPack_GooseEscape_Illeana_0"]
                = new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmIlleana ],
                    enemyIntent = "gooseEscape",
                    turnStart = true,
                    lines = [
                        new CustomSay()
                        {
                            who = "Goose",
                            what = "Honk!"
                        },
                        new CustomSay()
                        {
                            who = AmIlleana,
                            what = "It's getting away!",
                            loopTag = "mad".Check()
                        }
                    ]
                };                
                DB.story.all["EnemyPack_GooseEscape_Illeana_1"]
                = new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmIlleana ],
                    enemyIntent = "gooseEscape",
                    turnStart = true,
                    lines = [
                        new CustomSay()
                        {
                            who = "Goose",
                            what = "Honk!"
                        },
                        new CustomSay()
                        {
                            who = AmIlleana,
                            what = "No... I wanted goose for dinner...",
                            loopTag = "sad".Check()
                        }
                    ]
                };
            }
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "FUCK, couldn't add lines");
        }
    }
}


