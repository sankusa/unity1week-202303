using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;
using UniRx;
using SankusaLib;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;
using SankusaLib.SoundLib;

namespace Sankusa.unity1week202303.Presentation
{
    public class HumanTextEffectGenerator : HumanComponentBase
    {
        [SerializeField] private Transform effectGeneratePositionMarker;

        public override void Initialize(HumanCore humanCore)
        {
            base.Initialize(humanCore);

            humanCore
                .Human
                .OnParameterValueChanged
                .Subscribe(x =>
                {
                    TextEffect textEffect = Instantiate(EffectPrefabMaster.Instance.ParameterTextEffectPrefab, effectGeneratePositionMarker.position, Quaternion.identity);
                    textEffect.Text = x.Item1.Data.DisplayName + " " + x.Item2.ToString("+#;-#;");
                })
                .AddTo(this);

            humanCore
                .Human
                .OnThoughtValueChanged
                .Subscribe(x =>
                {
                    TextEffect textEffect = Instantiate(EffectPrefabMaster.Instance.ThoughtTextEffectPrefab, effectGeneratePositionMarker.position, Quaternion.identity);
                    textEffect.Text = x.Item1.Name + " " + x.Item2.ToString("+#;-#;");
                    SoundManager.Instance.PlaySe(SoundId.SE_Thought);
                })
                .AddTo(this);
        }

        public async UniTask GenerateTalkTextEffect(string message, float duration = 1f)
        {
            TextEffect textEffect = Instantiate(EffectPrefabMaster.Instance.TalkTextEffectPrefab, effectGeneratePositionMarker.position, Quaternion.identity);
            textEffect.Duration = duration;
            textEffect.Text = message;

            await UniTask.Delay(TimeSpan.FromSeconds(duration));
        }
    }
}