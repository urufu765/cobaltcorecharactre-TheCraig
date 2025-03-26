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


    public static void Ship_MDDTP_butDOUBLE(Ship __instance, ref int __result, State s, Combat c, int incomingDamage, Part part, bool piercing = false)
    {
        if (__instance.Get(ModEntry.Instance.TarnishStatus.Status) > 0)
        {
            if (__instance.Get(Status.corrode) > 0)
            {
                c.QueueImmediate(
                    new AStatus
                    {
                        status = Status.corrode,
                        statusAmount = -1,
                        targetPlayer = __instance.isPlayerShip
                    }
                );
            }
            __result *= 2;
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
}
