using System;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static class CardDialogue
{
    internal static void Inject()
    {
        DB.story.all["CATsummonedIlleanaCard_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmCat ],
            lookup = [ "summonIlleana" ],
            oncePerCombatTags = [ "summonIlleanaTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmCat,
                    what = "We need Illeana's expertise right about now."
                }
            }
        };
        DB.story.all["CATsummonedIlleanaCard_Multi_1"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmCat, AmIlleana ],
            lookup = [ "summonIlleana" ],
            oncePerCombatTags = [ "summonIlleanaTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "squint".Check(),
                    what = "Are you copying me?"
                },
                new CustomSay
                {
                    who = AmCat,
                    what = "ArE yOu CoPyInG mE?"
                }
            }
        };
        DB.story.all["Reminicent_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "reminiceCraig" ],
            oncePerCombatTags = [ "reminiceCraigTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "possessed".Check(),
                    what = "Quick! Toss a hull-breaching shell down to the cannoneer!"
                }
            }
        };
        DB.story.all["Reminicent_Multi_1"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "reminiceCraig" ],
            oncePerCombatTags = [ "reminiceCraigTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "possessed".Check(),
                    what = "There's nothing in the universe who can stop us now!"
                }
            }
        };
        DB.story.all["Coalescent_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "coalesceCraig" ],
            oncePerCombatTags = [ "coalesceCraigTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "possessedmad".Check(),
                    what = "If I'm going down, I'm taking you with me!"
                }
            }
        };
        DB.story.all["Coalescent_Multi_1"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "coalesceCraig" ],
            oncePerCombatTags = [ "coalesceCraigTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "possessedmad".Check(),
                    what = "I'm not letting you pass!"
                }
            }
        };
        DB.story.all["Obmutescent_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "obmutesceCraig" ],
            oncePerCombatTags = [ "obmutesceCraigTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "nap".Check(),
                    what = "..."
                }
            }
        };
        DB.story.all["Autotomy_Multi_0"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "autotomySnek" ],
            oncePerRunTags = [ "choppedOffSnekTail" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "intense".Check(),
                    what = "AAH!!!... wait no my tail's fine."
                }
            }
        };
        DB.story.all["Autotomy_Multi_1"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "autotomySnek" ],
            oncePerRunTags = [ "choppedOffSnekTail" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "intense".Check(),
                    what = "NOO!! Oh whew, thought you chopped my tail off."
                },
                new SaySwitch
                {
                    lines = new()
                    {
                        new CustomSay
                        {
                            who = AmDrake,
                            what = "And I'll do it again!",
                            loopTag = "sly"
                        }
                    }
                }
            }
        };
        DB.story.all["Autotomy_Multi_2"] = new()
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "autotomySnek" ],
            oncePerRunTags = [ "choppedOffSnekTail" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "intense".Check(),
                    what = "GAH! Wait I'm fine."
                }
            }
        };
        DB.story.all["BuildACure_Multi_0"] = new()  
        {
            type = NodeType.combat,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            lookup = [ "buildACure" ],  // Not implemented yet
            oncePerRunTags = [ "buildACureTag" ],
            lines = new()
            {
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "intense".Check(),
                    what = "So uhh, you guys aren't going to kick me out, right?"
                }
            }
        };
    }
}