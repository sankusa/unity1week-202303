using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(Inform), menuName = nameof(unity1week202303) + "/" + nameof(CommandEffectBase) + "/" + nameof(Inform))]
    public class Inform : CommandEffectBase
    {
        [SerializeField] private float thoughtRate;

        public override async UniTask InvokeAsync(CommandArg arg, CancellationToken token)
        {
            if(arg.User.Human.Thoughts.Count > 0)
            {
                await arg.User.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect(arg.User.Human.Thoughts[0].Name + " はいいぞ");
                await arg.Target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("なるほど");
                await UniTask.Delay(1000);
                arg.Target.Human.AddThoughtValue(arg.User.Human.Thoughts[0].Name, thoughtRate);
            }
        }
    }
}