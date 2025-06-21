using System;
using HarmonyLib;

namespace Illeana.Features;

public static class IlleanaClock
{
    private static int _updateFrames = 0;

    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: typeof(State).GetMethod(nameof(State.Update), AccessTools.all),
            postfix: new HarmonyMethod(typeof(IlleanaClock), nameof(ClockUpdate))
        );
    }

    private static void ClockUpdate()
    {
        _updateFrames++;
    }

    /// <summary>
    /// Checks whether the current frame is a multiple of the given interval. For when you want to perform constant updates that may be expensive and don't necessarily need to run every frame.
    /// </summary>
    /// <param name="interval"></param>
    /// <returns></returns>
    public static bool Clocked(int interval)
    {
        if (_updateFrames % interval == 0) return true;
        return false;
    }
}