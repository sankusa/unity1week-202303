using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Sankusa.unity1week202303.Domain;
using SankusaLib.SoundLib;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(Attack), menuName = nameof(unity1week202303) + "/" + nameof(CommandEffectBase) + "/" + nameof(Attack))]
    public class Attack : CommandEffectBase
    {
        [SerializeField] private float damageRate;
        public float DamageRate => damageRate;

        public override async UniTask InvokeAsync(CommandArg arg, CancellationToken token)
        {
            HumanParameter userAttack = arg.User.Human.FindParameter(HumanParameterId.Attack);
            HumanParameter targetBlock = arg.Target.Human.FindParameter(HumanParameterId.Block);
            HumanParameter targetHp = arg.Target.Human.FindParameter(HumanParameterId.HP);
            float userAttackValue = userAttack != null ? userAttack.Value : HumanParameterMaster.Instance.Find(HumanParameterId.Attack).Min;
            float userBlockValue = targetBlock != null ? targetBlock.Value : HumanParameterMaster.Instance.Find(HumanParameterId.Block).Min;
            if(userBlockValue == 0) userBlockValue = 1;
            int damage = (int)Mathf.Max(damageRate * userAttackValue / userBlockValue, 1);
            targetHp?.AddBaseValue(-damage);

            arg.InGameCamera.Shake();
            SoundManager.Instance.PlaySe(SoundId.SE_Attack);
            arg.Target.GetHumanComponent<HumanTextEffectGenerator>()?.GenerateTalkTextEffect("いたい");

            await UniTask.Yield();
        }
    }
}