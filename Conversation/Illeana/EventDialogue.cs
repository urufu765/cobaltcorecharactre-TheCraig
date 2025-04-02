using System;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static class EventDialogue
{
    internal static void Inject()
    {
        DB.story.all["ChoiceCardRewardOfYourColorChoice_Illeana"] = new()
        {
            type = NodeType.@event,
            oncePerRun = true,
            allPresent = [ AmIlleana ],
            bg = "BGBootSequence",
            lines = new()
            {
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Ow... I felt that in my bones.",
                    loopTag = "squint".Check()
                },
                new CustomSay()
                {
                    who = AmCat,
                    what = "Energy readings are back to normal."
                }
            }
        };
        DB.story.all["ShopkeeperInfinite_Illeana_Multi_0"] = new()
        {
            type = NodeType.@event,
            lookup = [ "shopBefore" ],
            bg = "BGShop",
            allPresent = [ AmIlleana ],
            lines = [
                new CustomSay
                {
                    who = AmShopkeeper,
                    what = "Howdy.",
                    flipped = true
                },
                new CustomSay
                {
                    who = AmIlleana,
                    what = "Have we met before?",
                    loopTag = "squint".Check()
                },
                new Jump
                {
                    key = "NewShop"
                }
            ]
        };
        DB.story.all["ShopkeeperInfinite_Illeana_Multi_1"] = new()
        {
            type = NodeType.@event,
            lookup = [ "shopBefore" ],
            bg = "BGShop",
            allPresent = [ AmIlleana ],
            lines = [
                new CustomSay
                {
                    who = AmShopkeeper,
                    what = "Back again, are we?",
                    flipped = true
                },
                new CustomSay
                {
                    who = AmIlleana,
                    what = "Yeah, need more material to experiment with.",
                    loopTag = "explain".Check()
                },
                new Jump
                {
                    key = "NewShop"
                }
            ]
        };
        DB.story.all["ShopkeeperInfinite_Illeana_Multi_2"] = new()
        {
            type = NodeType.@event,
            lookup = [ "shopBefore" ],
            bg = "BGShop",
            allPresent = [ AmIlleana ],
            lines = [
                new CustomSay
                {
                    who = AmShopkeeper,
                    what = "Hey.",
                    flipped = true
                },
                new CustomSay
                {
                    who = AmIlleana,
                    what = "Hey.",
                    loopTag = "sly".Check()
                },
                new Jump
                {
                    key = "NewShop"
                }
            ]
        };
        try
        {
            foreach(Instruction i in DB.story.all["DraculaTime"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            what = "No, I don't recall any Dracula in my friends list...",
                            loopTag = "squint".Check()
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to Dracular");
        }
        try
        {
            foreach(Instruction i in DB.story.all["AbandonedShipyard_Repaired"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            what = "I helped!"
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to AbandonedShipyard_Repaired");
        }
    }
}