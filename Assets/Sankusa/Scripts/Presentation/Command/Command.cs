using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sankusa.unity1week202303.Domain;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Sankusa.unity1week202303.Presentation
{
    [Serializable]
    public class Command
    {
        [SerializeField] private string commandId;
        public string CommandId => commandId;

        [SerializeField] private string name;
        public string Name => name;

        [SerializeField, Min(0)] private int cost;
        public int Cost => cost;

        [SerializeField] private CommandUser user;
        public CommandUser User => user;

        [SerializeField] private CommandTiming timing;
        public CommandTiming Timing => timing;

        [SerializeField] private CommandEffectBase commandEffect;
        public CommandEffectBase CommandEffect => commandEffect;

        [SerializeField] private string description;
        public string Description => description + " " + (cost > 0 ? "(信仰 -" + cost + ")" : "");

        public bool IsLearnable(HumanCore user)
        {
            if(user.Human.IsPlayer)
            {
                return this.user == CommandUser.Player || this.user == CommandUser.PlayerAndNPC;
            }
            else
            {
                return this.user == CommandUser.NPC || this.user == CommandUser.PlayerAndNPC;
            }
        }

        public async UniTask InvokeAsync(CommandArg commandArg, CancellationToken token)
        {
            commandArg.Faith.AddValue(-cost);
            await commandEffect.InvokeAsync(commandArg, token);
        }

        public bool IsInvokable(CommandArg arg)
        {
            return commandEffect.IsInvokable(arg) && arg.Faith.Value >= cost;
        }
    }
}