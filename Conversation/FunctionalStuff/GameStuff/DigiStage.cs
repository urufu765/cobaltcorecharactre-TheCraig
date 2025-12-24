using System.Collections.Generic;
using System.Linq;
using Illeana.Features;
using Microsoft.Extensions.Logging;

namespace Illeana.Conversation;


public enum DigiThing
{
    Empty,
    Wall,
    Start,
    Finish,
    Enemy,
}


public abstract class DigiStage
{
    public required (int x, int y) Size;
    public required VecI startPos;
    public required VecI endPos;
    public required (int x, int y, int width, int height)[] walls;
    public required VecI[] enemyPos;

    public List<List<DigiThing>> Load()
    {
        List<List<DigiThing>> things = [];
        for (int row = 0; row < Size.y; row++)
        {
            List<DigiThing> digis = [];
            for (int col = 0; col < Size.x; col++)
            {
                digis.Add(DigiThing.Empty);
            }
            things.Add([.. digis]);
        }
        foreach ((int x, int y, int width, int height) wall in walls)
        {
            for (int y = wall.y; y < wall.y + wall.height; y++)
            {
                for (int x = wall.x; x < wall.x + wall.width; x++)
                {
                    if (things[y][x] is DigiThing.Empty)
                    {
                        things[y][x] = DigiThing.Wall;
                    }
                    else
                    {
                        ModEntry.Instance.Logger.LogWarning("Attempted to replace an existing cell with wall?!");
                    }
                }
            }
        }
        foreach (VecI e in enemyPos)
        {
            if (things[e.y][e.x] is DigiThing.Empty)
            {
                things[e.y][e.x] = DigiThing.Enemy;
            }
            else
            {
                ModEntry.Instance.Logger.LogWarning("Attempted to replace an existing cell with enemy?!");
            }
        }
        if (things[startPos.y][startPos.x] is not DigiThing.Empty)
        {
            ModEntry.Instance.Logger.LogWarning("Replaced an existing cell with starting pos!");
        }
        things[startPos.y][startPos.x] = DigiThing.Start;
        if (things[endPos.y][endPos.x] is not DigiThing.Empty)
        {
            ModEntry.Instance.Logger.LogWarning("Replaced an existing cell with ending pos!");
        }
        things[endPos.y][endPos.x] = DigiThing.Finish;

        return things;
    }
}