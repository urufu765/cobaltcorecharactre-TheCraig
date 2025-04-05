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
                    what = "Wakey wakey!"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Hey... That's my line...",
                    loopTag = "tired".Check()
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "...",
                    loopTag = "neutral".Check()
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "This isn't my ship.",
                    loopTag = "squint".Check()
                },
                new CustomSay()
                {
                    who = AmCat,
                    what = "Who are you?",
                    loopTag = "squint"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Can't say...",
                    loopTag = "squint".Check()
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "But! I can make use of the unutilized hull you got.",
                    loopTag = "neutral".Check()
                },
                new CustomSay()
                {
                    who = AmCat,
                    what = "What! We need those!",
                    loopTag = "worried"
                },
                new CustomSay()
                {
                    who = AmIlleana,
                    what = "Don't worry! You won't even notice they're gone.",
                    loopTag = "sly".Check()
                }
            }
        };
        // DB.story.all["Illeana_Peri_0"] = new()
        // {
        //     type = NodeType.@event,
        //     lookup = new() {"zone_first"},
        //     once = true,
        //     allPresent = [ AmIlleana ],
        //     bg = "BGRunStart",
        //     lines = new()
        //     {
        //         new CustomSay()
        //         {
        //             who = AmCat,
        //             what = "Wakey wakey!"
        //         },
        //         new CustomSay()
        //         {
        //             who = AmIlleana,
        //             what = "Hey... That's my line...",
        //             loopTag = "tired".Check()
        //         },
        //         new CustomSay()
        //         {
        //             who = AmIlleana,
        //             what = "...",
        //             loopTag = "neutral".Check()
        //         },
        //         new CustomSay()
        //         {
        //             who = AmIlleana,
        //             what = "This isn't my ship.",
        //             loopTag = "squint".Check()
        //         },
        //         new CustomSay()
        //         {
        //             who = AmCat,
        //             what = "Who are you?",
        //             loopTag = "squint"
        //         },
        //         new CustomSay()
        //         {
        //             who = AmIlleana,
        //             what = "Can't say...",
        //             loopTag = "squint".Check()
        //         },
        //         new CustomSay()
        //         {
        //             who = AmIlleana,
        //             what = "But! I can make use of the unutilized hull you got.",
        //             loopTag = "neutral".Check()
        //         },
        //         new CustomSay()
        //         {
        //             who = AmCat,
        //             what = "What! We need those!",
        //             loopTag = "worried"
        //         },
        //         new CustomSay()
        //         {
        //             who = AmIlleana,
        //             what = "Don't worry! You won't even notice it's gone.",
        //             loopTag = "sly".Check()
        //         }
        //     }
        // };
    }
}