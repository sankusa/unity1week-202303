using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class TargetStatusPanel : MonoBehaviour
    {
        [SerializeField] private HumanStatusPanel humanStatusPanel;
        [Inject] private HumanManager humanManager;
        private HumanTargeter playerTargeter;
        
        void Start()
        {
            playerTargeter = humanManager.GetPlayerHumanCore().GetHumanComponent<HumanTargeter>();
        }

        void Update()
        {
            humanStatusPanel.SetModel(playerTargeter.Closest?.Human);
        }
    }
}