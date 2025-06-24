using HarmonyLib;
using Illeana.API;
using System;

namespace Illeana.Conversation;


public static class AutoDialogueAdvancer
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: typeof(Dialogue).GetMethod(nameof(Dialogue.OnInputPhase), AccessTools.all),
            postfix: new HarmonyMethod(typeof(AutoDialogueAdvancer), nameof(AutoDialogueAdvancer.ForceDialogueAdvance))
        );
    }

    private static void ForceDialogueAdvance(Dialogue __instance, G g)
    {
        if (!__instance.alreadyAdvancedThisFrame && __instance.bg is ICanAutoAdvanceDialogue icaad && icaad.AutoAdvanceDialogue())
        {
            __instance.alreadyAdvancedThisFrame = true;
            __instance.OnPlayerAdvanceInput(g);
        }
    }
}
