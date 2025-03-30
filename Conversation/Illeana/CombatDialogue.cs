using System;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static class CombatDialogue
{
    internal static void Inject()
    {
        DB.story.all[$"WeAreCorroded_Multi_0"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "No wait, stay! I got it.",
            loopTag = "intense".Check()
        });
        DB.story.all[$"WeAreCorroded_Multi_1"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Hold on, I got it under control!",
            loopTag = "neutral".Check()
        });
        DB.story.all[$"WeAreCorroded_Multi_2"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "We can totally fix that in the middle of a fight.",
            loopTag = "sly".Check()
        });
        DB.story.all[$"WeAreCorroded_Multi_3"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Nuh uh.",
            loopTag = "mad".Check()
        });
        DB.story.all[$"WeAreCorroded_Multi_4"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Hush, I'm concentrating.",
            loopTag = "squint".Check()
        });
        DB.story.all[$"WeAreCorroded_Multi_5"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "It's all part of the plan.",
            loopTag = "explain".Check()
        });
        DB.story.all[$"WeAreCorroded_Multi_6"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "I'm working on it!",
            loopTag = "mad".Check()
        });
        DB.story.all[$"WeAreCorroded_Multi_7"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Computer, snooze.",
            loopTag = "solemn".Check()
        });        
        DB.story.all[$"WeAreCorroded_Multi_8"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Uh yes?",
            loopTag = "curious".Check()
        });
        DB.story.all[$"TheyGotCorroded_Multi_5"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Did I do that?",
            loopTag = "sly".Check()
        });
        DB.story.all[$"ThatsALotOfDamageToUs_{AmIlleana}_0"] = new()
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
                    Text = "Too much damage! Too much damage!",
                    loopTag = "panic".Check()
                }
            }
        };
        DB.story.all[$"ThatsALotOfDamageToUs_{AmIlleana}_1"] = new()
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
                    Text = "That's too big of a hole to patch, even for me.",
                    loopTag = "shocked".Check()
                }
            }
        };        
        DB.story.all[$"ThatsALotOfDamageToUs_{AmIlleana}_2"] = new()
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
                    Text = "I can fix it... I can fix it...",
                    loopTag = "desperate".Check()
                }
            }
        };
        DB.story.all[$"ThatsALotOfDamageToThem_{AmIlleana}_0"] = new()
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
                    Text = "That's a lot of damage!",
                    loopTag = "shocked".Check()
                }
            }
        };        
        DB.story.all[$"ThatsALotOfDamageToThem_{AmIlleana}_1"] = new()
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
                    Text = "Booyah!",
                    loopTag = "silly".Check()
                }
            }
        };
        DB.story.all[$"WeGotShotButTookNoDamage_{AmIlleana}_0"] = new()
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
                    Text = "The results of my constant experimentions. Behold, perfection.",
                    loopTag = "explain".Check()
                }
            }
        };
        DB.story.all[$"WeGotShotButTookNoDamage_{AmIlleana}_1"] = new()
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
                    Text = "See? All that hull perforation wasn't in vain.",
                    loopTag = "explain".Check()
                }
            }
        };
        DB.story.all[$"WeGotShotButTookNoDamage_{AmIlleana}_2"] = new()
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
                    Text = "That could've been really bad... if you didn't believe in my research.",
                    loopTag = "explain".Check()
                }
            }
        };
        DB.story.all[$"WeAreMovingAroundALot_{AmIlleana}_0"] = new()
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
                    Text = "Dodge and weave! Dodge and weave!",
                    loopTag = "shocked".Check()
                }
            }
        };        
        DB.story.all[$"WeAreMovingAroundALot_{AmIlleana}_1"] = new()
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
                    Text = "The best form of defence is running away...",
                    loopTag = "explain".Check()
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    Text = "wait no I meant movement.",
                    loopTag = "intense".Check()
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
                DB.story.all[$"EnemyPack_GooseEscape"].lines.Add(
                    new CustomSay()
                    {
                        who = AmIlleana,
                        Text = "It's getting away!",
                        loopTag = "angry".Check()
                    }
                );
            }
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "FUCK");
        }
    }
}


