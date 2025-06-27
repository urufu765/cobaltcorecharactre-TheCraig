using Illeana.API;

namespace Illeana.Conversation;

public class BGIlleanaCafe : BG, ICanAutoAdvanceDialogue
{
    private bool _autoAdvance;
    private double timeToInterrupt = -1;
    private readonly BG baseBg = new BGCafeFlashback();

    public bool AutoAdvanceDialogue()
    {
        return _autoAdvance;
    }

    public override void Render(G g, double t, Vec offset)
    {
        baseBg.Render(g, t, offset);
        BGComponents.Letterbox();

        if (timeToInterrupt > 0) timeToInterrupt -= g.dt;
        if (timeToInterrupt <= 0 && timeToInterrupt > -1)
        {
            _autoAdvance = true;
            timeToInterrupt = -1;
        }
    }

    public override void OnAction(State s, string action)
    {
        switch (action)
        {
            case "autoAdvanceOn":
                _autoAdvance = true;
                break;
            case string a when a.Contains("autoInterrupt"):
                _autoAdvance = false;
                timeToInterrupt = double.Parse(a[^2..]) / 10;
                break;
            case "autoAdvanceOff":
                _autoAdvance = false;
                timeToInterrupt = -1;
                break;
        }
    }
}