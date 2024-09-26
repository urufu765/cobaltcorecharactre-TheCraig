using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;

namespace DemoMod;

internal class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;

    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;
    }
}

