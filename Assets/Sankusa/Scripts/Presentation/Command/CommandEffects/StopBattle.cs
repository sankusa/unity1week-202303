using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(StopBattle), menuName = nameof(unity1week202303) + "/" + nameof(CommandEffectBase) + "/" + nameof(StopBattle))]
    public class StopBattle : CommandEffectBase
    {
        public override async UniTask InvokeAsync(CommandArg arg, CancellationToken token)
        {
            arg.BattleManager.StopBattle(arg.User);
            arg.BattlePerformer.PlayBattleStopPerformance();

            await UniTask.CompletedTask;
        }
    }
}