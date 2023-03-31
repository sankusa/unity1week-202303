using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using Sankusa.unity1week202303.Domain;
using UniRx;
using System;

namespace Sankusa.unity1week202303.Presentation
{
    public class GameTimerText : MonoBehaviour
    {
        [SerializeField] private TMP_Text gameTimerText;
        [Inject] private GameTimer gameTimer;

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
            gameTimerText.text = TimeSpan.FromSeconds(gameTimer.RemainingTime).ToString(@"mm\:ss");
        }
    }
}