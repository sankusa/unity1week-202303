using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    abstract public class CommandEffectBase : ScriptableObject
    {
        public virtual async UniTask InvokeAsync(CommandArg arg, CancellationToken token)
        {
            await UniTask.Delay(1000, cancellationToken: token);
        }

        public virtual bool IsInvokable(CommandArg arg)
        {
            return true;
        }
    }
}