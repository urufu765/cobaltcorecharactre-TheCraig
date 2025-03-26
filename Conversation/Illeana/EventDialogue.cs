using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static class EventDialogue
{
    internal static void Inject()
    {
        DB.story.all[$"ChoiceCardRewardOfYourColorChoice_{AmIlleana}"] = new()
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
                    Text = "Ow... I felt that in my bones.",
                    loopTag = Instance.IlleanaAnim_Squint.Configuration.LoopTag
                },
                new CustomSay()
                {
                    who = Cat,
                    Text = "Energy readings are back to normal."
                }
            }
        };
    }
}