using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sankusa.unity1week202303.Domain
{
    [Serializable]
    public class HumanParameterData
    {
        [SerializeField] private string parameterId;
        public string ParameterId => parameterId;

        [SerializeField] private string displayName;
        public string DisplayName => displayName;

        [SerializeField] private bool useMin;
        public bool UseMin => useMin;

        [SerializeField] private float min;
        public float Min => min;

        [SerializeField] private bool useMax;
        public bool UseMax => useMax;

        [SerializeField] private float max;
        public float Max => max;

        [SerializeField, SimpleHorizontalDrawer] private List<HumanParameterRateAdditionResource> rateAdditionResources;
        public  IReadOnlyList<HumanParameterRateAdditionResource> RateAdditionResources => rateAdditionResources;
    }
}