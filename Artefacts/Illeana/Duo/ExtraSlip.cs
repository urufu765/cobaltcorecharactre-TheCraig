using System.Collections.Generic;

namespace Illeana.Artifacts;

/// <summary>
/// Autopilot when corroded
/// </summary>
[ArtifactMeta(pools = new[] { ArtifactPool.Common }), DuoArtifactMeta(duoDeck = Deck.hacker)]
public class ExtraSlip : Artifact
{
    public bool LeftUsed {get;set;}
    public bool RightUsed {get;set;}

    public override void OnCombatEnd(State state)
    {
        LeftUsed = RightUsed = false;
    }
    public override void OnCombatStart(State state, Combat combat)
    {
        LeftUsed = false;
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        RightUsed = false;
    }

    // TODO: insert code about left and right card used.
    public override void OnPlayerPlayCard(int energyCost, Deck deck, Card card, State state, Combat combat, int handPosition, int handCount)
    {
        if (!LeftUsed && handPosition == 0)
        {
            combat.Queue(new AStatus
            {
                status = Status.autododgeLeft,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key()
            });
            LeftUsed = true;
        }

        if (!RightUsed && handPosition == handCount)
        {
            combat.Queue(new AStatus
            {
                status = Status.autododgeRight,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key()
            });
            RightUsed = true;
        }
    }


    public override int ModifyAutopilotAmount()
    {
        if (MG.inst.g.state.ship.Get(Status.corrode) > 0)
        {
            return 1;
        }
        return 0;
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        List<Tooltip> l = StatusMeta.GetTooltips(ModEntry.Instance.TarnishStatus.Status, 1);
        l.Insert(0, new TTGlossary("status.autododgeRight", ["1"]));
        l.Insert(0, new TTGlossary("status.autododgeLeft", ["1"]));
        l.Insert(0, new TTGlossary("status.corrode", ["1"]));
        return l;
    }
}