using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sankusa.unity1week202303.Domain
{
    [Serializable]
    public class HumanParameter
    {
        [SerializeField, HumanParameterId] private string parameterId;
        public string ParameterId => parameterId;

        [SerializeField] private float baseValue;
        public float BaseValue
        {
            get => baseValue;
            private set
            {
                float baseValueTmp = value;
                if(Data.UseMin && baseValueTmp < Data.Min)
                {
                    baseValueTmp = Data.Min;
                }
                else if(Data.UseMax && baseValueTmp > Data.Max)
                {
                    baseValueTmp = Data.Max;
                }
                baseValue = baseValueTmp;
            }
        }

        public float AdditionalRate
        {
            get
            {
                float additionalRate = 0;
                // 他パラメータからの寄与(割合指定)
                foreach(HumanParameterRateAdditionResource rateAdditionResource in Data.RateAdditionResources)
                {
                    HumanParameter parameter = owner.FindParameter(rateAdditionResource.ParameterId);
                    if(parameter != null)
                    {
                        additionalRate += parameter.BaseValue * rateAdditionResource.ConvertRate;
                    }
                }
                return additionalRate;
            }
        }

        public float Value
        {
            get
            {
                float valueTmp = baseValue + baseValue * AdditionalRate;
                if(Data.UseMin && valueTmp < Data.Min)
                {
                    valueTmp = Data.Min;
                }
                else if(Data.UseMax && valueTmp > Data.Max)
                {
                    valueTmp = Data.Max;
                }
                return valueTmp;
            }
        }

        private HumanParameterData data;
        public HumanParameterData Data
        {
            get
            {
                if(data == null)
                {
                    data = HumanParameterMaster.Instance.Find(parameterId);
                }
                return data;
            }
        }

        [NonSerialized] private Human owner;

        public HumanParameter(string parameterId, float baseValue)
        {
            this.parameterId = parameterId;
            BaseValue = baseValue;
        }

        public void Initialize(Human owner)
        {
            this.owner = owner;
        }

        public void AddBaseValue(float value)
        {
            BaseValue += value;
            owner?.OnParameterValueChanged.OnNext((this, value));
        }
    }
}