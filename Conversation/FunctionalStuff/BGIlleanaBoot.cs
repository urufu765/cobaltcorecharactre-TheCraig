namespace Illeana.Conversation;

public class BGIlleanaBootSequence : BGBootSequence
{
    public override void Render(G g, double t, Vec offset)
    {
        base.Render(g, t, offset);
        BGComponents.Letterbox();
    }
}