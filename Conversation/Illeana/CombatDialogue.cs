using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static class CombatDialogue
{
    internal static void Inject()
    {
        DB.story.all[$"WeAreCorroded_{AmIlleana}"] = new()
        {
            type = NodeType.combat,
            allPresent = new() { }
        };
    }
}


