using System.Linq;
using Nickel;

namespace Illeana.Dialogue;

static class CommonDefinitions
{
    public static ModEntry Instance => ModEntry.Instance;

    public static string AmIlleana => Instance.IlleanaDeck.UniqueName;
    
    public static string AmCraig => "craig";  // Change to deck uniquename

    public const string AmUnknown = "johndoe";

    public const string AmCat = "comp";

    public static string AmDizzy => Deck.dizzy.Key();

    public const string AmVoid = "void";


    /// <summary>
    /// Safety checks if specific illeana animation exists, provides a placeholder if false
    /// </summary>
    /// <param name="loopTag">The Looptag of the animation</param>
    /// <returns>a valid looptag</returns>
    public static string Check(this string loopTag)
    {
        if (ModEntry.IlleanaAnims.Contains(loopTag))
        {
            return loopTag;
        }
        return "placeholder";
    }
}