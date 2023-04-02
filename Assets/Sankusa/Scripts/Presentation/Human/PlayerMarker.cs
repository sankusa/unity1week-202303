using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class PlayerMarker : HumanComponentBase
    {
        public override void Initialize(HumanCore humanCore)
        {
            base.Initialize(humanCore);
            humanCore.Human.IsPlayer = true;
            humanCore.Human.Name = PlayerSetting.playerName;
        }

        void OnDestroy()
        {
            humanCore.Human.IsPlayer = false;
        }
    }
}