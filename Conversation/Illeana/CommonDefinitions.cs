namespace Illeana.Dialogue;

static class CommonDefinitions
{
    public static ModEntry Instance => ModEntry.Instance;

    public static string AmIlleana => Instance.IlleanaDeck.UniqueName;

    public const string Cat = "comp";

    public static string AmDizzy => Deck.dizzy.Key();
}