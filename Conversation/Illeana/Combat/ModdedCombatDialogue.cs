using System;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static partial class CombatDialogue
{
    private static void ModdedInject()
    {
        try
        {
            if (ModEntry.Patch_EnemyPack)
            {
                DB.story.all["EnemyPack_GooseEscape_Illeana_0"]
                = new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmIlleana ],
                    enemyIntent = "gooseEscape",
                    turnStart = true,
                    lines = [
                        new CustomSay()
                        {
                            who = "Goose",
                            what = "Honk!"
                        },
                        new CustomSay()
                        {
                            who = AmIlleana,
                            what = "It's getting away!",
                            loopTag = "mad".Check()
                        }
                    ]
                };                
                DB.story.all["EnemyPack_GooseEscape_Illeana_1"]
                = new()
                {
                    type = NodeType.combat,
                    allPresent = [ AmIlleana ],
                    enemyIntent = "gooseEscape",
                    turnStart = true,
                    lines = [
                        new CustomSay()
                        {
                            who = "Goose",
                            what = "Honk!"
                        },
                        new CustomSay()
                        {
                            who = AmIlleana,
                            what = "No... I wanted goose for dinner...",
                            loopTag = "sad".Check()
                        }
                    ]
                };
            }
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "FUCK, couldn't add lines");
        }
    }
}