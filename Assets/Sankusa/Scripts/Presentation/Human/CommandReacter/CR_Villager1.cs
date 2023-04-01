using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class CR_Villager1 : CommandReacterBase
    {
        public override async UniTask ReactionAsync(CommandArg commandArg)
        {
            Debug.Log("XX");
            if(commandArg.CommandId == CommandId.Greeting)
            {
                humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("こんにちは・・・");
            }
            else
            {
                humanCore.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("・・・");
            }
            await UniTask.CompletedTask;
        }
    }
}