using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using Illeana.Artifacts;
using Illeana.External;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

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
                    new(AmIsaac, "explain", "I've read in a magazine somewhere that snakes see with their tongue."),
                    new(AmIsaac, "But I haven't seen you do that throughout our journey."),
                    new(AmIlleana, "Ah that?", true),
                    new(AmIlleana, "explain", "That's because I'm mostly a robot inside.", true),
                    new(AmIlleana, "giggle", "See?", true),
                    new(AmIlleana, "Oh also I don't have a tongue.", true),
                    new(AmIsaac, "writing", "I see, so you're not actually a biological snake... how do you taste things?"),
                    new(AmIlleana, "explain", "I use my nose.", true),
                    new(AmIsaac, "writing", "Do you NEED to eat? Is there a charging port on you somewhere?"),
                    new(AmIlleana, "...", true),
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
        });
    }
}