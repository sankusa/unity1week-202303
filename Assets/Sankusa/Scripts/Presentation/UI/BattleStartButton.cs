using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Components;
using Sankusa.unity1week202303.Domain;
using Zenject;
using UniRx;
using SankusaLib;

namespace Sankusa.unity1week202303.Presentation
{
    public class BattleStartButton : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [Inject] private HumanManager humanManager;
        [Inject] private BattleManager battleManager;
        private HumanCore playerHumanCore;

        void Start()
        {
            playerHumanCore = humanManager.GetPlayerHumanCore();

            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    gameObject.SetActive(playerHumanCore.Human.State == HumanState.Free && playerHumanCore.GetHumanComponent<HumanTargeter>().Closest != null);
                })
                .AddTo(this);

            button.AddListenerToPointerClick(() => battleManager.StartBattle(playerHumanCore, playerHumanCore.GetHumanComponent<HumanTargeter>().Closest));
        }
    }
}