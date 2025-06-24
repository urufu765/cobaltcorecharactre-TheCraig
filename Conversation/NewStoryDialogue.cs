using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using Illeana.Artifacts;
using Illeana.External;
using static Illeana.Conversation.CommonDefinitions;

namespace Illeana.Conversation;

internal class NewStoryDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>(){
            {"Illeana_Intro_0", new(){
                type = NodeType.@event,
                lookup = [ "zone_first" ],
                once = true,
                allPresent = [ AmIlleana ],
                bg = "BGRunStart",
                dialogue = [
                    new(AmCat, "Wakey wakey!"),
                    new(AmIlleana, "tired", "Hey... That's my line..."),
                    new(AmIlleana, "solemn", "..."),
                    new(AmIlleana, "squint", "This isn't my ship."),
                    new(AmCat, "squint", "Who are you?"),
                    new(AmIlleana, "squint", "Can't say..."),
                    new(AmIlleana, "But! I can make use of all this unutilized hull you got."),
                    new(AmCat, "worried", "What! We need that!"),
                    new(AmIlleana, "sly", "Don't worry, you won't even notice it's gone.")
                ]
            }},
            {"Illeana_Intro_1", new(){
                type = NodeType.@event,
                lookup = [ "zone_first" ],
                once = true,
                allPresent = [ AmIlleana ],
                requiredScenes = ["Illeana_Intro_0"],
                bg = "BGRunStart",
                dialogue = [
                    new(AmCat, "Beep beep!"),
                    new(AmIlleana, "tired", "I'm up..."),
                    new(AmIlleana, "squint", "..."),
                    new(AmIlleana, "curious", "What's with all this padding on the wall?"),
                    new(AmCat, "smug", "It's to stop you from destroying anything."),
                    new(AmIlleana, "silly", "Oh really? Is that a challenge?"),
                    new(AmCat, "worried", "What? No?"),
                    new(AmCat, "lean", "Hey! Get back here!")
                ]
            }},
            {"Illeana_Intro_2", new(){
                type = NodeType.@event,
                lookup = [ "zone_first" ],
                once = true,
                allPresent = [ AmIlleana ],
                requiredScenes = ["Illeana_Intro_1"],
                bg = "BGRunStart",
                dialogue = [
                    new(AmCat, "squint", "..."),
                    new(AmIlleana, "nap", "..."),
                    new(AmCat, "mad", "Wake up!"),
                    new(AmIlleana, "shocked", "AAH!"),
                    new(AmIlleana, "intense", "Oh hello."),
                    new(AmCat, "grumpy", "You better not touch anything."),
                    new(AmIlleana, "curious", "..."),
                    new(AmCat, "grumpy", "I realized there's no stopping you, no matter what I try."),
                    new(AmCat, "grumpy", "So I'm just going to tell you to not do what you usually do."),
                    new(AmIlleana, "But if I don't do anything, how would I know I'm doing what I usually do?"),
                    new(AmCat, "squint", "Good point..."),
                    new(AmIlleana, "explain", "So I'll touch something and see if that's part of my usual routine."),
                    new(AmCat, "Alright, go ahead then."),
                    new(AmCat, "squint", "..."),
                    new(AmCat, "mad", "Wait wh- HEY!")
                ]
            }},
            {"Illeana_Peri_0", new(){
                type = NodeType.@event,
                lookup = [ "zone_first"],
                once = true,
                allPresent = [ AmIlleana, AmPeri ],
                bg = "BGRunStart",
                requiredScenes = [ "Illeana_Intro_0", "Peri_1" ],
                dialogue = [
                    new(AmPeri, "Illeana? Can I ask you something?"),
                    new(AmIlleana, "What is it?", true),
                    new(AmPeri, "On your file, your photo ID looks nothing like you."),
                    new(AmIlleana, "squint", "Maybe the lighting wasn't good.", true),
                    new(AmPeri, "squint", "Your eyes are different."),
                    new(AmIlleana, "intense", "Maybe the lighting was different.", true),
                    new(AmPeri, "squint", "..."),
                    new(AmIlleana, "silly", "...", true),
                    new(AmPeri, "squint", "I guess you look similar enough...")
                ]
            }},
            {"Illeana_Peri_1", new(){
                type = NodeType.@event,
                lookup = [ "zone_first"],
                once = true,
                allPresent = [ AmIlleana, AmPeri ],
                bg = "BGRunStart",
                requiredScenes = [ "Illeana_Peri_0", "RunWinWho_Illeana_3" ],
                dialogue = [    
                    new(AmPeri, "Illeana was it? Can I ask you something?"),
                    new(AmIlleana, "Go ahead.", true),
                    new(AmPeri, "squint", "I have your file here, and your picture looks different..."),
                    new(AmIlleana, "possessed", "So you found out.", true),
                    new(AmPeri, "What?"),
                    new(AmIlleana, "Hmm?", true),
                    new(AmPeri, "Say again?"),
                    new(AmIlleana, "explain", "I said it might be because that photo might've been sunbleached.", true),
                    new(AmPeri, "squint", "Doesn't that make the photo lighter?"),
                    new(AmIlleana, "Or something. Look, is this really that important?", true),
                    new(AmPeri, "squint", "..."),
                    new(AmPeri, "squint", "Just curious.")
                ]
            }},
            {"Illeana_Isaac_0", new(){
                type = NodeType.@event,
                lookup = ["after_crystal"],
                bg = "BGCrystalNebula",
                allPresent = [AmIlleana, AmIsaac],
                once = true,
                priority = true,
                requiredScenes = ["Illeana_Intro_0"],
                dialogue = [
                    new(AmIsaac, "Hey Illeana."),
                    new(AmIlleana, "Sup?", true),
                    new(AmIsaac, "Mind if I ask something personal?"),
                    new(AmIlleana, "Depends.", true),
                    new(AmIsaac, "explains", "I've read in a magazine somewhere that snakes see with their tongue."),
                    new(AmIsaac, "But I haven't seen you do that throughout our journey."),
                    new(AmIlleana, "Ah that?", true),
                    new(AmIlleana, "explain", "That's because I'm mostly a robot inside.", true),
                    new(AmIlleana, "giggle", "See?", true),
                    new(AmIlleana, "Oh also I don't have a tongue.", true),
                    new(AmIsaac, "writing", "I see, so you're not actually a biological snake... how do you taste things?"),
                    new(AmIlleana, "explain", "I use my nose.", true),
                    new(AmIsaac, "writing", "Do you NEED to eat? Is there a charging port on you somewhere?"),
                    new(AmIlleana, "squint", "I think I have one on my back? I THINK I can recharge myself I guess? Never tried it.", true),
                    new(AmIsaac, "writing", "Do you have a brain or a hard disk?"),
                    new(AmIlleana, "intense", "Uhhhhh ummm... The latter?", true),
                    new(AmIsaac, "writing", "Did you digitalize your mind? Are you an AI?"),
                    new(AmIlleana, "panic", "...", true),
                    new(AmIlleana, "shocked", "...", true),
                    new(AmIlleana, "intense", "Actually, can we forget that this conversation ever happened?", true),
                    new(AmIsaac, "Okay.")
                ]
            }},
            {"Illeana_Riggs_0", new(){
                type = NodeType.@event,
                lookup = [ "zone_first"],
                once = true,
                allPresent = [ AmIlleana, AmRiggs ],
                bg = "BGRunStart",
                requiredScenes = [ "Illeana_Intro_0"],
                dialogue = [
                    new(AmRiggs, "..."),
                    new(AmIlleana, "...", true),
                    new(AmRiggs, "serious", "..."),
                    new(AmIlleana, "...", true),
                    new(AmRiggs, "squint", "..."),
                    new(AmIlleana, "...", true),
                    new(AmRiggs, "squint", "Umm, can I help you?"),
                    new(AmIlleana, "Oh! Don't mind me, pretend I'm not here.", true),
                    new(AmRiggs, "squint", "It's a little difficult with you looking over my shoulder..."),
                    new(AmIlleana, "Let me observe you a bit more, then I'll be out of your fur.", true),
                    new(AmRiggs, "serious", "What is this for?"),
                    new(AmIlleana, "Figuring out how best to overclock the thrusters.", true),
                    new(AmRiggs, "squint", "And this helps how?"),
                    new(AmIlleana, "silly", "I don't know, but it's fun to look at.", true),
                    new(AmRiggs, "huh", "...")
                ]
            }},
            {"Illeana_Drake_0", new(){
                type = NodeType.@event,
                lookup = [ "zone_first"],
                once = true,
                allPresent = [ AmIlleana, AmDrake ],
                bg = "BGRunStart",
                requiredScenes = [ "Illeana_Intro_0"],
                dialogue = [
                    new(AmDrake, "squint", "..."),
                    new(AmIlleana, "curious", "...", true),
                    new(AmDrake, "squint", "..."),
                    new(AmIlleana, "Why are you staring at me like that?", true),
                    new(AmDrake, "squint", "I'm trying to figure out how to get a jar your size."),
                    new(AmIlleana, "intense", "... why?", true),
                    new(AmDrake, "blush", "I heard snake rum is delicious."),
                    new(AmIlleana, "unamused", "You do know making something like that takes ages, right?", true),
                    new(AmDrake, "slyblush", "I can wait."),
                    new(AmIlleana, "squint", "Don't you dare.", true)
                ]
            }}
        });
    }
}