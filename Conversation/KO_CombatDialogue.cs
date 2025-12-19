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
            {"WeAreTarnished", new(){
                type = NodeType.combat,
                oncePerRun = true,
                allPresent = [ AmIsaac ],
                lastTurnPlayerStatuses = [ Tarnished ],
                dialogue = [
                    new(new QMulti()),
                    new(AmIsaac, "panic", "이거 문재가 될것 같은데..."),
                    new(AmIlleana, "sly", "그냥 안맞으면 되잖아."),

                    new(new QMulti([ AmPeri, AmIlleana ])),
                    new(AmPeri, "mad", "야! 지금 뭐하는거야?!"),
                    new(AmIlleana, "silly", "최선 다하고 있습니다!"),

                    new(new QMulti([AmPeri])),
                    new(AmPeri, "mad", "우리 이제 맞으면 큰일나."),
                    new([
                        new(AmIlleana, "intense", "물건 밖으로 던지면 더 잘 피할수도!"),
                        new(AmIlleana, "sly", "걍 맞지마."),
                        new(AmIlleana, "아니, 내가 그냥 고치면 되잖아.")
                    ]),

                    new(new QMulti([AmDrake])),
                    new(AmDrake, "panic", "아, 온도 높여도 문재가 안 없어지잖아..."),

                    new(new QMulti([AmDizzy])),
                    new(AmDizzy, "squint", "셔틀이 산산조각 나고 있어."),

                    new(new QMulti([AmCat])),
                    new(AmCat, "squint", "우리 빨리 튀어야 되!")
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
