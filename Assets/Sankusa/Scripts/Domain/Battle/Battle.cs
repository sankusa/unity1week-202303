using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sankusa.unity1week202303.Domain
{
    public class Battle
    {
        private readonly HumanCore[] joiners = new HumanCore[2];
        public IEnumerable<HumanCore> Joiners => joiners;

        public Battle(HumanCore humanCore1, HumanCore humanCore2)
        {
            joiners[0] = humanCore1;
            joiners[1] = humanCore2;
        }
    }
}