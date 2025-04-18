using System;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static partial class EventDialogue
{
    private static void Reply()
    {
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
        try
        {
            foreach(Instruction i in DB.story.all["EphemeralCardGift"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "screamB".Check(),
                            what = "Aaagh!"
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to EphemeralCardGift");
        }
        try
        {
            foreach(Instruction i in DB.story.all["ForeignCardOffering_After"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "curious".Check(),
                            what = "What's this?"
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to ForeignCardOffering_After");
        }
        try
        {
            foreach(Instruction i in DB.story.all["ForeignCardOffering_Refuse"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "squint".Check(),
                            what = "I'm not letting anyone in my head."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to ForeignCardOffering_Refuse");
        }
        try
        {
            foreach(Instruction i in DB.story.all["Freeze_1"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            flipped = true,
                            loopTag = "curious".Check(),
                            what = "I wonder what it tastes like?"
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to Freeze_1");
        }
        try
        {
            foreach(Instruction i in DB.story.all["GrandmaShop"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            flipped = true,
                            loopTag = "salavating".Check(),
                            what = "Kitten."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to GrandmaShop");
        }
        try
        {
            foreach(Instruction i in DB.story.all["Knight_1"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            flipped = true,
                            loopTag = "intense".Check(),
                            what = "I'm not suited for such glory!"
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to Knight_1");
        }
        try
        {
            foreach(Instruction i in DB.story.all["LoseCharacterCard"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            what = "Abort! Abort!"
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to LoseCharacterCard");
        }
        try
        {
            foreach(Instruction i in DB.story.all["LoseCharacterCard_No"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            what = "Never again."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to LoseCharacterCard_No");
        }        
        try
        {
            foreach(Instruction i in DB.story.all["Sasha_2_Multi_2"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "sad".Check(),
                            what = "Can't play sports..."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to Sasha_2_Multi_2");
        }        
        try
        {
            foreach(Instruction i in DB.story.all["SogginsEscape_1"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "knife".Check(),
                            what = "..."
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to SogginsEscape_1");
        }        
        try
        {
            foreach(Instruction i in DB.story.all["Soggins_Infinite"].lines)
            {
                if (i is SaySwitch ss)
                {
                    ss.lines.Add(
                        new CustomSay
                        {
                            who = AmIlleana,
                            loopTag = "tired".Check(),
                            what = "Do we really have to help him?"
                        }
                    );
                    break;
                }
            }
        }
        catch (Exception err)
        {
            Instance.Logger.LogError(err, "Failed to add Illeana response to Soggins_Infinite");
        }        
    }
}