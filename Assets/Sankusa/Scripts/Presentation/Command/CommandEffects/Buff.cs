using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(Buff), menuName = nameof(unity1week202303) + "/" + nameof(CommandEffectBase) + "/" + nameof(Buff))]
    public class Buff : CommandEffectBase
    {
        [SerializeField, HumanParameterId] private string parameterId;
        [SerializeField] private int value;

        public override async UniTask InvokeAsync(CommandArg arg, CancellationToken token)
        {
            await arg.User.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("ハアアァァァ!");
            HumanParameter parameter = arg.User.Human.FindParameter(parameterId);
            if(parameter == null)
            {
                parameter = new HumanParameter(parameterId, 0);
                arg.User.Human.AddParameter(parameter);
            }
            parameter.AddBaseValue(value);
        }
    }
}