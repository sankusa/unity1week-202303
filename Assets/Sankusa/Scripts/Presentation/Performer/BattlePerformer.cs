using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Containers;
using  TMPro;
using Zenject;
using DG.Tweening;

namespace Sankusa.unity1week202303.Presentation
{
    public class BattlePerformer : MonoBehaviour
    {
        [SerializeField] private UIView uiView;
        [SerializeField] private TMP_Text mainText;
        [Inject] private InGameCamera inGameCamera;

        void Start()
        {
            mainText
                .DOFade(0.5f, 1f)
                .SetLoops(-1, LoopType.Yoyo)
                .Play()
                .SetLink(gameObject);

        }

        public void PlayBattleStartPerformance()
        {
            inGameCamera.Zoom(true);
            uiView.Show();
        }

        public void PlayBattleStopPerformance()
        {
            inGameCamera.Zoom(false);
            uiView.Hide();
        }
    }
}