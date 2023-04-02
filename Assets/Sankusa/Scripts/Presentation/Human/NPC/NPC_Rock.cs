using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class NPC_Rock : NPCBase
    {
        private int informCount = 0;
        public override async UniTask ReactionAsync(CommandArg commandArg)
        {
            if(commandArg.CommandId == CommandId.Greeting)
            {
                if(humanCore.Human.SafeGetReceivedCommand(commandArg.CommandId) == 0)
                {
                    await humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・");
                    humanCore.Human.FindParameter(HumanParameterId.Like).AddBaseValue(2);
                }
            }
            else if(commandArg.CommandId == CommandId.Punch)
            {
                humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・").Forget();
            }
            else if(commandArg.CommandId == CommandId.StartBattle)
            {
                humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・").Forget();
            }
            else if(commandArg.CommandId == CommandId.Inform)
            {
                HumanParameter like = humanCore.Human.FindParameter(HumanParameterId.Like);
                await commandArg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・");
                commandArg.Target.Human.AddThoughtValue(PlayerSetting.targetname, 999);
                
            }
            await UniTask.CompletedTask;
        }
    }
}