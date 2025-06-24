using Illeana.Conversation;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using System;

namespace Illeana;

[Obsolete("Not used any more", true)]
internal static class DrawLoadingScreenFixer
{
    internal static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: typeof(MG).GetMethod("DrawLoadingScreen", AccessTools.all),
            prefix: new HarmonyMethod(typeof(DrawLoadingScreenFixer), nameof(DrawLoadingScreen_Prefix)),
            postfix: new HarmonyMethod(typeof(DrawLoadingScreenFixer), nameof(DrawLoadingScreen_Postfix))
        );
    }

    private static void DrawLoadingScreen_Prefix(MG __instance, ref int __state) => __state = __instance.loadingQueue?.Count ?? 0;

    private static void DrawLoadingScreen_Postfix(MG __instance, ref int __state)
    {
        if (__state <= 0) return;
        if ((__instance.loadingQueue?.Count ?? 0) > 0) return;
    }
}
