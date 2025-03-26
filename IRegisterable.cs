using Nanoray.PluginManager;
using Nickel;

namespace Illeana;

internal interface IRegisterable
{
    static abstract void Register(IPluginPackage<IModManifest> package, IModHelper helper);
}