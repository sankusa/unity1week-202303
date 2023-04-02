using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UniRx;

namespace Sankusa.unity1week202303.Domain
{
    [Serializable]
    public class Human
    {
        private ReactiveProperty<bool> isPlayer = new ReactiveProperty<bool>(false);
        public bool IsPlayer
        {
            get => isPlayer.Value;
            set => isPlayer.Value = value;
        }
        public IObservable<bool> OnIsPlayerChanged => isPlayer;

        [SerializeField] private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }

        [SerializeField] private string description;
        public string Description => description;

        private ReactiveProperty<HumanState> state = new ReactiveProperty<HumanState>();
        public HumanState State => state.Value;
        public IObservable<HumanState> OnStateChanged => state;

        [NonSerialized] private ReactiveProperty<HumanCore> battleTarget = new ReactiveProperty<HumanCore>();
        public HumanCore BattleTarget => battleTarget.Value;
        public IObservable<HumanCore> OnBattleTargetChanged => battleTarget;

        // 信仰生産量
        [SerializeField] private float faithProduce;
        public float FaithProduce => faithProduce;

        // パラメータ
        [SerializeField, SimpleHorizontalDrawer] private List<HumanParameter> parameters = new List<HumanParameter>();
        public IReadOnlyList<HumanParameter> Parameters => parameters;

        // 使用可能コマンド
        [SerializeField, CommandId] private List<string> usableCommandIdList = new List<string>();
        public IReadOnlyList<string> UsableCommandIdList => usableCommandIdList;

        // 思想
        [SerializeField, SimpleHorizontalDrawer] private List<Thought> thoughts = new List<Thought>();
        public IReadOnlyList<Thought> Thoughts => thoughts;

        public bool Finished
        {
            get
            {
                return thoughts.Count > 0 && thoughts[0].Name == PlayerSetting.targetname;
            }
        }

        private Subject<(HumanParameter, float)> onParameterValueChanged = new Subject<(HumanParameter, float)>();
        public Subject<(HumanParameter, float)> OnParameterValueChanged => onParameterValueChanged;

        private Subject<(Thought, float)> onThoughtValueChanged = new Subject<(Thought, float)>();
        public Subject<(Thought, float)> OnThoughtValueChanged => onThoughtValueChanged;

        private ReactiveProperty<bool> isInvokingCommand = new ReactiveProperty<bool>();
        public bool IsInvokingCommand
        {
            get => isInvokingCommand.Value;
            set => isInvokingCommand.Value = value;
        }
        public IObservable<bool> OnIsInvokingCommandChanged => isInvokingCommand;

        private Dictionary<string, int> receivedCommands = new Dictionary<string, int>();
        public Dictionary<string, int> ReceivedCommands => receivedCommands;

        // CommandMasterはPresentation名前空間にあるのでHumanParameterMasterのようにシングルトンを利用して具象型を参照するのは避けたい。
        // なのでInitialize時にインターフェース型で注入してもらう。
        private ICommandMaster commandMaster;

        private bool isBrainwashed = false;
        private Subject<Unit> onBrainwashed = new Subject<Unit>();
        public IObservable<Unit> OnBrainwashed => onBrainwashed;

        // parameterにhumanを注入(Parameterの実効値計算に必要なので他から参照される前に呼ぶこと)
        public void Initialize(ICommandMaster commandMaster)
        {
            this.commandMaster = commandMaster;
            parameters.ForEach(x => x.Initialize(this));
            SortParameters();
            SortUsableCommandIdList();
            thoughts.ForEach(x =>
            {
                if(x.Name == Thought.thoughtNameForPlayer) x.Name = PlayerSetting.targetname;
            });
            SortThoughts();
        }

        public void SetState(HumanState state)
        {
            this.state.Value = state;
        }

        public void SetBattleTarget(HumanCore battleTarget)
        {
            this.battleTarget.Value = battleTarget;
        }

        public HumanParameter FindParameter(string parameterId)
        {
            return parameters.Find(x => x.ParameterId == parameterId);
        }

        public void AddParameter(HumanParameter parameter)
        {
            parameters.Add(parameter);
            parameter.Initialize(this);
            SortParameters();
        }

        private void SortParameters()
        {
            parameters.Sort((a, b) =>
            {
                HumanParameterMaster parameterMaster = HumanParameterMaster.Instance;
                return parameterMaster.FindIndex(a.ParameterId) - parameterMaster.FindIndex(b.ParameterId);
            });
        }

        public void AddUsableCommandId(string commandId)
        {
            usableCommandIdList.Add(commandId);
            SortUsableCommandIdList();
        }

        private void SortUsableCommandIdList()
        {
            usableCommandIdList.Sort((a, b) =>
            {
                return commandMaster.FindIndex(a) - commandMaster.FindIndex(b);
            });
        }

        public void AddThoughtValue(string name, float value)
        {
            Thought thought = thoughts.Find(x => x.Name == name);
            if(thought == null)
            {
                thought = new Thought(name);
                thoughts.Add(thought);
            }
            thought.Value += value;
            
            SortThoughts();

            onThoughtValueChanged.OnNext((thought, value));

            if(!isBrainwashed)
            {
                if(thoughts[0].Name == PlayerSetting.targetname)
                {
                    isBrainwashed = true;
                    onBrainwashed.OnNext(Unit.Default);
                }
            }
        }

        private void SortThoughts()
        {
            thoughts.Sort((a, b) => b.Value.CompareTo(a.Value));
        }

        public void IncrementReceivedCommand(string commandId)
        {
            if(!receivedCommands.ContainsKey(commandId))
            {
                receivedCommands[commandId]= 0;
            }
            receivedCommands[commandId]++;
        }

        public int SafeGetReceivedCommand(string commandId)
        {
            return receivedCommands.ContainsKey(commandId) ? receivedCommands[commandId] : 0;
        }
    }
}