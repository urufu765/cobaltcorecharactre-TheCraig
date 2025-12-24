using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Illeana.Features;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace Illeana.Conversation;

public enum LisardGameState
{
    wait,
    intro,
    gameplay,
    gameover,
    gameover_confirm,
    winner,
    winner_confirm,
}

public class LisardPuzzle : Route, IScriptTarget, OnMouseDown, OnInputPhase
{
    /// <summary>
    /// Background to paint in the... background
    /// </summary>
    public BG? bg = null;

    /// <summary>
    /// Current mood of the game
    /// </summary>
    public LisardGameState current = LisardGameState.wait;
    public UIKey? lastGpSelection;

    /// <summary>
    /// Held directional key accumulator, to allow older inputs to work if the newer input has been let go.
    /// </summary>
    public int[] heldDirection = [0, 0, 0, 0];

    /// <summary>
    /// Whether character is currently in motion, don't allow inputs to do things while in motion.
    /// </summary>
    public bool inMotion = false;

    /// <summary>
    /// Current game stage
    /// </summary>
    public int stage = -1;

    /// <summary>
    /// How many stages there are
    /// </summary>
    public const int STAGES = 1;

    /// <summary>
    /// History of moves done, such that if the player fucks up and loses a life, it does a fancy animation of moving backwards back to the start of the stage
    /// </summary>
    public List<(int x, int y)>[] moveHistory = [.. Enumerable.Range(0, STAGES).Select(_ => new List<(int, int)>())];

    /// <summary>
    /// How many lives left. Death on 0.
    /// </summary>
    public int Lives {get;set;} = 3;

    /// <summary>
    /// Movements clock
    /// </summary>
    public int movementTick = 0;

    /// <summary>
    /// Size/radius of a unit in a grid, =pos +/- value
    /// </summary>
    private const double UNIT_SIZE = 20;

    public VecI pos;

    public void LoadStage(G g)
    {
        
    }

    public override void DrawBG(G g)
    {
        base.DrawBG(g);
        double age = g.state.map.age;
        int speed = g.settings.reduceMotion? 0 : 48;
        Vec offset = new(age * -speed, 0.0);
        Vector2 _ = new();
        if (bg is not null)
        {
            bg.Render(g, age, offset);
            return;
        }
        g.state.map.RenderBg(g, age, offset);
    }

    public override void Update(G g)  // Do gameplay here
    {
        base.Update(g);
        ReturnToMemoryIfDone(g);
    }

    /// <summary>
    /// Show dialogue for winning/losing
    /// </summary>
    /// <param name="g"></param>
    public void ReturnToMemoryIfDone(G g)
    {
        if (current is LisardGameState.gameover_confirm)
        {  // TODO
            g.state.ChangeRoute(() => Dialogue.MakeDialogueRouteOrSkip(g.state, "", OnDone.vault));
        }
        else if (current is LisardGameState.winner_confirm)
        {  // TODO
            g.state.ChangeRoute(() => Dialogue.MakeDialogueRouteOrSkip(g.state, "", OnDone.vault));
        }
    }

    /// <summary>
    /// Input rendering + hud rendering
    /// </summary>
    /// <param name="g"></param>
    public override void Render(G g)
    {
        base.Render(g);
        UIKey? uiKey = lastGpSelection;
        if (uiKey is not null)
        {
            if (Input.gamepadIsActiveInput)
            {
                Input.currentGpKey = uiKey;
            }
            lastGpSelection = null;
        }
    }

    /// <summary>
    /// Gameplay rendering
    /// </summary>
    /// <param name="g"></param>
    public override void RenderBehindCockpit(G g)
    {
        base.RenderBehindCockpit(g);
    }

    /// <summary>
    /// Physical input stuff
    /// </summary>
    /// <param name="g"></param>
    /// <param name="b"></param>
    public void OnInputPhase(G g, Box b)
    {
        (int x, int y)? i = GetDirection();
        if (i is not null)
        {
            DoAThing(i.Value);
        }
    }

    public void OnMouseDown(G g, Box b)
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Translates input into X or Y directional value, priority on the newest input while retaining the previous input to memory so that when the player releases the most recent input, the older held input is then used.
    /// </summary>
    /// <returns></returns>
    public (int x, int y)? GetDirection()
    {
        if (Input.GetKeyHeld(Keys.Right) || Input.GetKeyHeld(Keys.D) || Input.GetGpHeld(Btn.DpRight))
        {
            heldDirection[0]++;
        }
        else heldDirection[0] = 0;
        if (Input.GetKeyHeld(Keys.Left) || Input.GetKeyHeld(Keys.A) || Input.GetGpHeld(Btn.DpLeft))
        {
            heldDirection[1]++;
        }
        else heldDirection[1] = 0;
        if (Input.GetKeyHeld(Keys.Up) || Input.GetKeyHeld(Keys.W) || Input.GetGpHeld(Btn.DpUp))
        {
            heldDirection[2]++;
        }
        else heldDirection[2] = 0;
        if (Input.GetKeyHeld(Keys.Down) || Input.GetKeyHeld(Keys.S) || Input.GetGpHeld(Btn.DpDown))
        {
            heldDirection[3]++;
        }
        else heldDirection[3] = 0;

        int d = -1;
        int shortest = int.MaxValue;
        for (int i = 0; i < heldDirection.Length; i++)
        {
            if (heldDirection[i] > 0 && heldDirection[i] < shortest)
            {
                d = i;
                shortest = heldDirection[i];
            }
        }
        return d switch
        {
            0 => (-1, 0),
            1 => (1, 0),
            2 => (0, -1),
            3 => (0, 1),
            _ => null
        };
    }

    /// <summary>
    /// Does things according to input
    /// </summary>
    /// <param name="dir"></param>
    public void DoAThing((int x, int y) dir)
    {
        
    }
}