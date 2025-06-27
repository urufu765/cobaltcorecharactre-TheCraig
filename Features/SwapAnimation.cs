using System;
using HarmonyLib;
using Microsoft.Extensions.Logging;

namespace Illeana.Features;

public static class SwapTheAnimation
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: typeof(Character).GetMethod(nameof(Character.DrawFace), AccessTools.all),
            prefix: new HarmonyMethod(typeof(SwapTheAnimation), nameof(FaceSwapper))
        );
    }

    private static void FaceSwapper(Character __instance, ref string animTag, bool mini = false)
    {
        if (__instance.type == ModEntry.IlleanaTheSnek.CharacterType)
        {
            if (IlleanaClock.Clocked(15))
            {
                ModEntry.Instance.shoeanaMode = ModEntry.Instance.settings.ProfileBased.Current.AntiSnakeMode;
            }

            if (ModEntry.Instance.shoeanaMode)
            {
                animTag = mini ? "shoeanamini" : "shoeana";
            }
        }
    }

    
}