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

public class SetXRenderer
{

    public static void Apply(Harmony harmony)
    {

        harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Card), nameof(Card.RenderAction)),
            transpiler: new HarmonyMethod(MethodBase.GetCurrentMethod()!.DeclaringType!, nameof(IgnoreXHintRenderForSetStatus))
        );
    }

    private static object IgnoreXHintRenderForSetStatus()
    {
        throw new NotImplementedException();
    }
}