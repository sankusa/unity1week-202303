using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(Talk), menuName = nameof(unity1week202303) + "/" + nameof(CommandEffectBase) + "/" + nameof(Talk))]
    public class Talk : CommandEffectBase
    {
        [SerializeField] private string message;

        public override async UniTask InvokeAsync(CommandArg arg, CancellationToken token)
        {
            await arg.User.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect(message);
        }
    }
}