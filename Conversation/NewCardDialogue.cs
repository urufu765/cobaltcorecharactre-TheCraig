using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using Illeana.Artifacts;
using Illeana.External;
using static Illeana.Conversation.CommonDefinitions;

namespace Illeana.Conversation;

internal class NewCardDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("en", new Dictionary<string, DialogueMachine>(){
            {"CATsummonedIlleanaCard_Multi_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmCat ],
                lookup = [ "summonIlleana" ],
                oncePerCombatTags = [ "summonIlleanaTag" ],
                dialogue = [
                    new(AmCat, "We need Illeana's expertise right about now.")
                ]
            }},
            {"CATsummonedIlleanaCard_Multi_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmCat, AmIlleana ],
                lookup = [ "summonIlleana" ],
                oncePerCombatTags = [ "summonIlleanaTag" ],
                dialogue = [
                    new(AmIlleana, "squint", "Are you copying me?"),
                    new(AmCat, "ArE yOu CoPyInG mE?")
                ]
            }},
            {"Reminicent_Multi_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                lookup = [ "reminiceCraig" ],
                oncePerCombatTags = [ "reminiceCraigTag" ],
                dialogue = [
                    new(AmIlleana, "possessed", "Quick! toss a hull-breaching shell down to the cannoneer!")
                ]
            }},
            {"Reminicent_Multi_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                lookup = [ "reminiceCraig" ],
                oncePerCombatTags = [ "reminiceCraigTag" ],
                dialogue = [
                    new(AmIlleana, "possessed", "There's nothing in the universe who can stop us now!")
                ]
            }},
            {"Coalescent_Multi_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                lookup = [ "coalesceCraig" ],
                oncePerCombatTags = [ "coalesceCraigTag" ],
                dialogue = [
                    new(AmIlleana, "possessedmad", "If I'm going down, I'm taking you with me!")
                ]
            }},
            {"Coalescent_Multi_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                lookup = [ "coalesceCraig" ],
                oncePerCombatTags = [ "coalesceCraigTag" ],
                dialogue = [
                    new(AmIlleana, "possessedmad", "I'm not letting you pass!")
                ]
            }},
            {"Obmutescent_Multi_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                lookup = [ "obmutesceCraig" ],
                oncePerCombatTags = [ "obmutesceCraigTag" ],
                dialogue = [
                    new(AmIlleana, "nap", "...")
                ]
            }},
            {"Autotomy_Multi_0", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                lookup = [ "autotomySnek" ],
                oncePerRunTags = [ "choppedOffSnekTail" ],
                dialogue = [
                    new(AmIlleana, "intense", "AAH!!!... wait no my tail's fine.")
                ]
            }},
            {"Autotomy_Multi_1", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                lookup = [ "autotomySnek" ],
                oncePerRunTags = [ "choppedOffSnekTail" ],
                dialogue = [
                    new(AmIlleana, "intense", "NOO!! Oh whew, thought you chopped my tail off."),
                    new([
                        new(AmDrake, "sly", "And I'll do it again!"),
                        new(AmDrake, "sly", "And that's not the worst of it.")
                    ])
                ]
            }},
            {"Autotomy_Multi_2", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIlleana ],
                lookup = [ "autotomySnek" ],
                oncePerRunTags = [ "choppedOffSnekTail" ],
                dialogue = [
                    new(AmIlleana, "intense", "GAH! Wait I'm fine.")
                ]
            }},
        });
    }
}