using System;
using FMOD.Studio;
using FSPRO;
using Illeana.API;
using Microsoft.Extensions.Logging;
using Nickel;

namespace Illeana.Conversation;

public class BGCraigShip : BG, ICanAutoAdvanceDialogue
{
    private bool _autoAdvance;
    private double timeToInterrupt = -1;
    private bool personaPower;
    private bool beepBoop;
    private double beepBoopDecay;
    private double beepBoopHold;
    private double _beepBoopRandoTrigger;

    private static Spr BGn_0_Platform => ModEntry.Instance.BGShip_0_Platform;
    private static Spr BGn_1_Craig => ModEntry.Instance.BGShip_1_Craig;
    private static Spr BGn_1_CraigProps => ModEntry.Instance.BGShip_1_CraigProps;
    private static Spr BGn_2_Persona => ModEntry.Instance.BGShip_2_Persona;
    private static Spr BGn_3_Backing => ModEntry.Instance.BGShip_3_Backing;
    private static Spr BGn_4_Props => ModEntry.Instance.BGShip_4_Props;
    private static Spr BGs_A_Craig => ModEntry.Instance.BGShip_A_Craig;
    private static Spr BGs_B_Persona => ModEntry.Instance.BGShip_B_Persona;
    private static Spr BGs_C_Props => ModEntry.Instance.BGShip_C_Props;
    private static Spr BGa_D_Glass => ModEntry.Instance.BGShip_D_Glass;
    
    /*
    Layer order (from front to back):
    - Letterbox
    - Platform
    - (screen)Craig
    - Craig
    - (screen)Persona
    - Persona
    - Backing
    - (screen)Props
    - Props
    - Glow shit
    - (add)Glass
    - Stars
    */

    public bool AutoAdvanceDialogue()
    {
        return _autoAdvance;
    }

    public override void Render(G g, double t, Vec offset)
    {
        Color darkerGrey = new Color(0.1f, 0.1f, 0.1f);
        Color darkGrey = new Color(0.2f, 0.2f, 0.2f);
        Color darkishGrey = new Color(0.35f, 0.35f, 0.35f);
        Color grey = new Color(0.65f, 0.65f, 0.65f);
        Color coldDarkish = new Color(0.0f, 0.12f, 0.14f);
        Color coldDark = new Color(0.0f, 0.05f, 0.06f);
        Color warm = new Color(0.25f, 0.24f, 0.22f);
        Color warmDark = new Color(0.12f, 0.1f, 0.06f);
        if (timeToInterrupt > 0) timeToInterrupt -= g.dt;
        if (timeToInterrupt <= 0 && timeToInterrupt > -1)
        {
            _autoAdvance = true;
            timeToInterrupt = -1;
        }
        Voronois.RenderStarsMap(g, t, 12, G.screenSize / 2, G.screenSize, offset * 4 / 8);  // Stars

        Draw.Sprite(BGa_D_Glass, 0, 0, color:new(1,1,1,0.2), blend: BlendMode.Add);

        Draw.Sprite(BGn_4_Props, 0, 0, color:Color.Lerp(Colors.black, Colors.white, 0.2));
        Draw.Sprite(BGs_C_Props, 0, 0, color:Glow_Breathe(t, darkGrey, darkishGrey, 0.25, 0.6, squareify:0.15, flicker:0.7), blend: BlendMode.Screen);

        Draw.Sprite(BGn_3_Backing, 0, 0);

        if (personaPower)
        {
            Draw.Sprite(BGs_B_Persona, 0, 0, color: Glow_Breathe(t, darkGrey, darkishGrey, 0.25, squareify:0.15, flicker:0.7), blend: BlendMode.Screen);
        }

        
        Draw.Sprite(BGn_1_Craig, 0, 0, color:new Color(0.2549f, 0.7098f, 0.6824f).gain(0.2));
        Draw.Sprite(BGn_1_CraigProps, 0, 0);
        Draw.Sprite(BGs_A_Craig, 0, 0, color: Glow_Breathe(t, darkerGrey, darkGrey, 3, 0.1), blend: BlendMode.Screen);
        Glow.Draw(new(149, 192), 15, Glow_Breathe(t, warmDark, warm, 0.01, flicker:1.2));
        Glow.Draw(new(153, 189), 15, Glow_Breathe(t, warmDark, warm, 0.01, 0.4, flicker:1.2));
        if (personaPower)
        {
            Draw.Sprite(BGn_2_Persona, 0, 0, color: Glow_Breathe(t, grey, Colors.white, 2));
            Glow.Draw(new(161, 185), new Vec(7, 24), Glow_Breathe(t, coldDark, coldDarkish, 0.25, flicker: 0.7));
            Glow.Draw(new(159, 160), new Vec(13, 25), Glow_Breathe(t, Colors.black, coldDark, 0.25, flicker: 0.7));
            Glow.Draw(new(160, 177), new Vec(11, 16), Glow_Breathe(t, Colors.black, coldDark, 0.25, flicker: 0.7));
            Glow.Draw(new(151, 157), new Vec(10, 15), Glow_Breathe(t, warmDark.gain(0.5), warm.gain(0.3), 0.25, flicker: 0.7));
        }

            Draw.Sprite(BGn_0_Platform, 0, 0);

        BGComponents.Letterbox();
        UpdateSounds(g.dt);
    }

    /// <summary>
    /// Makes the glow stuff pulse in a sine wave
    /// </summary>
    /// <param name="dt">Delta Time (accumulated)</param>
    /// <param name="dark">Darkest color</param>
    /// <param name="light">Lightest color</param>
    /// <param name="cycleTime">Time for full revolution</param>
    /// <param name="offset">Cycle offset (from 0 to <1)</param>
    /// <param name="squareify">0 for sine wave, 1 for square wave</param> 
    /// <param name="flicker">Introduce random flickering, from 0 to 1.0 chance</param>
    /// <param name="fIntensity">Intensity of flicker. 1 is no change, more = lighter, less = darker</param>
    /// <returns></returns>
    private static Color Glow_Breathe(double dt, Color dark, Color light, double cycleTime, double offset=0, double squareify=0, double flicker=1)
    {
        // The sinning section ;)
        double sin = (Math.Sin(dt / cycleTime * Math.PI - (offset * Math.PI)) + 1) / 2;
        double square = Math.Round(sin);
        double lerpVal = Mutil.Lerp(sin, square, squareify);

        if (flicker != 1)
        {
            Random rand = new();
            return Color.Lerp(dark, light, lerpVal).gain(Mutil.Lerp(1, flicker, rand.NextDouble()));
        }

        return Color.Lerp(dark, light, lerpVal);
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
            case "powerOn":
                personaPower = true;
                break;
            case "powerOff":
                personaPower = false;
                break;
            case "makeBeepBoopSounds":
                beepBoop = true;
                beepBoopDecay = 1;
                beepBoopHold = 2;
                break;
            case "toasterDing":
                beepBoop = false;
                ISoundInstance isid = ModEntry.Instance.BGShip_1_Ding.CreateInstance();
                isid.Volume = 0.2f;
                // Make it play a hotel bell ding
                break;
            case "saveProject":
                ISoundInstance isip = ModEntry.Instance.BGShip_0_Down.CreateInstance();
                isip.Volume = 1f;
                break;
            case "killSounds":
                personaPower = beepBoop = false;
                break;
        }
    }

    public void UpdateSounds(double tFrame)
    {
        if (personaPower)
        {
            EventInstance? eventInstance = Audio.Auto(Event.Scenes_ColdStart);
            if (eventInstance == null)
            {
                return;
            }
            eventInstance.GetValueOrDefault().setParameterByName("ColdStart", 0f, false);
        }
        if (beepBoop)
        {
            if (beepBoopHold > 0) beepBoopHold -= tFrame;
            else
            {
                if (beepBoopDecay > 0) beepBoopDecay -= tFrame;
            }

            // do sound, decay makes it go from 100% vol to 20%, randotrigger triggers a random beep boop every random second
            if (_beepBoopRandoTrigger <= 0)
            {
                _beepBoopRandoTrigger = 0.05 + Mutil.NextRand() * 0.1;
                ISoundInstance isi = ModEntry.Instance.BGShip_2_Beep.CreateInstance();
                //isi.Volume = (float)Mutil.Lerp(0.05, 0.1, beepBoopDecay);
                isi.Volume = (float)Mutil.Lerp(0.2, 0.4, beepBoopDecay);
                isi.Pitch = (float)Mutil.Lerp(0.7, 2, Mutil.NextRand());
            }
            else
            {
                _beepBoopRandoTrigger -= tFrame;
            }
        }
    }
}