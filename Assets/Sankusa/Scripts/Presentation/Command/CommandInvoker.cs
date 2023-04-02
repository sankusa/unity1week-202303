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
        private readonly DiContainer diContainer;
        private readonly CancellationTokenSource source = new CancellationTokenSource();

        [Inject]
        public CommandInvoker(Faith faith, GameTimer gameTimer, FinishFlag finishFlag, HumanManager humanManager, BattleManager battleManager, CommandMaster commandMaster, BattlePerformer battlePerformer, InGameCamera inGameCamera, DiContainer diContainer)
        {
            this.faith = faith;
            this.gameTimer = gameTimer;
            this.finishFlag = finishFlag;
            this.humanManager = humanManager;
            this.battleManager = battleManager;
            this.commandMaster = commandMaster;
            this.battlePerformer = battlePerformer;
            this.inGameCamera = inGameCamera;
            this.diContainer = diContainer;
        }

        public async UniTask InvokeCommandAsync(HumanCore user, string commandId)
        {
            user.Human.IsInvokingCommand = true;

            CommandArg arg = ConstructCommandArg(user, commandId);

            await commandMaster.FindByCommandId(arg.CommandId).InvokeAsync(arg, source.Token);

            NPCBase npc = user.Human.BattleTarget?.GetHumanComponent<NPCBase>();
            if(npc != null)
            {
                await npc.ReactionAsync(arg);
            }

            user.Human.BattleTarget?.Human.IncrementReceivedCommand(commandId);

            user.Human.IsInvokingCommand = false;

            if(npc != null)
            {
                await npc.ActAsync();
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
            arg.DiContainer = diContainer;
            return arg;
        }

        public void Dispose()
        {
            source.Cancel();
        }
    }
}