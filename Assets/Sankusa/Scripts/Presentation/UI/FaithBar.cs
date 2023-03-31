using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Doozy.Runtime.UIManager.Components;
using Sankusa.unity1week202303.Domain;
using UniRx;

namespace Sankusa.unity1week202303.Presentation
{
    public class FaithBar : MonoBehaviour
    {
        [SerializeField] private UISlider slider;
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
            slider.value = faith.Value / faith.Max;
        }
    }
}