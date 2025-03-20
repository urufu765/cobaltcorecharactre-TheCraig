namespace Craig.Actions;

/// <summary>
/// An action that gives 1 boots if corrode is present, otherwise no.
/// </summary>
public class ABootsOrNoBoots : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.0;
        if (s.ship.Get(Status.corrode) > 0)
        {
            c.QueueImmediate(new AStatus
            {
                status = Status.hermes,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key()
            });
        }
        else
        {
            c.QueueImmediate(new AStatus
            {
                status = Status.engineStall,
                statusAmount = 1,
                targetPlayer = true,
                artifactPulse = Key()
            });
        }
    }
}