using System;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static partial class CombatDialogue
{
    private static void IlleanaCombat()
    {
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

    }
}
