using System.Collections.Generic;
using System.Linq;
using Nickel;
using Hooker = Illeana.External.IKokoroApi.IV2.IStatusLogicApi.IHook;

namespace Illeana.Artifacts;

/// <summary>
/// Extra heat when corrode, extra curse when tarnish
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "rft.Marielle::Marielle")]
public class ReactiveFormula : Artifact
{
    private static Status? CurseLoc => ModEntry.Instance.Helper.Content.Statuses.LookupByUniqueName("rft.Marielle::Curse")?.Status;
    private static Status Tarnish => ModEntry.Instance.TarnishStatus.Status;

    private Status? Curse = null;

    public override List<Tooltip>? GetExtraTooltips()
    {
        Curse ??= CurseLoc;
        if (Curse is null) return null;

        List<Tooltip> l = StatusMeta.GetTooltips(Curse.Value, 1);
        l.AddRange(StatusMeta.GetTooltips(Tarnish, 1));
        l.Insert(0, new TTGlossary("status.corrode", ["1"]));
        l.Insert(0, new TTGlossary("status.heat", ["1"]));
        return l;
    }
}

public class ReactiveFormulae : Hooker
{
    private static Status? CurseLoc => ModEntry.Instance.Helper.Content.Statuses.LookupByUniqueName("rft.Marielle::Curse")?.Status;
    private static Status Tarnish => ModEntry.Instance.TarnishStatus.Status;

    private Status? Curse = null;


    public ReactiveFormulae()
    {
        ModEntry.Instance.KokoroApi.V2.StatusLogic.RegisterHook(this);
    }

    public void ReactiveFormulaPossibilityCheck(IModHelper help)
    {
        if (!help.ModRegistry.LoadedMods.ContainsKey("rft.Marielle"))
        {
            ModEntry.Instance.KokoroApi.V2.StatusLogic.UnregisterHook(this);
        }
    }

    public int ModifyStatusChange(Hooker.IModifyStatusChangeArgs args)
    {
        // Check for duo presence
        if (args.State.EnumerateAllArtifacts().Find(a => a is ReactiveFormula) is not ReactiveFormula rf)
        {
            goto skipCalc;
        }

        // Check if the Curse status id has been stored
        if (Curse is null)
        {
            Curse = CurseLoc;
            if (Curse is null) goto skipCalc;
        }


        if (args.Status == Status.heat && args.Ship.Get(Status.corrode) > 0)
        {
            rf.Pulse();
            return args.NewAmount + args.Ship.Get(Status.corrode);
        }

        if (args.Status == Curse && args.Ship.Get(Tarnish) > 0)
        {
            rf.Pulse();
            return args.NewAmount + args.Ship.Get(Tarnish);
        }


        skipCalc:
        return args.NewAmount;  // default
    }
}