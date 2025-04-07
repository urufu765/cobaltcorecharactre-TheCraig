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
                                what = "And I haven't had my breakfast today."
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
                                what = "You look very delicious."
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
            foreach(Instruction i in DB.story.all["CrabFactsAreOverNow_Multi_0"].lines)
            {
                if (i is SaySwitch ss)
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
        try
        {
            foreach(Instruction i in DB.story.all["OverheatDrakeFix_Multi_6"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "squint".Check(),
                            what = "Good job. Don't ever do that again."
                        }
                    );
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "solemn".Check(),
                            what = "You know, I had the patchkit ready."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to OverheatDrakeFix_Multi_6");
        }
        try
        {
            foreach(Instruction i in DB.story.all["OverheatDrakesFault_Multi_9"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "tired".Check(),
                            what = "I'll get the fire extinguisher."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to OverheatDrakesFault_Multi_9");
        }
        try
        {
            foreach(Instruction i in DB.story.all["RiderAvast"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "curious".Check(),
                            what = "A vest?"
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to RiderAvast");
        }
        try
        {
            bool skip1 = false;
            foreach(Instruction i in DB.story.all["RiderTiderunnerShouts"].lines)
            {
                if (i is SaySwitch ss)
                {
                    if (skip1)
                    {
                        ss.lines.Add(
                            new CustomSay
                            {
                                who = AmIlleana,
                                loopTag = "squint".Check(),
                                what = "You're not allowed to have it."
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
            Instance.Logger.LogError(err, "Failed to add Illeana response to RiderTiderunnerShouts");
        }
        try
        {
            bool skip1 = false;
            foreach(Instruction i in DB.story.all["SkunkFirstTurnShouts_Multi_0"].lines)
            {
                if (i is SaySwitch ss)
                {
                    if (skip1)
                    {
                        ss.lines.Add(
                            new CustomSay
                            {
                                who = AmIlleana,
                                what = "I'm not an errosion engineer you know."
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
            Instance.Logger.LogError(err, "Failed to add Illeana response to SkunkFirstTurnShouts_Multi_0");
        }
        try
        {
            foreach(Instruction i in DB.story.all["SogginsEscapeIntent_1"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "tired".Check(),
                            what = "Just get out of here."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to SogginsEscapeIntent_1");
        }
        try
        {
            foreach(Instruction i in DB.story.all["SogginsEscapeIntent_3"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "giggle".Check(),
                            what = "Hee hee heeeee."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to SogginsEscapeIntent_3");
        }
        try
        {
            foreach(Instruction i in DB.story.all["Soggins_Missile_Shout_1"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "mad".Check(),
                            what = "Shoot you with what?"
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to Soggins_Missile_Shout_1");
        }
        try
        {
            foreach(Instruction i in DB.story.all["SpikeGetsChatty_Multi_0"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            what = "Here I come."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to SpikeGetsChatty_Multi_0");
        }
        try
        {
            foreach(Instruction i in DB.story.all["TookDamageHave2HP_Multi_1"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "mad".Check(),
                            what = "I'm on it."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to TookDamageHave2HP_Multi_1");
        }
        try
        {
            foreach(Instruction i in DB.story.all["WeJustGainedHeatAndDrakeIsHere_Multi_0"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "mad".Check(),
                            what = "You're messing up my experiments."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to WeJustGainedHeatAndDrakeIsHere_Multi_0");
        }

    }
}