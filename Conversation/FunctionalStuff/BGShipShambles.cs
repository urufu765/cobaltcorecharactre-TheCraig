using System;
using FSPRO;
using Illeana.API;
using Microsoft.Extensions.Logging;
using Nickel;

namespace Illeana.Conversation;

public class BGShipShambles : BG, ICanAutoAdvanceDialogue
{
    private bool _autoAdvance;
    private double timeToInterrupt = -1;
    private double flash;
    public double Flashbang { get => flash; }
    public double Whitening { get => rumbling / 4; }
    private bool rumble;
    private double rumbling;
    // private double weewooA;
    // private bool weewooAgo;
    // private ISoundInstance? weewooASound;
    // private double weewooB = 1.5;
    // private bool weewooBgo;
    // private ISoundInstance? weewooBSound;
    // private bool weewoo;
    // private float weewooVol;
    public bool AutoAdvanceDialogue()
    {
        return _autoAdvance;
    }

    public override void Render(G g, double t, Vec offset)
    {
        Color c = new Color(0, 0.1, 0.2, 1.0).gain(0.5);
        Draw.Fill(c);
        BGComponents.NormalStars(g, t, offset);
        BGComponents.RegularNebula(g, offset, c);

        BGComponents.ScrapField(g, offset, g.state.map.GetPrimaryColor().gain(0.7));

        if (flash > 0)
        {
            //Draw.Fill(Colors.white.fadeAlpha(flash / 2));
            flash -= g.dt;
        }

        //BGComponents.Letterbox(); // Handled by external method

        if (timeToInterrupt > 0) timeToInterrupt -= g.dt;
        if (timeToInterrupt <= 0 && timeToInterrupt > -1)
        {
            _autoAdvance = true;
            timeToInterrupt = -1;
        }

        if (rumble)
        {
            rumbling += g.dt;
            // Draw.Fill(new Color(0.25, 0.5, 1).gain(rumbling / 4), blend: BlendMode.Screen);
            // Draw.Fill(Colors.white.fadeAlpha(rumbling / 5));
            g.state.shake = rumbling;
        }

        // weewooA += g.dt;
        // if (weewooA > 3)
        // {
        //     weewooA -= 3;
        //     weewooAgo = true;
        // }
        // weewooB += g.dt;
        // if (weewooB > 3)
        // {
        //     weewooB -= 3;
        //     weewooBgo = true;
        // }

        // if (weewoo) if (weewooVol < 1) weewooVol += (float)g.dt / 2;
        //         else { if (weewooVol > 0) weewooVol -= (float)g.dt; }

        UpdateSound();
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
            case "flashbang":
                flash = 1;
                ISoundInstance isi = ModEntry.Instance.BGShambles_0_Tinnitus.CreateInstance();
                isi.Volume = 0.7f;
                break;
            case "rumble":
                rumble = true;
                rumbling = 0;
                break;
            // case "weewoo":
            //     weewoo = true;
            //     break;
            case "stopAll":
                _autoAdvance = false;
                timeToInterrupt = -1;
                rumble = false;
                // weewoo = false;
                flash = rumbling = 0;
                break;
        }
    }

    public void UpdateSound()
    {
        if (rumble)
        {
            Audio.Auto(Event.Scenes_CobaltCritical);
        }


        // if (weewooAgo)
        // {
        //     weewooASound = ModEntry.Instance.WeeWooA.CreateInstance();
        //     weewooAgo = false;
        // }
        // if (weewooBgo)
        // {
        //     weewooBSound = ModEntry.Instance.WeeWooB.CreateInstance();
        //     weewooBgo = false;
        // }
        // try
        // {
        //     if (weewooASound is ISoundInstance isia)
        //     {
        //         isia.Volume = weewooVol;
        //     }
        //     if (weewooBSound is ISoundInstance isib)
        //     {
        //         isib.Volume = weewooVol;
        //     }
        // }
        // catch (Exception err)
        // {
        //     ModEntry.Instance.Logger.LogError(err, "Whoops!");
        // }
    }
}