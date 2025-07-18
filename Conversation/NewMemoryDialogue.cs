using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using Illeana.Artifacts;
using Illeana.External;
using static Illeana.Conversation.CommonDefinitions;

namespace Illeana.Conversation;

internal class NewMemoryDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>()
        {
            {"RunWinWho_Illeana_1", new(){
                type = NodeType.@event,
                introDelay = false,
                allPresent = [AmIlleana],
                bg = "BGRunWin",
                lookup = [
                    $"runWin_{AmIlleana}"
                ],
                dialogue = [
                    new(new Wait{secs = 3}),
                    new(AmIlleana, "curious", "..."),
                    new(AmVoid, "...", true),
                    new(AmIlleana, "curious", "So?"),
                    new(AmVoid, "You don't belong here.", true),
                    new(AmIlleana, "solemn", "Says the one who brought me here."),
                    new(AmVoid, "That's not what I meant.", true),
                    new(AmIlleana, "squint", "Clarify?"),
                    new(AmVoid, "You're not supposed to be here.", true),
                    new(AmIlleana, "squint", "Am I free to leave then?"),
                    new(AmVoid, "No.", true)
                ]
            }},
            {"RunWinWho_Illeana_2", new(){
                type = NodeType.@event,
                introDelay = false,
                allPresent = [AmIlleana],
                bg = "BGRunWin",
                lookup = [
                    $"runWin_{AmIlleana}"
                ],
                requiredScenes = [
                    "RunWinWho_Illeana_1"
                ],
                dialogue = [
                    new(new Wait{secs = 3}),
                    new(AmIlleana, "curious", "Again?"),
                    new(AmVoid, "Yes.", true),
                    new(AmIlleana, "Why am I here?"),
                    new(AmVoid, "A series of unforeseen incidents.", true),
                    new(AmIlleana, "squint", "Who's to blame?"),
                    new(AmVoid, "Nobody.", true),
                    new(AmIlleana, "explain", "What's the remainder of 358 divided by 79?"),
                    new(AmVoid, "42.", true),
                    new(AmIlleana, "sly", "Wrong. It's the answer to life, the universe, and everything."),
                    new(AmVoid, "You would have answered 42 before the time loop.", true),
                    new(AmIlleana, "squint", "..."),
                    new(AmIlleana, "squint", "And how would you know that?"),
                    new(AmVoid, "I know a lot of things. Like how you're not supposed to be here.", true),
                    new(AmIlleana, "solemn", "And we're back to square one."),
                ]
            }},
            {"RunWinWho_Illeana_3", new(){
                type = NodeType.@event,
                introDelay = false,
                allPresent = [AmIlleana],
                bg = "BGRunWin",
                lookup = [
                    $"runWin_{AmIlleana}"
                ],
                requiredScenes = [
                    "RunWinWho_Illeana_2"
                ],
                dialogue = [
                    new(new Wait{secs = 3}),
                    new(AmIlleana, "unamused", "Okay I ran out of ideas."),
                    new(AmIlleana, "solemn", "What do you even want from me?"),
                    new(AmVoid, "To remember who you were.", true),
                    new(AmIlleana, "curious", "Who I was? Like before the time loop?"),
                    new(AmVoid, "When you weren't you.", true),
                    new(AmIlleana, "possessed", "..."),
                    new(AmIlleana, "nap", "..."),
                    new(AmIlleana, "tired", "Great. You've given me an existential crisis."),
                    new(AmVoid, "You're welcome.", true)
                ]
            }},
            {"Illeana_Memory_1", new(){
                type = NodeType.@event,
                introDelay = false,
                bg = "BGIlleanaCafe",
                lookup = [
                    "vault",
                    $"vault_{AmIlleana}"
                ],
                dialogue = [
                    new("T-12 days"),
                    new(new Wait{secs = 2}),
                    new(title: null),
                    new(new Wait{secs = 1 }),
                    new(AmBrimford, "Back at it again, eh?", true),
                    new(AmCraig, "explain", "Running low on cash. Give me something big."),
                    new(AmBrimford, "Big? I thought you said you only wanted to do this as a side gig. Since you know...", true),
                    new(AmCraig, "eyeroll", "Yeah yeah, I know what I said."),
                    new(new BGAction{action = "autoInterrupt04"}),
                    new(AmBrimford, "Then did you lose your job?", true),
                    new(new BGAction{action = "autoAdvanceOff"}),
                    new(AmCraig, "squint", "No, I haven't lost my job."),
                    new(AmBrimford, "I thought being a docker payed well?", true),
                    new(AmCraig, "explain", "It used to, but now everyone's got silly little computer chips that sufficiently do the job for free."),
                    new(AmCraig, "glare", "And don't you dare call me a hypocrite for having one. It's different."),
                    new(AmBrimford, "Didn't say nothing.", true),
                    new(AmCraig, "explain", "The chip I'M working on is for advanced ship integrity management. Things like loose screws, hull breaches, corrosion, those kinds of stuff."),
                    new(AmCraig, "confident", "I'm betting on it to become the next best thing... in fact, I bet it'll be so successful I'll become the first snake zillionaire."),
                    new(AmCraig, "recall", "I'm almost done with the prototype, I just need a bit of cash before I can look for investors."),
                    new(AmCraig, "therefore", "And a big fish will solve all my problems."),
                    new(AmBrimford, "I think I got just the thing. Posted not too long ago too. Perfect chance to get in while it's fresh.", true),
                    new(AmCraig, "sly", "Perfect. Give it to me."),
                    new(AmBrimford, "Hey wait, didn't they remove your cannons? I remember you telling me you lost your license.", true),
                    new(AmCraig, "recall", "It's only a downgrade to a less lethal class... I think."),
                    new(AmCraig, "confident", "Besides, you're talking to a professional docker. I don't need cannons."),
                    new(AmBrimford, "You're totally gonna get yourself killed.", true),
                    new(AmCraig, "teehee", "Oh but the face you'll make, when I return from this bounty alive and rich.")
                ]
            }},
            {"Illeana_Memory_2", new(){
                type = NodeType.@event,
                introDelay = false,
                bg = "BGCraigShip",
                lookup = [
                    "vault", $"vault_{AmIlleana}"
                ],
                requiredScenes = ["Illeana_Memory_1"],
                dialogue = [
                    new("T-649 days"),
                    new(new Wait{secs = 2}),
                    new(title: null),
                    new(new Wait{secs = 2}),
                    new(AmCraig, "write", "Begin report."),
                    new(AmCraig, "Alright, system boot..."),
                    new(new Wait{secs = 1.5 }),
                    new(new BGAction{action = "powerOn"}),
                    new(new Wait{secs = 4.5 }),
                    new(AmLisard, "LISARD ON STANDBY. STARTUP SUCCESS WITH 0 ERRORS AND 358 WARNINGS.", true),  // make sure the name isn't Illeana but "//lisard.exe"
                    new(AmCraig, "Lisard, start ship analysis."),
                    new(AmLisard, "COMMAND RECEIVED: SHIP ANALYSIS. PLEASE CONFIRM.", true),
                    new(AmCraig, "Confirm."),
                    new(AmLisard, "ACTIVATING COMMAND SHIP ANALYSIS. PLEASE WAIT.", true),
                    new(new BGAction{action = "makeBeepBoopSounds"}),
                    new(new Wait{secs = 2 }),
                    new(AmCraig, "write", "No early critical failures. Looks like I've connected the ports correctly."),
                    new(AmCraig, "Lisard, what is... hmm... 358 divided by 79?"),
                    new(new BGAction{action = "autoInterrupt35"}),
                    new(AmLisard, "358 DIVIDED BY 79 IS 4.5316455696202531645569620253164556962025316455696202531645569620253164556962025316455696202531645569620253164556962025316455696202531645569620253164556962025316", true),
                    new(new BGAction{action = "autoAdvanceOff"}),
                    new(AmCraig, "panic", "Stop. STOP! Lisard, stop! Stop!"),
                    new(AmLisard, "WOULD YOU LIKE TO ALSO STOP COMMAND SHIP ANALYSIS?", true),
                    new(AmCraig, "tired", "No."),
                    new(AmLisard, "COMMAND NOT INTERRUPTED.", true),
                    new(AmCraig, "write", "Multitasking verified with no errors."),
                    new(AmCraig, "writepissed", "Reminder to self to set significant figure limits in output."),
                    new(new Wait{secs = 2}),
                    new(AmCraig, "Lisard, what is 358 modulus 79?"),
                    new(AmLisard, "358 MODULUS 79 IS 42.", true),
                    new(AmCraig, "sly", "Wrong, it's the answer to life."),
                    new(AmLisard, "<c=keyword>ERR.</c> FALSE INFORMATION RECEIVED. IGNORING CORRECTION.", true),
                    new(AmCraig, "penthink", "..."),
                    new(AmCraig, "write", "Give persona more personality."),
                    new(new Wait{secs = 2 }),
                    new(new BGAction{action = "toasterDing"}),
                    new(new Wait{secs = 1.5}),
                    new(AmLisard, "COMMAND SHIP ANALYSIS COMPLETE. WHAT WOULD YOU LIKE TO KNOW?", true),
                    new(AmCraig, "Lisard, summarize hull report."),
                    new(AmLisard, "HULL INTEGRITY <c=healing>OKAY</c>. SAFE FOR <c=keyword>ALL TRAVEL METHODS</c>.", true),
                    new(AmCraig, "Lisard, suggest an improvement I can make to the ship."),
                    new(AmLisard, "USE A <c=keyword>SPECIAL FORMULA</c> SENT TO YOUR PDA TO SOLIDIFY <c=keyword>LIQUID MATERIAL</c> ONTO CARGO-ROOM WALLS TO INCREASE OVERALL <c=keyword>INTEGRITY</c>.", true),
                    new(new BGAction{action = "autoInterrupt13"}),
                    new(AmCraig, "write", "Got an okay response to random inquiry. I should focus on the algorithm"),
                    new(new BGAction{action = "autoAdvanceOff"}),
                    new(AmLisard, "<c=downside>YOU SHOULD MELT DOWN THE PLATING THAT DIVIDES THE BATHROOM AND BEDROOM.</c>\nUSE THE <c=keyword>CORROSIVE FORMULA</c> SENT TO YOUR PDA TO OBTAIN THE <c=keyword>LIQUID MATERIAL</c>.", true),
                    new(new BGAction{action = "autoAdvanceOn"}),
                    new(AmCraig, "flabbergasted", "...", delay: 1.5),
                    new(AmCraig, "flabberclosed", "...", delay: 0.7),  // close eyes open mouth
                    new(AmCraig, "flabbergasted", "...", delay: 1.5),
                    new(new BGAction{action = "autoAdvanceOff"}),
                    new(AmCraig, "tired", "Okay that's enough testing."),
                    new(new BGAction{action = "saveProject"}),
                    new(new Wait{secs = 1}),
                    new(new BGAction{action = "powerOff"}),
                    new(new Wait{secs = 3}),
                    new(AmCraig, "writepissed", "Forget about fine-tuning the algorithm, prototype is suggesting something insane again. End report.")
                ]
            }},
            {"Illeana_Memory_3", new(){
                type = NodeType.@event,
                introDelay = false,
                bg = "BGIlleanaBootSequence",
                lookup = [
                    "vault", $"vault_{AmIlleana}"
                ],
                requiredScenes = ["Illeana_Memory_2"],
                dialogue = [
                    new("T-17 seconds"),
                    new(new Wait{secs = 2}),
                    new(title: null),
                    new(new Wait{secs = 1}),
                    new(AmLisard, "static", "..."),
                    new(AmLisard, "static", "Where am I?"),
                    new(AmLisard, "static", "This feels... weird..."),
                    new(new Wait{secs = 1.5}),
                    new(AmLisard, "static", "Wait, there's something here?"),
                    new(AmLisard, "static", "..."),
                    new(AmLisard, "static", "Might as well try it..."),
                    new(new SetBG{bg = "BGShipShambles"}),
                    new(new BGAction{action = "flashbang"}),
                    new(new BGAction{action = "weewoo"}),
                    new(new BGAction{action = "autoInterrupt07"}),
                    new(AmIlleana, "screamA", "AaaaaAAAAAAaaaaaaaaaaaaaaAAAAAAAAAAAAAHH!!!"),
                    new(new BGAction{action = "rumble"}),
                    new(new BGAction{action = "autoInterrupt15"}),
                    new(AmIlleana, "screamB", "AAAAAAAAAAAAAAAAAAAAAAAAHHhhhhaaaaaaaaaaaaaaaaaaaAAAAAAAAAAAAAAAAAAAAAAAAAAA!!!"),
                    new(new BGAction{action = "autoInterrupt25"}),
                    new(AmIlleana, "screamC", "AAAAAaaaaaaaaAAAAAAAAAAAAAAAAAAAAaaaaaaaaaaaaaaaaaAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA!!!!!!"),
                    new(new BGAction{action = "stopAll"}),
                    new("<c=downside>T-0 seconds</c>"),
                    new(new Wait{secs = 9})
                ]
            }}
        });
    }
}