using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Doozy.Runtime.UIManager.Components;
using SankusaLib;
using SankusaLib.SoundLib;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class TitleSceneManager : MonoBehaviour
    {
        [SerializeField] private UIButton startButton;
        [SerializeField] private Text nameText;
        [SerializeField] private Text targetText;
        [SerializeField, SoundId] private string bgmId;

        void Start()
        {
            startButton.AddListenerToPointerClick(LoadInGameScene);
            SoundManager.Instance.CrossFadeBgm(bgmId);
        }

        private void LoadInGameScene()
        {
            PlayerSetting.playerName = nameText.text;
            PlayerSetting.targetname = targetText.text;
            
            Blackout.Instance.PlayBlackout(1, () =>
            {
                SceneManager.LoadScene("InGameScene");
            });
        }
    }
}