using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(Finish), menuName = nameof(unity1week202303) + "/" + nameof(CommandEffectBase) + "/" + nameof(Finish))]
    public class Finish : CommandEffectBase
    {
        public override async UniTask InvokeAsync(CommandArg arg, CancellationToken token)
        {
            arg.FinishFlag.Value = true;

            await UniTask.CompletedTask;
        }

        // public override bool IsInvokable(CommandArg arg)
        // {
        //     return arg.Faith.IsFilled;
        // }
    }
}