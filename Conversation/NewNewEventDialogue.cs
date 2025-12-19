using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using Illeana.Artifacts;
using Illeana.External;
using static Illeana.Conversation.CommonDefinitions;

namespace Illeana.Conversation;

internal class NewNewEventDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>(){
            {$"ChoiceCardRewardOfYourColorChoice_{AmIlleana}", new(){
                type = NodeType.@event,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                bg = "BGBootSequence",
                dialogue = [
                    new(AmIlleana, "squint", "Ow... I felt that in my bones."),
                    new(AmCat, "Energy readings are back to normal.")
                ]
            }},
            {"ShopkeeperInfinite_Illeana", new(){
                type = NodeType.@event,
                lookup = [ "shopBefore" ],
                bg = "BGShop",
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(new QMulti()),
                    new(AmShopkeeper, "Howdy", true),
                    new(AmIlleana, "squint", "Have we met before?"),
                    new(new Jump{key = "NewShop"}),

                    new(new QMulti()),
                    new(AmShopkeeper, "Back again, are we?", true),
                    new(AmIlleana, "explain", "Yeah, need more material to experiment with."),
                    new(new Jump{key = "NewShop"}),

                    new(new QMulti()),
                    new(AmShopkeeper, "Hey.", true),
                    new(AmIlleana, "sly", "Hey."),
                    new(new Jump{key = "NewShop"})
                ]
            }},
            {$"CrystallizedFriendEvent_{AmIlleana}", new(){
                type = NodeType.@event,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                bg = "BGCrystalizedFriend",
                dialogue = [
                    new(new Wait{secs = 1.5}),
                    new(AmIlleana, "sly", "Hey.")
                ]
            }},
            {$"LoseCharacterCard_{AmIlleana}", new(){
                type = NodeType.@event,
                allPresent = [ AmIlleana ],
                oncePerRun = true,
                bg = "BGSupernova",
                dialogue = [
                    new(AmIlleana, "shocked", "My research!")
                ]
            }},
            {"DraculaTime", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "squint", "No, I don't recall any Dracula in my friends list...")
                ]
            }},
            {"AbandonedShipyard_Repaired", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "I helped!")
                ]
            }},
            {"EphemeralCardGift", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "screamB", "AAAAAAAAAAaaaaaaaagh!")
                ]
            }},
            {"ForeignCardOffering_After", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "curious", "What's this?")
                ]
            }},
            {"ForeignCardOffering_Refuse", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "squint", "Get out of my head.")
                ]
            }},
            {"Freeze_1", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "curious", "I wonder what it tastes like?")
                ]
            }},
            {"GrandmaShop", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "salavating", "Kitten.")
                ]
            }},
            {"Knight_1", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "intense", "Such glory!")
                ]
            }},
            {"LoseCharacterCard", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "Abort? Abort!")
                ]
            }},
            {"LoseCharacterCard_No", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "explain", "That wasn't so bad.")
                ]
            }},
            {"Sasha_2_Multi_2", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "sad", "Can't play sports...")
                ]
            }},
            {"SogginsEscape_1", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "knife", "...")
                ]
            }},
            {"Soggins_Infinite", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "tired", "Do we really have to help him?")
                ]
            }},
        });
    }
}