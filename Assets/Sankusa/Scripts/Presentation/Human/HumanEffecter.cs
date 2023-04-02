using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;

namespace  Sankusa.unity1week202303.Presentation
{
    public class HumanEffecter : HumanComponentBase
    {
        [SerializeField] private GameObject finishParticle;

        void Update()
        {
            finishParticle.SetActive(!humanCore.Human.IsPlayer && humanCore.Human.Finished);
        } 
    }
}