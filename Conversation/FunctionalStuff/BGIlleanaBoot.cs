using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Illeana.Conversation;


public class BlinkText
{
    public required List<string> text;
    public required int index;
    public double interval = 1;
    public double timer = 0;
    public int flip = 0;

    public int Flip()
    {
        flip++;
        if (flip >= text.Count) flip = 0;
        return flip;
    }
}


public class AnimText
{
    public required string text;
    public required List<string> anim;
    public required int index;
    public double interval = 1.0;
    public double timer = 0.0;
    public int flip = 0;

    public int Flip()
    {
        flip++;
        if (flip >= anim.Count) flip = 0;
        return flip;
    }
}


public class BGIlleanaBootSequence : BG
{
    private readonly BG baseBg = new BGBootSequence();
    private List<string> displayText = [];
    private Queue<string> animText = [];
    private List<BlinkText> blinkText = [];
    private List<double> debugText = [];
    private double animTimer = 0.0;
    private double animInterval = 1.0;
    public override void Render(G g, double t, Vec offset)
    {
        baseBg.Render(g, t, offset);
        if (animText.Count > 0 && displayText.Count > 0)
        {
            animTimer += g.dt;
            if (animTimer > animInterval)
            {
                animTimer = 0;
                displayText[^1] += animText.Dequeue();
            }
        }
        if (blinkText.Count > 0)
        {
            foreach (BlinkText bt in blinkText)
            {
                bt.timer += g.dt;
                if (bt.timer > bt.interval)
                {
                    bt.timer = 0;
                    displayText[bt.index] = bt.text[bt.Flip()];
                }
            }
        }
        for (int i = 0; i < displayText.Count; i++)
        {
            Draw.Text(displayText[i], 100, 90 + (i * 15), null, Colors.textBold);
        }
        // while (debugText.Count < displayText.Count) debugText.Add(0.0);
        // if (debugText.Count > 0) debugText[^1] += g.dt;
        // for (int j = 0; j < debugText.Count; j++)
        // {
        //     Draw.Text(debugText[j].ToString(), 100, 115 + (j * 15));
        // }
        BGComponents.Letterbox();
    }

    public override void OnAction(State s, string action)
    {
        base.OnAction(s, action);
        switch (action)
        {
            case "clear":
                displayText = [];
                blinkText = [];
                debugText = [];
                animText = [];
                break;
            case string t when t.Contains("animationInterval"):
                animInterval = double.Parse(t.Split("$")[1]);
                break;
            case string t when t.Contains(">>>"):
                displayText.Add(t[3..]);
                break;
            case ">+>":
                animText = [];
                break;
            case string t when t.Contains(">+>"):
                animText = new(t[3..].Select(c => c.ToString()));
                break;
            case string t when t.Contains(">x>"):
                animText = new(t[3..].Split("|"));
                break;
            case string t when t.Contains(">*>"):
                blinkText.Add(new()
                {
                    text = [..t[3..].Split('$')],
                    index = displayText.Count
                });
                displayText.Add(blinkText[^1].text[0]);
                break;
            case string t when t.Contains("blinkInterval"):
                blinkText[^1].interval = double.Parse(t.Split('$')[1]);
                break;
            case string t when t.Contains("blinkSpecific"):
                List<string> l = [..t.Split('$')[1].Split('/')];
                if (l.Count != 2) break;
                blinkText[int.Parse(l[0])].interval = double.Parse(l[1]);
                break;
            case string t when t.Contains("setLatest"):
                displayText[^1] = t.Split('$')[1];
                break;
            case string t when t.Contains("setAndClearLatest"):
                displayText[^1] = t.Split('$')[1];
                blinkText.RemoveAt(blinkText.Count - 1);
                break;
        }
    }
}