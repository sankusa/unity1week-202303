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
                await arg.User.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect(PlayerSetting.targetname + " はいいぞ");
            }
        }
    }
}