using System.Linq;
using Microsoft.Extensions.Logging;
using Nickel;
using static Illeana.Conversation.CommonDefinitions;
using HarmonyLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Nanoray.Shrike;
using Nanoray.Shrike.Harmony;

namespace Illeana.Conversation;

public static class DialogueDrawShift
{
    public static void Apply(Harmony harmony)
    {
        harmony.Patch(
            original: typeof(Dialogue).GetMethod(nameof(Dialogue.Render), AccessTools.all),
            transpiler: new HarmonyMethod(typeof(DialogueDrawShift), nameof(ShiftDialogueToTheRight))
        );
        harmony.Patch(
            original: typeof(Character).GetMethod(nameof(Character.DrawFace), AccessTools.all),
            postfix: new HarmonyMethod(typeof(DialogueDrawShift), nameof(ExtraFX))
        );
    }

    private static void ExtraFX(Character __instance, G g, double x, double y, bool flipX, string animTag, double animationFrame, bool mini = false, bool? isSelected = null, bool renderLocked = false, bool hideFace = false)
    {
        if (__instance.type == ModEntry.LisardEXE.CharacterType && animTag == "static")
        {
            int noiseIndex = (int)Mutil.Mod(g.state.time * 12 + x + y, Character.noiseSprites.Count);
            Draw.Sprite(
                Character.noiseSprites[noiseIndex],
                x + 4,
                y + 1,
                color: Colors.textMain
            );
        }
        if (g.state.GetDialogue()?.bg is BGShipShambles shambles)
        {
            Draw.Fill(Colors.white.fadeAlpha(shambles.Flashbang));
            BGComponents.Letterbox();
            Draw.Fill(new Color(0.25, 0.5, 1).gain(shambles.Whitening), blend: BlendMode.Screen);
            Draw.Fill(Colors.white.fadeAlpha(shambles.Whitening));
        }
    }

    public static IEnumerable<CodeInstruction> ShiftDialogueToTheRight(IEnumerable<CodeInstruction> instructions, ILGenerator il)
    {
        try
        {
            return new SequenceBlockMatcher<CodeInstruction>(instructions)
                .Find(SequenceBlockMatcherFindOccurence.First,
                SequenceMatcherRelativeBounds.WholeSequence,
                ILMatches.AnyLdloc.GetLocalIndex(out StructRef<int> i),
                ILMatches.Ldstr("Eunice_Memory_3"),
                ILMatches.AnyCall,
                ILMatches.Brtrue,
                ILMatches.Br
                ).Find(SequenceBlockMatcherFindOccurence.First,
                SequenceMatcherRelativeBounds.After,
                ILMatches.AnyLdcI4,
                ILMatches.AnyStloc.GetLocalIndex(out StructRef<int> j),
                ILMatches.Br
                ).Find(SequenceBlockMatcherFindOccurence.First,
                SequenceMatcherRelativeBounds.After,
                ILMatches.AnyLdloc,
                ILMatches.AnyStloc
                ).Insert(SequenceMatcherPastBoundsDirection.Before,
                SequenceMatcherInsertionResultingBounds.JustInsertion,
                [
                    new(OpCodes.Ldloc_S, i.Value),
                    new(OpCodes.Call, AccessTools.DeclaredMethod(typeof(DialogueDrawShift), nameof(MoveDialogueIfIlleanaDialogue))),
                    new(OpCodes.Stloc_S, j.Value)
                ]).AllElements();
        }
        catch (Exception err)
        {
            ModEntry.Instance.Logger.LogError(err, "Couldn't shift dialogue oho");
            throw;
        }
    }

    private static int MoveDialogueIfIlleanaDialogue(string toCompare)
    {
        if (toCompare == "Illeana_Memory_2") return 90;
        return 0;
    }
}