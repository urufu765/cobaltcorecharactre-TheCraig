using Nanoray.PluginManager;
using Nickel;

namespace Craig;

internal interface IRegisterable
{
    static abstract void Register(IPluginPackage<IModManifest> package, IModHelper helper);
}