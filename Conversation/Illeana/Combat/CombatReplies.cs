using System;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static partial class CombatDialogue
{
    private static void Replies()
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
            foreach(Instruction i in DB.story.all["BanditThreats_Multi_0"].lines)
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
            Instance.Logger.LogError(err, "Failed to add Illeana response to BanditThreats_Multi_0");
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
        try
        {
            foreach(Instruction i in DB.story.all["DillianShouts"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "solemn".Check(),
                            what = "The feeling's not mutual."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to DillianShouts");
        }
        try
        {
            foreach(Instruction i in DB.story.all["DualNotEnoughDronesShouts_Multi_2"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "panic".Check(),
                            what = "How did you know I was part robot?"
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to DualNotEnoughDronesShouts_Multi_2");
        }
    }
}