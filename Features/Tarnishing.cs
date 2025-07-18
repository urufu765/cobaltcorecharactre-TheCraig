using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Illeana.External;
using HarmonyLib;
using Nanoray.PluginManager;
using Nickel;

namespace Illeana.Features;

public class Tarnishing : IKokoroApi.IV2.IStatusLogicApi.IHook
{

    public Tarnishing()
    {
        ModEntry.Instance.KokoroApi.V2.StatusLogic.RegisterHook(this);

        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.ModifyDamageDueToParts)),
            postfix: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(Ship_MDDTP_butDOUBLE))
        );
    }


    public static void Ship_MDDTP_butDOUBLE(Ship __instance, ref int __result)
    {
        if (__instance.Get(ModEntry.Instance.TarnishStatus.Status) > 0)
        {
            __result *= 2;
            __instance.PulseStatus(ModEntry.Instance.TarnishStatus.Status);
        }
    }

    public bool HandleStatusTurnAutoStep(IKokoroApi.IV2.IStatusLogicApi.IHook.IHandleStatusTurnAutoStepArgs args)
    {
        if (args.Status != ModEntry.Instance.TarnishStatus.Status) return false;
        if (args.Timing != IKokoroApi.IV2.IStatusLogicApi.StatusTurnTriggerTiming.TurnStart) return false;
        if (args.Amount > 0)
        {
            args.Amount -= 1;
        }
        return false;
    }

    public int ModifyStatusChange(IKokoroApi.IV2.IStatusLogicApi.IHook.IModifyStatusChangeArgs args)
    {
        // // Convert new Corrode to Tarnish if Tarnish is present
        // if (args.Status == Status.corrode && args.NewAmount > 0 && args.Ship.Get(ModEntry.Instance.TarnishStatus.Status) > 0)
        // {
        //     args.Ship.Add(ModEntry.Instance.TarnishStatus.Status, args.NewAmount);
        //     return 0;
        // }

        // Convert all Corrode to Tarnish when Tarnish is added
        if (args.Status == ModEntry.Instance.TarnishStatus.Status && args.NewAmount > 0 && args.Ship.Get(Status.corrode) > 0)
        {
            int result = args.Ship.Get(Status.corrode);
            args.Ship.Set(Status.corrode, 0);
            return result + args.NewAmount;
        }

        // Default
        return args.NewAmount;
    }
}
