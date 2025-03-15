using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Craig.External;
using HarmonyLib;
using Nanoray.PluginManager;
using Nickel;

namespace Craig.Features;

public class Tarnishing : IRegisterable, IKokoroApi.IV2.IStatusLogicApi.IHook
{
    internal static IStatusEntry TarnishStatus { get; private set; } = null!;

    public static void Register(IPluginPackage<IModManifest> package, IModHelper h)
    {
        TarnishStatus = ModEntry.Instance.Helper.Content.Statuses.RegisterStatus("Tarnish", new StatusConfiguration
        {
            Definition = new StatusDef
            {
                isGood = false,
                affectedByTimestop = true,
                color = new Color("a43fff"),
                icon = ModEntry.RegisterSprite(package, "assets/tarnish.png").Sprite
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["status", "Tarnish", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["status", "Tarnish", "desc"]).Localize
        });

        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.ModifyDamageDueToParts)),
            transpiler: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(Ship_MDDTP_butDOUBLE))
        );

        var instance = new Tarnishing();
        ModEntry.Instance.KokoroApi.V2.StatusLogic.RegisterHook(instance);
    }


    public static void Ship_MDDTP_butDOUBLE(Ship __instance, ref int __result, State s, Combat c, int incomingDamage, Part part, bool piercing = false)
    {
        if (__instance.Get(TarnishStatus.Status) > 0)
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
        if (args.Status != TarnishStatus.Status) return false;
        if (args.Timing != IKokoroApi.IV2.IStatusLogicApi.StatusTurnTriggerTiming.TurnStart) return false;
        if (args.Amount > 0)
        {
            args.Amount -= 1;
        }
        return false;
    }
}
