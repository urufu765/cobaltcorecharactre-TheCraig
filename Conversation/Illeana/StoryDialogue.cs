using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static class StoryDialogue
{
    internal static void Inject()
    {
        DB.story.all["Illeana_Intro_0"] = new()
        {
            type = NodeType.@event,
            lookup = new() {"zone_first"},
            once = true,
            allPresent = [ AmIlleana ],
            bg = "BGRunStart",
            lines = new()
            {
                new CustomSay()
                {
                    who = AmCat,
                    Text = "Wakey wakey!"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    Text = "Hey... That's my line...",
                    loopTag = "tired".Check()
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    Text = "...",
                    loopTag = "neutral".Check()
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    Text = "This isn't my ship.",
                    loopTag = "squint".Check()
                },
                new CustomSay()
                {
                    who = AmCat,
                    Text = "Who are you?",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    Text = "Can't say...",
                    loopTag = "squint".Check()
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    Text = "But! I can make use of the unutilized hull you got.",
                    loopTag = "neutral".Check()
                },
                new CustomSay()
                {
                    who = AmCat,
                    Text = "What! We need those!",
                    loopTag = "worried"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    Text = "Don't worry! You won't even notice it's gone.",
                    loopTag = "sly".Check()
                }
            }
        };
    }
}