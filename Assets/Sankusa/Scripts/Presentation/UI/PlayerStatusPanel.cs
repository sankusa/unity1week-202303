using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class PlayerStatusPanel : MonoBehaviour
    {
        [SerializeField] private HumanStatusPanel humanStatusPanel;
        [Inject] private HumanManager humanManager;
        
        void Start()
        {
            humanStatusPanel.SetModel(humanManager.GetPlayerHumanCore().Human);
        }
    }
}