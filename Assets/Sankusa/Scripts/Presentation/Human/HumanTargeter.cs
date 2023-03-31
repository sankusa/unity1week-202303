using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class HumanTargeter : HumanComponentBase
    {
        private HashSet<HumanCore> humanCores = new HashSet<HumanCore>();

        public HumanCore Closest => humanCores.OrderBy(x => (x.transform.position - transform.position).magnitude).FirstOrDefault();

        void OnTriggerEnter(Collider col)
        {
            HumanCore humanCore = col.GetComponent<HumanCore>();
            if(humanCore != null && humanCore != this.humanCore)
            {
                humanCores.Add(humanCore);
            }
        }

        void OnTriggerExit(Collider col)
        {
            HumanCore humanCore = col.GetComponent<HumanCore>();
            if(humanCore != null && humanCore != this.humanCore)
            {
                humanCores.Remove(humanCore);
            }
        }
    }
}