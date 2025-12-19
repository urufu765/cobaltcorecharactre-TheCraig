using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Nanoray.PluginManager;
using Nickel;
using Illeana.Artifacts;
using Illeana.External;
using static Illeana.Conversation.CommonDefinitions;

namespace Illeana.Conversation;

/// <summary>
/// For DialogueMachine's multilingual feature demonstration
/// </summary>
internal class KO_CombatDialogue : IRegisterable
{
    public static void Register(IPluginPackage<IModManifest> package, IModHelper helper)
    {
        LocalDB.DumpStoryToLocalLocale("ko", new Dictionary<string, DialogueMachine>(){
            {"ThatsALotOfDamageToUs_Illeana", new(){
                type = NodeType.combat,
                enemyShotJustHit = true,
                minDamageDealtToPlayerThisTurn = 3,
                allPresent = [ AmIlleana ],
                dialogue = [
                    new(new QMulti()),
                    new(AmIlleana, "panic", "아악! 잠시! 퍼우스!!!"),

                    new(new QMulti()),
                    new(AmIlleana, "shocked", "나 이거 못 고칠 것 같은걸..."),

                    new(new QMulti()),
                    new(AmIlleana, "intense", "기-기다려봐, 나 이거 고칠 수 있어... 잠시만 기다려봐!")
                ]
            }},
            {"WeAreCorroded_Multi_0", new(){
                dialogue = [
                    new(),
                    new(AmIlleana, "intense", "잠깐만! 금방 고칠게.")
                ]
            }},
            {"BanditThreats_Multi_0", new(){
                edit = [
                    new(EMod.countFromStart, 1, AmIlleana, "panic", "저기요?! 주문 잘못된 것 같은데요...")
                ]
            }},
        });
    }
}
