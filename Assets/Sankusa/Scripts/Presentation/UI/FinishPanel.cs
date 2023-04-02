using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Components;
using Doozy.Runtime.UIManager.Containers;
using UnityEngine.SceneManagement;
using SankusaLib;

namespace Sankusa.unity1week202303.Presentation
{
    public class FinishPanel : MonoBehaviour
    {
        [SerializeField] private UIView uiView;
        [SerializeField] private UIButton titlebutton;

        void Start()
        {
            titlebutton.AddListenerToPointerClick(() =>
            {
                Blackout.Instance.PlayBlackout(1, () =>
                {
                    SceneManager.LoadScene("TitleScene");
                });
            });
        }

        public void Show()
        {
            uiView.Show();
        }
    }
}