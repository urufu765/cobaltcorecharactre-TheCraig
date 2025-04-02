using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static class MemoryDialogue
{
    internal static void Inject()
    {
        DB.story.all["RunWinWho_Illeana_1"] = new()
        {
            type = NodeType.@event,
            introDelay = false,
            allPresent = [ AmIlleana ],
            bg = "BGRunWin",
            lookup = [
                $"runWin_{AmIlleana}"
            ],
            lines = new()
            {
                new Wait
                {
                    secs = 3
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "speechless".Check(),  // Speechless
                    Text = "..."
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    Text = "You don't belong here."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "curious".Check(),  // Curious
                    Text = "I don't?"
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    Text = "No."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "unamused".Check(),
                    Text = "..."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "eyebrowraise".Check(),
                    Text = "Am I free to leave then?"
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    Text = "No."
                }
            }
        };
        DB.story.all["RunWinWho_Illeana_2"] = new()
        {
            type = NodeType.@event,
            introDelay = false,
            allPresent = [ AmIlleana ],
            bg = "BGRunWin",
            lookup = [
                $"runWin_{AmIlleana}"
            ],
            requiredScenes = [
                $"RunWinWho_{AmIlleana}_1"
            ],
            lines = new()
            {
                new Wait
                {
                    secs = 3
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "curious".Check(),  // Curious
                    Text = "Why am I here?"
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    Text = "A series of unforeseen incidents."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "curious".Check(),  // Eyebrow raise
                    Text = "Why do I feel like these memories aren't mine?"
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    Text = "Because they aren't."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "shocked".Check(),
                    Text = "What?"
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "blinkrapid".Check(),
                    Text = "..."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    Text = "What?"
                },
            }
        };
        DB.story.all["RunWinWho_Illeana_3"] = new()
        {
            type = NodeType.@event,
            introDelay = false,
            allPresent = [ AmIlleana ],
            bg = "BGRunWin",
            lookup = [
                $"runWin_{AmIlleana}"
            ],
            requiredScenes = [
                $"RunWinWho_{AmIlleana}_2"
            ],
            lines = new()
            {
                new Wait
                {
                    secs = 3
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "tired".Check(),  // Tired animated
                    Text = "Sorry, can you give me a moment?"
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "tired".Check(),  // Tired animated
                    Text = "I'm having an identity crisis."
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    Text = "You have all the time in the world."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "desperate".Check(),  // Desperate
                    Text = "Really?"
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    Text = "No."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "speechless".Check(),  // Thousand mile stare
                    Text = "..."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "smile".Check(),  // Smile
                    Text = "..."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "silly",
                    Text = "I don't know if that was intentional, but you've made me feel a little less overwhelmed. Thanks."
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    Text = "You're welcome."
                }
            }
        };
        DB.story.all["Illeana_Memory_1"] = new()
        {
            type = NodeType.@event,
            introDelay = false,
            bg = "BGVault",
            lookup = [
                "vault",
                $"vault_{AmIlleana}"
            ],
            lines = new()
            {
                new TitleCard  // Replace with Custom Titlecard
                {
                    
                },
                new Wait
                {
                    secs = 2,
                },
                new TitleCard
                {
                    empty = true,
                },
                new Wait
                {
                    secs = 1
                }
            }
        };        
        DB.story.all["Illeana_Memory_2"] = new()
        {
            type = NodeType.@event,
            introDelay = false,
            bg = "BGVault",
            lookup = [
                "vault",
                $"vault_{AmIlleana}"
            ],
            requiredScenes = [
                $"{AmIlleana}_Memory_1"
            ],
            lines = new()
            {
                new TitleCard  // Replace with Custom Titlecard
                {
                    
                },
                new Wait
                {
                    secs = 2,
                },
                new TitleCard
                {
                    empty = true,
                },
                new Wait
                {
                    secs = 1
                }
            }
        };
        // Illeana downloads herself to Craig's body and becomes her.
        DB.story.all["Illeana_Memory_3"] = new()
        {
            type = NodeType.@event,
            introDelay = false,
            bg = "BGVault",
            lookup = [
                "vault",
                $"vault_{AmIlleana}"
            ],
            requiredScenes = [
                $"{AmIlleana}_Memory_2"
            ],
            lines = new()
            {
                new TitleCard  // Replace with Custom Titlecard
                {
                    
                },
                new Wait
                {
                    secs = 2,
                },
                new TitleCard
                {
                    empty = true,
                },
                new Wait
                {
                    secs = 1
                },
                new CustomSay
                {
                    who = AmUnknown,
                    Text = "..."
                },
                new CustomSay
                {
                    who = AmUnknown,
                    Text = "Where am I?"
                },
                new CustomSay
                {
                    who = AmUnknown,
                    Text = "This feels weird..."
                },
                new Wait
                {
                    secs = 1.5
                },
                new CustomSay
                {
                    who = AmUnknown,
                    Text = "Wait, there's something here?"
                },
                new CustomSay
                {
                    who = AmUnknown,
                    Text = "..."
                },
                new CustomSay
                {
                    who = AmUnknown,
                    Text = "Might as well try it..."
                },
                // Flashbang.
                new Wait
                {
                    secs = 1
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "screamA".Check(),
                    Text = "aaaaaaaaaaahAAAAAAAAAAAAAAAAAAAAHH!!!!"
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "screamB".Check(),
                    Text = "AAAAAAAAAAHHhhaaaaaaaaaaaaaaaaaAAAAAAA!!!"
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "screamC".Check(),
                    Text = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA!!!!!"
                },
                // RUMBLE
                new Wait
                {
                    secs = 5
                },
                // Kill sounds on
                // TitlecardT-0 seconds
                new Wait
                {
                    secs = 9
                }
            }
        };
    }
}