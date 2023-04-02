using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;
using Zenject;

namespace Sankusa.unity1week202303.Presentation
{
    public class CommandArg
    {
        private HumanCore user;
        public HumanCore User
        {
            get => user;
            set => user = value;
        }

        private HumanCore target;
        public HumanCore Target
        {
            get => target;
            set => target = value;
        }

        private string commandId;
        public string CommandId
        {
            get => commandId;
            set => commandId = value;
        }

        private BattleManager battleManager;
        public BattleManager BattleManager
        {
            get => battleManager;
            set => battleManager = value;
        }

        private Faith faith;
        public Faith Faith
        {
            get => faith;
            set => faith = value;
        }

        private GameTimer gameTimer;
        public GameTimer GameTimer
        {
            get => gameTimer;
            set => gameTimer = value;
        }

        private FinishFlag finishFlag;
        public FinishFlag FinishFlag
        {
            get => finishFlag;
            set => finishFlag = value;
        }

        private BattlePerformer battlePerformer;
        public BattlePerformer BattlePerformer
        {
            get => battlePerformer;
            set => battlePerformer = value;
        }

        private InGameCamera inGameCamera;
        public InGameCamera InGameCamera
        {
            get => inGameCamera;
            set => inGameCamera = value;
        }

        private DiContainer diContainer;
        public DiContainer DiContainer
        {
            get => diContainer;
            set => diContainer = value;
        }
    }
}