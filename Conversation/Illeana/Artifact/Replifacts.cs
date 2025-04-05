using System;
using Illeana.Artifacts;
using Microsoft.Extensions.Logging;
using static Illeana.Dialogue.CommonDefinitions;

namespace Illeana.Dialogue;

internal static partial class ArtifactDialogue
{
    private static void Replies()
    {
        try
        {
            DB.story.all["ArtifactShieldPrepIsGone_Multi_0"].doesNotHaveArtifacts?.Add(
                "WarpPrototype".F()
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone0");
        }
        try
        {
            DB.story.all["ArtifactShieldPrepIsGone_Multi_1"].doesNotHaveArtifacts?.Add(
                "WarpPrototype".F()
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone1");
        }
        try
        {
            DB.story.all["ArtifactShieldPrepIsGone_Multi_2"].doesNotHaveArtifacts?.Add(
                "WarpPrototype".F()
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone2");
        }
        try
        {
            DB.story.all["ArtifactShieldPrepIsGone_Multi_3"].doesNotHaveArtifacts?.Add(
                "WarpPrototype".F()
            );
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Failed to add condition to ShieldPrepIsGone3");
        }
    }
}