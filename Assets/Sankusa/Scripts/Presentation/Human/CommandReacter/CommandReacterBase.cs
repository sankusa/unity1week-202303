using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Sankusa.unity1week202303.Domain ;

namespace Sankusa.unity1week202303.Presentation
{
    public class CommandReacterBase : HumanComponentBase
    {
        public virtual async UniTask ReactionAsync(CommandArg commandArg)
        {
            await UniTask.CompletedTask;
        }
    }
}