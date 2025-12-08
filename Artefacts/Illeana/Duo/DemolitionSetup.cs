using System.Collections.Generic;
using System.Linq;
using Illeana.Actions;
using Nickel;
using Dynaman = Shockah.Dyna.IDynaHook;

namespace Illeana.Artifacts;

/// <summary>
/// Launch and stick charge to gain evade, detonate charge to lose it all. Most of it's already done by the API, just need to find the object tooltips, and make sure to also use the launch bit to check since charges can just appear apparently
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoModDeck = "Shockah.Dyna::Dyna")]
public class DemolitionSetup : Artifact
{
    public int ThingsLeft { get; set; } = 1;  // How many times evade can be given.
    public bool InCombat { get; set; } = false;  // Just for visual purposes
    public bool ActionReset = false;  // For checking value carryover in the hook in case a missed charge makes the next non-launched charge legal.

    public const int THINGSLIMIT = 3;

    public override int? GetDisplayNumber(State s)
    {
        return InCombat? ThingsLeft : null;
    }

    public override Spr GetSprite()
    {
        return (ThingsLeft > 0 || !InCombat)? base.GetSprite():ModEntry.Instance.SprDemolitionSetupDepleted;
    }

    public override void OnCombatEnd(State state)
    {
        InCombat = false;
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        ActionReset = InCombat = true;
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        ThingsLeft = THINGSLIMIT;
    }

    public override List<Tooltip> GetExtraTooltips()
    {
        return
        [
            new TTGlossary("status.evade", ["1"])
        ];
    }

    public override void OnQueueEmptyDuringPlayerTurn(State state, Combat combat)
    {
        ActionReset = true;
    }
}


public class DemolitionSetupProfessional : Dynaman
{
    private bool chargeFired = false;
    public DemolitionSetupProfessional()
    {
        ModEntry.Instance.DynaApi?.RegisterHook(this, 0);
    }

    /// <summary>
    /// If somehow the mod's API is loaded, but the mod isn't... an impossible ask but you never know.
    /// </summary>
    /// <param name="help"></param>
    public void DemolitionSetupProfessionalPossibilityCheck(IModHelper help)
    {
        if (!help.ModRegistry.LoadedMods.ContainsKey("Shockah.Dyna"))
        {
            ModEntry.Instance.DynaApi?.UnregisterHook(this);
        }
    }

    public void OnChargeFired(State state, Combat combat, Ship targetShip, int worldX)
    {
        if (!targetShip.isPlayerShip && state.EnumerateAllArtifacts().Find(a => a is DemolitionSetup) is DemolitionSetup ds) 
        {
            chargeFired = true;
            ds.ActionReset = false;
        }
    }
    public void OnChargeSticked(State state, Combat combat, Ship ship, int worldX)
    {
        if (!ship.isPlayerShip && chargeFired) 
        {
            if (state.EnumerateAllArtifacts().Find(a => a is DemolitionSetup) is DemolitionSetup ds && ds.ThingsLeft > 0 && !ds.ActionReset)
            {
                ds.ThingsLeft--;
                combat.Queue([
                    new ADoesLiterallyNothingButItsForACreditCard
                    {  // Repurposed to serve as a timer
                        timer = 0.2
                    },
                    new AStatus
                    {
                        status = Status.evade,
                        statusAmount = 1,
                        targetPlayer = true,
                        artifactPulse = ds.Key()
                    }
                ]);
            }
            chargeFired = false;
        }
    }
    public void OnChargeTrigger(State state, Combat combat, Ship ship, int worldX)
    {
        if (!ship.isPlayerShip && state.ship.Get(Status.evade) > 0 && state.EnumerateAllArtifacts().Find(a => a is DemolitionSetup) is DemolitionSetup ds)
        {
            combat.Queue(new AStatus
            {
                status = Status.evade,
                statusAmount = 0,
                targetPlayer = true,
                mode = AStatusMode.Set,
                artifactPulse = ds.Key()
            });
        }
    }
}