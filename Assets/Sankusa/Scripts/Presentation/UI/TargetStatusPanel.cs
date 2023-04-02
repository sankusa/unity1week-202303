using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Sankusa.unity1week202303.Domain;
using TMPro;

namespace Sankusa.unity1week202303.Presentation
{
    public class TargetStatusPanel : MonoBehaviour
    {
        [SerializeField] private HumanStatusPanel humanStatusPanel;
        [SerializeField] private TMP_Text rewardText;
        [Inject] private HumanManager humanManager;
        [Inject] private CommandMaster commandMaster;
        private HumanTargeter playerTargeter;
        
        void Start()
        {
            playerTargeter = humanManager.GetPlayerHumanCore().GetHumanComponent<HumanTargeter>();
        }

        void Update()
        {
            HumanCore target = playerTargeter.Closest;

            humanStatusPanel.SetModel(target?.Human);
            if(target == null)
            {
                rewardText.text = "";
            }
            else
            {
                string rewardMessage = "";
                NPCReward reward = target.GetHumanComponent<NPCReward>();
                if(reward != null)
                {
                    foreach(string commandId in reward.RewardCommands)
                    {
                        rewardMessage += commandMaster.FindByCommandId(commandId).Name + "(コマンド)\n";
                    }
                    if(rewardMessage == "") rewardMessage = "なし";
                    rewardText.text = rewardMessage;
                }
            }
        }
    }
}