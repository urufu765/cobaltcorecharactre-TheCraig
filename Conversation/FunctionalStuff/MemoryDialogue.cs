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
                    loopTag = "unamused".Check(),
                    what = "..."
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    what = "You don't belong here."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "curious".Check(),  // Curious
                    what = "I don't?"
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    what = "No."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "solemn".Check(),
                    what = "..."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "solemn".Check(),
                    what = "Am I free to leave then?"
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    what = "No."
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
                    what = "Why am I here?"
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    what = "A series of unforeseen incidents."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "curious".Check(),  // Eyebrow raise
                    what = "Why do I feel like these memories aren't mine?"
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    what = "Because they aren't."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "shocked".Check(),
                    what = "What?"
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "blinkrapid".Check(),
                    what = "............................."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "panic".Check(),
                    what = "WHAT?!"
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
                    what = "Sorry, can you give me a moment?"
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "tired".Check(),  // Tired animated
                    what = "I'm having an identity crisis."
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    what = "You have all the time in the world."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "neutral".Check(),  // Desperate
                    what = "Really?"
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    what = "No."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "blinkrapid".Check(),  // Thousand mile stare
                    what = "..................."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "silly".Check(),  // Smile
                    what = "..."
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "silly",
                    what = "I don't know if that was intentional, but you've made me feel a little less overwhelmed. Thanks."
                },
                new CustomSay
                {
                    who = AmVoid,
                    flipped = true,
                    what = "You're welcome."
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
                },
                new CustomSay
                {
                    who = AmBrimford,
                    what = "Back again are we?"
                },
                new CustomSay
                {
                    who = AmCraig,
                    what = "Give me something big this time."
                },
                new CustomSay
                {
                    who = AmBrimford,
                    what = "Don't you do this as a side gig? Since you know..."
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigEyeroll".Check(),
                    what = "Yeah yeah, since I got a full-time job and all."
                },
                new CustomSay
                {
                    who = AmCraig,
                    what = "Listen, people don't need professional dockers anymore. Their silly little computer chips that come standard in the modern space ships is enough for them."
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigGlare".Check(),
                    what = "And don't you dare call me a hypocrite, the artificial intelligence I'M working on can't even orient the ship correctly."
                },
                new CustomSay
                {
                    who = AmBrimford,
                    what = "Didn't say nothing."
                },
                new CustomSay
                {
                    who = AmCraig,
                    what = "Anyways, enterprises aren't hiring me, I'm running low on money, I need a big fish to catch."
                },
                new CustomSay
                {
                    who = AmBrimford,
                    what = "I've got just the thing. Posted not too long ago too. Perfect chance to get in while it's fresh."
                },
                new CustomSay
                {
                    who = AmBrimford,
                    what = "Hey wait, isn't your ship missing cannons? I remember you telling me you lost your firearm license along with your cannons."
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigExplain".Check(),
                    what = "You're talking to a professional docker."
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigExcited".Check(),
                    what = "I'm sure I can compensate by grabbing the target ship with my giant docking hands!"
                },
                new CustomSay
                {
                    who = AmBrimford,
                    what = "You're totally gonna get yourself killed."
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigSly".Check(),
                    what = "Oh the face you'll make when I return from this bounty alive and rich."
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
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigInjured".Check(),
                    what = "Agh.. who would've known they had a decent sharpshooter aboard that ship..."
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigInjuredButDetermined".Check(),
                    what = "But this won't stop me from living another day!"
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigInjured".Check(),
                    what = "Computer, initiate emergency escape protocal."
                },
                new CustomSay
                {
                    who = "illeanaComp",
                    what = "Your engines have been destroyed. Your thrusters ruined beyond recognition. Your robotic hands have been disabled. The fact you are still conscious is a miracle."
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigInjured".Check(),
                    what = "... What have I said about irrelevant information?"
                },
                new CustomSay
                {
                    who = "illeanaComp",
                    what = "To forget about it and only inform you about hard facts. Unfortunately, these are the hard facts."
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigClosed".Check(),
                    what = "..."
                },
                new CustomSay
                {
                    who = AmCraig,
                    loopTag = "craigInjured".Check(),
                    what = "Damn... guess I'll die..."
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
                    what = "..."
                },
                new CustomSay
                {
                    who = AmUnknown,
                    what = "Where am I?"
                },
                new CustomSay
                {
                    who = AmUnknown,
                    what = "This feels weird..."
                },
                new Wait
                {
                    secs = 1.5
                },
                new CustomSay
                {
                    who = AmUnknown,
                    what = "Wait, there's something here?"
                },
                new CustomSay
                {
                    who = AmUnknown,
                    what = "..."
                },
                new CustomSay
                {
                    who = AmUnknown,
                    what = "Might as well try it..."
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
                    what = "aaaaaaaaaaahAAAAAAAAAAAAAAAAAAAAHH!!!!"
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "screamB".Check(),
                    what = "AAAAAAAAAAHHhhaaaaaaaaaaaaaaaaaAAAAAAA!!!"
                },
                new CustomSay
                {
                    who = AmIlleana,
                    loopTag = "screamC".Check(),
                    what = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA!!!!!"
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