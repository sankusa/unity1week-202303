using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using Sankusa.unity1week202303.Domain;
using UniRx;

namespace Sankusa.unity1week202303.Presentation
{
    public class FaithText : MonoBehaviour
    {
        [SerializeField] private TMP_Text faithText;
        [Inject] private Faith faith;

        void Start()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    Repaint();
                })
                .AddTo(this);
        }

        private void Repaint()
        {
            faithText.text = faith.Value.ToString("0.0") + "/" + faith.Max.ToString("0.0");
        }
    }
}