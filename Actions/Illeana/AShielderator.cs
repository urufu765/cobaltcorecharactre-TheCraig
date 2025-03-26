namespace Craig.Actions;

/// <summary>
/// An action that provides temporary shield based on how much corrode is there
/// </summary>
public class AShielderator : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.0;
        if (s.ship.Get(Status.corrode) > 0)
        {
            c.QueueImmediate(new AStatus
            {
                status = Status.tempShield,
                statusAmount = s.ship.Get(Status.corrode) * 2,
                targetPlayer = true,
                artifactPulse = Key()
            });
        }
    }
}