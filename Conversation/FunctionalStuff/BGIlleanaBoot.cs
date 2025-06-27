namespace Illeana.Conversation;

public class BGIlleanaBootSequence : BG
{
    private readonly BG baseBg = new BGBootSequence();
    public override void Render(G g, double t, Vec offset)
    {
        baseBg.Render(g, t, offset);
        BGComponents.Letterbox();
    }
}