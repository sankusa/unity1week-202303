using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sankusa.unity1week202303.Domain
{
    [Serializable]
    public class HumanParameterRateAdditionResource
    {
        [SerializeField, HumanParameterId] private string parameterId;
        public string ParameterId => parameterId;

        [SerializeField] private float convertRate;
        public float ConvertRate => convertRate;
    }
}