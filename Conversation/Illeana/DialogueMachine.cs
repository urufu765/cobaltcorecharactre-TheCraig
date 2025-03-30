using Illeana.Dialogue;
using HarmonyLib;
using Microsoft.Extensions.Logging;

namespace Illeana;

public static class DialogueMachine
{
    public static void Apply()
    {
        StoryDialogue.Inject();
        EventDialogue.Inject();
        CombatDialogue.Inject();
        CombatDialogue.ModdedInject();
    }
}