using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(None), menuName = nameof(unity1week202303) + "/" + nameof(CommandEffectBase) + "/" + nameof(None))]
    public class None : CommandEffectBase
    {
        [SerializeField] private string message;

        public override async UniTask InvokeAsync(CommandArg arg, CancellationToken token)
        {
            await UniTask.CompletedTask;
        }
    }
}