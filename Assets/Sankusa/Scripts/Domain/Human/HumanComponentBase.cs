using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sankusa.unity1week202303.Domain
{
    public abstract class HumanComponentBase : MonoBehaviour
    {
        protected HumanCore humanCore;
        public virtual void Initialize(HumanCore humanCore)
        {
            this.humanCore = humanCore;
        }
    }
}