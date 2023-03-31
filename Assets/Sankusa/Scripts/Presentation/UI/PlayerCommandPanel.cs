using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Doozy.Runtime.UIManager.Containers;
using Doozy.Runtime.UIManager.Components;
using TMPro;
using UniRx;
using Zenject;
using Sankusa.unity1week202303.Domain;
using System.Linq;
using SankusaLib;

namespace Sankusa.unity1week202303.Presentation
{
    public class PlayerCommandPanel : MonoBehaviour
    {
        [SerializeField] private UIView uiView;
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private Image background;
        [SerializeField] private GridLayoutGroup grid;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private CommandButton commandButtonPrefab;
        [Inject] private BattleManager battleManager;
        [Inject] private HumanManager humanManager;
        [Inject] private DiContainer container;
        private HumanCore playerHumanCore;
        

        void Start()
        {
            // Playerをセット
            playerHumanCore = humanManager.GetPlayerHumanCore();

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
            gameObject.SetActive(!playerHumanCore.Human.IsInvokingCommand);

            if(battleManager.Battles.Where(x => x.Joiners.Where(y => y == playerHumanCore).FirstOrDefault() != null).FirstOrDefault() != null)
            {
                titleText.text = "布教コマンド";
                ColorUtility.TryParseHtmlString("#FF4EB5", out Color titleColor);
                titleText.color = titleColor;
                background.color = new Color(1f, 0.25f, 0.7f, 0.58f);
            }
            else
            {
                titleText.text = "フィールドコマンド";
                titleText.color = Color.white;
                background.color = new Color(0f, 0f, 0f, 0.58f);
            }

            List<Command> invokableCommands = null;
            if(playerHumanCore.Human.State == HumanState.Free)
            {
                invokableCommands = playerHumanCore.Human.UsableCommandIdList
                    .Select(x => CommandMaster.Instance.FindByCommandId(x))
                    .Where(x => x.Timing == CommandTiming.Field || x.Timing == CommandTiming.FieldAndBattle)
                    .ToList();
            }
            else if(playerHumanCore.Human.State == HumanState.Battle)
            {
                invokableCommands = playerHumanCore.Human.UsableCommandIdList
                    .Select(x => CommandMaster.Instance.FindByCommandId(x))
                    .Where(x => x.Timing == CommandTiming.Battle || x.Timing == CommandTiming.FieldAndBattle)
                    .ToList();
            }

            List<CommandButton> commandButtons = TransformUtil.AdjustChildCount<CommandButton>(container, grid.transform, commandButtonPrefab.gameObject, invokableCommands.Count);

            for(int i = 0; i < invokableCommands.Count; i++)
            {
                commandButtons[i].SetValue(playerHumanCore, invokableCommands[i].CommandId);
            }
        }
    }
}