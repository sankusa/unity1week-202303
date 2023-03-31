using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Runtime.UIManager.Components;
using Sankusa.unity1week202303.Domain;
using SankusaLib;
using Zenject;
using UniRx;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Sankusa.unity1week202303.Presentation
{
    public class CommandButton : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [SerializeField] private TMP_Text label;
        [Inject] private CommandMaster commandMaster;
        [Inject] private CommandInvoker commandInvoker;
        private HumanCore commandUser;
        private Command command;

        void Start()
        {
            button.AddListenerToPointerClick(() =>
            {
                // コマンド実行
                commandInvoker.InvokeCommandAsync(commandUser, command.CommandId).Forget();
            });

            button.AddListenerToPointerEnter(() =>
            {
                Debug.Log(command.Name);
            });
            button.AddListenerToPointerExit(() =>
            {
                Debug.Log(command.Name);
            });

            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    Repaint();
                })
                .AddTo(this);
        }

        public void Initialize(CommandInvoker commandInvoker)
        {
            this.commandInvoker = commandInvoker;
        }

        public void SetValue(HumanCore commandUser, string commandId)
        {
            this.commandUser = commandUser;
            command = commandMaster.FindByCommandId(commandId);
        }

        private void Repaint()
        {
            if(command == null)
            {
                label.text = "-";
                return;
            }
            
            label.text = command.Name;
            button.interactable = commandInvoker.IsInvokable(commandUser, command.CommandId);
        }
    }
}