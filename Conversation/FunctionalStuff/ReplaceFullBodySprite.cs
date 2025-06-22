using System.Linq;
using Microsoft.Extensions.Logging;
using Nickel;
using static Illeana.Dialogue.CommonDefinitions;
using HarmonyLib;
using System;

namespace Illeana.Dialogue;

public static class ReplaceSnakeBodyArt
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: typeof(Events).GetMethod(nameof(Events.RunWinWho), AccessTools.all),
            postfix: new HarmonyMethod(typeof(ReplaceSnakeBodyArt), nameof(SwitchFullBody))
        );
    }

    private static void SwitchFullBody()
    {
        if (!BGRunWin.charFullBodySprites.ContainsKey(AmIlleanaDeck)) return;
        if (ModEntry.Instance.settings.ProfileBased.Current.AntiSnakeMode)
        {
            BGRunWin.charFullBodySprites[AmIlleanaDeck] = ModEntry.Instance.ShoeanaEnd;
        }
        else
        {
            Random rand = new();
            if (rand.Next(100) == 0)
            {
                BGRunWin.charFullBodySprites[AmIlleanaDeck] = ModEntry.Instance.IlloodleEnd;
            }
            else
            {
                BGRunWin.charFullBodySprites[AmIlleanaDeck] = ModEntry.Instance.IlleanaEnd;
            }
        }
    }
}