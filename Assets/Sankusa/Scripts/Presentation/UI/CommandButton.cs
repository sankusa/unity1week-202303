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
using System;
using UnityEngine.UI;

namespace Sankusa.unity1week202303.Presentation
{
    public class CommandButton : MonoBehaviour
    {
        [SerializeField] private UIButton button;
        [SerializeField] private TMP_Text label;
        [SerializeField] private Image inactiveImage;
        [Inject] private CommandMaster commandMaster;
        [Inject] private CommandInvoker commandInvoker;
        private HumanCore commandUser;
        private Command command;

        private Subject<Command> onPointerEnter = new Subject<Command>();
        public IObservable<Command> OnPointerEnter => onPointerEnter;

        private Subject<Command> onPointerExit = new Subject<Command>();
        public IObservable<Command> OnPointerExit => onPointerExit;

        void Start()
        {
            button.AddListenerToPointerClick(() =>
            {
                if(commandInvoker.IsInvokable(commandUser, command.CommandId))
                {
                    // コマンド実行
                    commandInvoker.InvokeCommandAsync(commandUser, command.CommandId).Forget();
                }
            });

            button.AddListenerToPointerExit(() =>
            {
                onPointerExit.OnNext(command);
            });

            button.AddListenerToPointerEnter(() =>
            {
                onPointerEnter.OnNext(command);
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
            inactiveImage.enabled = !commandInvoker.IsInvokable(commandUser, command.CommandId);
        }
    }
}