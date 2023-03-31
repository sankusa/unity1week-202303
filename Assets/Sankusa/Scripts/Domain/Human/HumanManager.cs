using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Sankusa.unity1week202303.Domain
{
    public class HumanManager
    {
        private List<HumanCore> humanCores = new List<HumanCore>();
        public IReadOnlyList<HumanCore> HumanCores => humanCores;

        public void Add(HumanCore humanCore)
        {
            humanCores.Add(humanCore);
        }
        public void Remove(HumanCore humanCore)
        {
            humanCores.Remove(humanCore);
        }

        public HumanCore GetPlayerHumanCore()
        {
            return humanCores.Where(x => x.Human.IsPlayer).FirstOrDefault();
        }
    }
}