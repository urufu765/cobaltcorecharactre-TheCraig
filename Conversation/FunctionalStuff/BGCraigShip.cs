using FMOD.Studio;
using FSPRO;
using Illeana.API;

namespace Illeana.Conversation;

public class BGCraigShip : BGRunStart, ICanAutoAdvanceDialogue
{
    private bool _autoAdvance;
    private double timeToInterrupt = -1;
    private bool personaPower;
    private bool beepBoop;
    private double beepBoopADSRDecay;
    private double _beepBoopRandoTrigger;

    private static Spr BGn_0_Platform => ModEntry.Instance.BGShip_0_Platform;
    private static Spr BGn_1_Craig => ModEntry.Instance.BGShip_1_Craig;
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
        if (timeToInterrupt > 0) timeToInterrupt -= g.dt;
        if (timeToInterrupt <= 0 && timeToInterrupt > -1)
        {
            _autoAdvance = true;
            timeToInterrupt = -1;
        }
        base.Render(g,t,offset);  // Stars

        Draw.Sprite(BGa_D_Glass, 0, 0, color:new(1,1,1,0.2), blend: BlendMode.Add);

        Draw.Sprite(BGn_4_Props, 0, 0);
        Draw.Sprite(BGs_C_Props, 0, 0, color:Color.Lerp(Colors.black, Colors.white, 0.5), blend: BlendMode.Screen);

        Draw.Sprite(BGn_3_Backing, 0, 0);

        if (personaPower)
        {
            Draw.Sprite(BGn_2_Persona, 0, 0, color:new(1,1,1,1));
            Draw.Sprite(BGs_B_Persona, 0, 0, color:Color.Lerp(Colors.black, Colors.white, 0.5), blend: BlendMode.Screen);
        }

        Draw.Sprite(BGn_1_Craig, 0, 0);
        Draw.Sprite(BGs_A_Craig, 0, 0, color:Color.Lerp(Colors.black, Colors.white, 0.5), blend: BlendMode.Screen);

        Draw.Sprite(BGn_0_Platform, 0, 0);

        BGComponents.Letterbox();
        UpdateSounds();
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
                beepBoopADSRDecay = 2;
                break;
            case "toasterDing":
                // Make it play a hotel bell ding
                break;
            case "killSounds":
                personaPower = beepBoop = false;
                break;
        }
    }

    public void UpdateSounds()
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
            // do sound, decay makes it go from 100% vol to 20%, randotrigger triggers a random beep boop every random second
        }
    }
}