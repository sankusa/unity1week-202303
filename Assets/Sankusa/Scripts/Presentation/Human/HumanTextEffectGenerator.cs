using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;
using UniRx;
using SankusaLib;

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
                })
                .AddTo(this);
        }

        public void GenerateTalkTextEffect(string message)
        {
            TextEffect textEffect = Instantiate(EffectPrefabMaster.Instance.TalkTextEffectPrefab, effectGeneratePositionMarker.position, Quaternion.identity);
            textEffect.Text = message;
        }
    }
}