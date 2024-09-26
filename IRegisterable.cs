using Nanoray.PluginManager;
using Nickel;

namespace DemoMod;

internal interface IRegisterable
{
    static abstract void Register(IPluginPackage<IModManifest> package, IModHelper helper);
}