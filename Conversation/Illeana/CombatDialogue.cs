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
            loopTag = Instance.IlleanaAnim_Intense.Configuration.LoopTag
        });
        DB.story.all[$"WeAreCorroded_Multi_1"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Hold on, I got it under control!",
            loopTag = Instance.IlleanaAnim_Neutral.Configuration.LoopTag
        });
        DB.story.all[$"WeAreCorroded_Multi_2"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "We can totally fix that in the middle of a fight.",
            loopTag = Instance.IlleanaAnim_Sly.Configuration.LoopTag
        });
        DB.story.all[$"WeAreCorroded_Multi_3"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Nuh uh.",
            loopTag = Instance.IlleanaAnim_Neutral.Configuration.LoopTag
        });
        DB.story.all[$"WeAreCorroded_Multi_4"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Hush, I'm concentrating.",
            loopTag = Instance.IlleanaAnim_Squint.Configuration.LoopTag
        });
        DB.story.all[$"WeAreCorroded_Multi_5"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "It's all part of the plan.",
            loopTag = Instance.IlleanaAnim_Explain.Configuration.LoopTag
        });
        DB.story.all[$"WeAreCorroded_Multi_6"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "I'm working on it!",
            loopTag = Instance.IlleanaAnim_Mad.Configuration.LoopTag
        });
        DB.story.all[$"WeAreCorroded_Multi_7"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Computer, snooze.",
            loopTag = Instance.IlleanaAnim_Solemn.Configuration.LoopTag
        });        
        DB.story.all[$"WeAreCorroded_Multi_8"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Uh yes?",
            loopTag = Instance.IlleanaAnim_Neutral.Configuration.LoopTag
        });
        DB.story.all[$"TheyGotCorroded_Multi_5"].lines.Add(new CustomSay()
        {
            who = AmIlleana,
            Text = "Did I do that?",
            loopTag = Instance.IlleanaAnim_Sly.Configuration.LoopTag
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
                    loopTag = Instance.IlleanaAnim_Panic.Configuration.LoopTag
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
                    loopTag = Instance.IlleanaAnim_Panic.Configuration.LoopTag
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
                    loopTag = Instance.IlleanaAnim_Panic.Configuration.LoopTag
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
                    loopTag = Instance.IlleanaAnim_Sly.Configuration.LoopTag
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
                    loopTag = Instance.IlleanaAnim_Neutral.Configuration.LoopTag  // Change to Excited/surprised
                }
            }
        };
    }
}


