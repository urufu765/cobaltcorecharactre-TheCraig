namespace Illeana.API;

/// <summary>
/// An interface for BG that allows the conversation to automatically advance, for scripted timed dialogue.
/// </summary>
public interface ICanAutoAdvanceDialogue
{
    /// <summary>
    /// If set to true, auto advance dialogue.
    /// </summary>
    /// <returns></returns>
    public bool AutoAdvanceDialogue();
}