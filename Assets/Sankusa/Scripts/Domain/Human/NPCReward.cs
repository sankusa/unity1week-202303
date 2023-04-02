using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Zenject;

namespace Sankusa.unity1week202303.Domain
{
    public class NPCReward : HumanComponentBase
    {
        [SerializeField, CommandId] private List<string> rewardCommands;
        public IReadOnlyList<string> RewardCommands => rewardCommands;
        [Inject] private HumanManager humanManager;

        public override void Initialize(HumanCore humanCore)
        {
            base.Initialize(humanCore);
            humanCore.Human.OnBrainwashed
                .Subscribe(_ =>
                {
                    rewardCommands.ForEach(x => humanManager.GetPlayerHumanCore().Human.AddUsableCommandId(x));
                })
                .AddTo(this);
        }
    }
}