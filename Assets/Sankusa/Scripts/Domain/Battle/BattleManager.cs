using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

namespace Sankusa.unity1week202303.Domain
{
    public class BattleManager
    {
        private List<Battle> battles = new List<Battle>();
        public IEnumerable<Battle> Battles => battles;

        public void StartBattle(HumanCore humanCore1, HumanCore humanCore2)
        {
            if(humanCore1.Human.State == HumanState.Free && humanCore2.Human.State == HumanState.Free)
            {
                humanCore1.Human.SetBattleTarget(humanCore2);
                humanCore2.Human.SetBattleTarget(humanCore1);

                humanCore1.Human.SetState(HumanState.Battle);
                humanCore2.Human.SetState(HumanState.Battle);

                battles.Add(new Battle(humanCore1, humanCore2));
            }
        }

        public void StopBattle(HumanCore humanCore)
        {
            Battle battle = battles.Find(x => x.Joiners.Contains(humanCore));
            if(battle != null)
            {
                battles.Remove(battle);

                foreach(HumanCore joiner in battle.Joiners)
                {
                    joiner.Human.SetState(HumanState.Free);
                }
                foreach(HumanCore joiner in battle.Joiners)
                {
                    joiner.Human.SetBattleTarget(null);
                }
            }
        }
    }
}