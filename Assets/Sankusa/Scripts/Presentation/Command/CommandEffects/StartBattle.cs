using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(StartBattle), menuName = nameof(unity1week202303) + "/" + nameof(CommandEffectBase) + "/" + nameof(StartBattle))]
    public class StartBattle : CommandEffectBase
    {
        public override async UniTask InvokeAsync(CommandArg arg, CancellationToken token)
        {
            arg.BattleManager.StartBattle(arg.User, arg.User.GetHumanComponent<HumanTargeter>().Closest);
            arg.BattlePerformer.PlayBattleStartPerformance();

            await UniTask.CompletedTask;
        }

        public override bool IsInvokable(CommandArg arg)
        {
            return arg.User.GetComponent<HumanTargeter>().Closest != null;
        }
    }
}