using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading;
using UnityEngine;
using Sankusa.unity1week202303.Domain;
using System.Linq;
using DG.Tweening;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(VillagerBig), menuName = nameof(unity1week202303) + "/" + nameof(CommandEffectBase) + "/" + nameof(VillagerBig))]
    public class VillagerBig : CommandEffectBase
    {
        [SerializeField] private string message;

        public override async UniTask InvokeAsync(CommandArg arg, CancellationToken token)
        {
            HumanManager humanManager = arg.DiContainer.Resolve<HumanManager>();
            HumanCore target = humanManager.HumanCores.ToList().Find(x => x.GetHumanComponent<NPC_Villager>() != null);
            Tweener tweener = target.transform.DOScale(target.transform.localScale.x + 1, 1);
            await target.GetHumanComponent<HumanTextEffectGenerator>().GenerateTalkTextEffect("うわあああああ！？");
            await UniTask.WaitUntil(() => !tweener.IsActive() || !tweener.IsPlaying());
        }
    }
}