using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Sankusa.unity1week202303.Domain;
using System.Linq;
using Zenject;

namespace Sankusa.unity1week202303.Presentation
{
    public class NPC_Old : NPCBase
    {
        [Inject] private HumanManager humanManager;

        public override IReadOnlyList<string> TemporaryCommands
        {
            get
            {
                List<string> commands = new List<string>();
                commands.AddRange(temporaryCommands);
                if(talkCount > 4)
                {
                    commands.Add(CommandId.VillagerBig);
                }
                return commands;
            }
        }
        private int talkCount;

        public override async UniTask ReactionAsync(CommandArg commandArg)
        {
            if(commandArg.CommandId == CommandId.Greeting)
            {
                await humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("無駄じゃ");
            }
            else if(commandArg.CommandId == CommandId.Punch)
            {
                humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("やめるんじゃ").Forget();
            }
            else if(commandArg.CommandId == CommandId.StartBattle)
            {
                humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("オヌシは・・・").Forget();
            }
            else if(commandArg.CommandId == CommandId.Inform)
            {
                await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("我が定めか・・・");
                
                commandArg.Target.Human.AddThoughtValue(PlayerSetting.targetname, 9999);
                await UniTask.Delay(1000);

                await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect(PlayerSetting.targetname + " は最高です!");
            }
            else if(commandArg.CommandId == CommandId.Talk)
            {
                if(talkCount == 0)
                {
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("完成しなかったんじゃ・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("この世界は・・・", 2);
                }
                else if(talkCount == 1)
                {
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("元々はオヌシが好きなものを", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("ワレワレに「つたえる」", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("そういう世界だったんじゃ・・・", 2);
                }
                else if(talkCount == 2)
                {
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("しかし完成しなかった", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("代わりにこの世界の創造主が", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("いかに愚かであったかをワシが「つたえる」", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("そんな世界になってしまったんじゃ", 2);
                }
                else if(talkCount == 3)
                {
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("創造主は愚かじゃった・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("計画性が無かったんじゃ・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("色々なコマンドを使って住民と", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("交流できるようにしようとしたんじゃが", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("そんなの1週間で作れるわけないじゃろ・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("我らの創造主は馬鹿じゃ・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("見通しが甘いんじゃ・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("それ以前に完成しても多分あんまり面白くないんじゃ・・・", 3);
                }
                else if(talkCount == 4)
                {
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("でも設計は頑張ったんじゃ・・・");
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("拡張性とか、そういうの・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("コマンドの自由度とか・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("村人を大きくするコマンドとかもあるぞ", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("使えるようにしたから使ってみるんじゃ・・・", 2);
                }
                else if(talkCount == 5)
                {
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("だが結局", 1);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("ゲームとしてうまくまとめられなかったんじゃ・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("全ては視野の狭さが招いた結果じゃ・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("余りにも愚かじゃ・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("クリアするコマンドを使えるようにしたから", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("それでクリアしてくれ・・・", 2);
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("さらばじゃ・・・", 2);
                    humanManager.GetPlayerHumanCore().Human.AddUsableCommandId(CommandId.Finish);
                }
                else if(talkCount > 5)
                {
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("もう何もないぞ・・・");
                }

                talkCount++;
            }
            await UniTask.CompletedTask;
        }
    }
}