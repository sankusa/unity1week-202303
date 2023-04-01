using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace Sankusa.unity1week202303.Presentation
{
    public class CommandInvoker : IDisposable
    {
        private readonly Faith faith;
        private readonly GameTimer gameTimer;
        private readonly FinishFlag finishFlag;
        private readonly HumanManager humanManager;
        private readonly BattleManager battleManager;
        private readonly CommandMaster commandMaster;
        private readonly BattlePerformer battlePerformer;
        private readonly InGameCamera inGameCamera;
        private readonly CancellationTokenSource source = new CancellationTokenSource();

        [Inject]
        public CommandInvoker(Faith faith, GameTimer gameTimer, FinishFlag finishFlag, HumanManager humanManager, BattleManager battleManager, CommandMaster commandMaster, BattlePerformer battlePerformer, InGameCamera inGameCamera)
        {
            this.faith = faith;
            this.gameTimer = gameTimer;
            this.finishFlag = finishFlag;
            this.humanManager = humanManager;
            this.battleManager = battleManager;
            this.commandMaster = commandMaster;
            this.battlePerformer = battlePerformer;
            this.inGameCamera = inGameCamera;
        }

        public async UniTaskVoid InvokeCommandAsync(HumanCore user, string commandId)
        {
            CommandArg arg = ConstructCommandArg(user, commandId);

            await commandMaster.FindByCommandId(arg.CommandId).InvokeAsync(arg, source.Token);

            CommandReacterBase reacter = user.Human.BattleTarget?.GetHumanComponent<CommandReacterBase>();
            if(reacter != null)
            {
                await reacter.ReactionAsync(arg);
            }
        }

        public bool IsInvokable(HumanCore user, string commandId)
        {
            CommandArg arg = ConstructCommandArg(user, commandId);

            return commandMaster.FindByCommandId(arg.CommandId).IsInvokable(arg);
        }

        private CommandArg ConstructCommandArg(HumanCore user, string commandId)
        {
            CommandArg arg = new CommandArg();
            arg.BattleManager = battleManager;
            arg.CommandId = commandId;
            arg.User = user;
            arg.Target = user.Human.BattleTarget;
            arg.Faith = faith;
            arg.GameTimer = gameTimer;
            arg.FinishFlag = finishFlag;
            arg.BattlePerformer = battlePerformer;
            arg.InGameCamera = inGameCamera;
            return arg;
        }

        public void Dispose()
        {
            source.Cancel();
        }
    }
}