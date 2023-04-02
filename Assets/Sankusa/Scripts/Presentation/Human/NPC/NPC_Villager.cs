using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class NPC_Villager : NPCBase
    {
        private int informCount = 0;
        public override async UniTask ReactionAsync(CommandArg commandArg)
        {
            if(commandArg.CommandId == CommandId.Greeting)
            {
                if(humanCore.Human.SafeGetReceivedCommand(commandArg.CommandId) == 0)
                {
                    await humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("こんにちは");
                    humanCore.Human.FindParameter(HumanParameterId.Like).AddBaseValue(2);
                }
                else if(humanCore.Human.SafeGetReceivedCommand(commandArg.CommandId) == 1)
                {
                    await humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("はいはい、こんにちは");
                    humanCore.Human.FindParameter(HumanParameterId.Like).AddBaseValue(1);
                }
                else if(humanCore.Human.SafeGetReceivedCommand(commandArg.CommandId) == 2)
                {
                    await humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("? こんにちは");
                }
                else if(humanCore.Human.SafeGetReceivedCommand(commandArg.CommandId) == 3)
                {
                    await humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("こ、こんにちは");
                }
                else if(humanCore.Human.SafeGetReceivedCommand(commandArg.CommandId) >= 4)
                {
                    await humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・");
                    humanCore.Human.FindParameter(HumanParameterId.Like).AddBaseValue(-1);
                }
            }
            else if(commandArg.CommandId == CommandId.Punch)
            {
                humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("いたい").Forget();
                await UniTask.Delay(300);
                humanCore.Human.FindParameter(HumanParameterId.Like).AddBaseValue(-5);
                await UniTask.Delay(700);
            }
            else if(commandArg.CommandId == CommandId.StartBattle)
            {
                humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("旅の人かな？").Forget();
            }
            else if(commandArg.CommandId == CommandId.Inform)
            {
                HumanParameter like = humanCore.Human.FindParameter(HumanParameterId.Like);
                HumanParameter hp = humanCore.Human.FindParameter(HumanParameterId.HP);
                if(hp.Value == 0)
                {
                    await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("ヒィ、" + PlayerSetting.targetname + "は最高ですぅ。許してくださいぃ。");
                    commandArg.Target.Human.AddThoughtValue(PlayerSetting.targetname, 1000);
                }
                else
                {
                    if(like.Value > 0)
                    {
                        if(informCount == 0)
                        {
                            await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("なるほど");
                        }
                        else if(informCount == 1)
                        {
                            await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("へぇ");
                        }
                        else if(informCount >= 2)
                        {
                            await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect(PlayerSetting.targetname + "ってすばらしいね");
                        }
                        
                        commandArg.Target.Human.AddThoughtValue(PlayerSetting.targetname, like.Value);
                        informCount++;
                    }
                    else if(like.Value == 0)
                    {
                        await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("いきなり言われても・・・");
                    }
                    else
                    {
                        await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("どうかしてるんじゃないですか？");
                    }
                }
            }
            await UniTask.CompletedTask;
        }

        public override async UniTask ActAsync()
        {
            if(humanCore.Human.FindParameter(HumanParameterId.HP).Value == 0)
            {
                await humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("許して・・・");
            }
            else
            {
                if(humanCore.Human.FindParameter(HumanParameterId.Like).Value < 0)
                {
                    humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("許さんぞ貴様").Forget();
                    await commandInvoker.InvokeCommandAsync(humanCore, CommandId.Punch);
                }
                await UniTask.Delay(1000, cancellationToken: source.Token);
            }
        }
    }
}