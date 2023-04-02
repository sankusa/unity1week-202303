using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;
using Zenject;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Sankusa.unity1week202303.Presentation
{
    public class NPCBase : HumanComponentBase
    {
        [SerializeField, CommandId] protected List<string> temporaryCommands;
        public virtual IReadOnlyList<string> TemporaryCommands => temporaryCommands;

        [Inject] protected CommandInvoker commandInvoker;
        protected CancellationTokenSource source = new CancellationTokenSource();

        public virtual async UniTask ActAsync()
        {
            await UniTask.CompletedTask;
        }

        public virtual async UniTask ReactionAsync(CommandArg commandArg)
        {
            await UniTask.CompletedTask;
        }
    }
}