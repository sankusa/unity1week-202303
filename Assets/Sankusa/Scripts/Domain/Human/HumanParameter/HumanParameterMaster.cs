using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SankusaLib;
using SankusaLib.SingletonLib;

namespace Sankusa.unity1week202303.Domain
{
    [CreateAssetMenu(fileName = nameof(HumanParameterMaster), menuName = nameof(unity1week202303) + "/" + nameof(HumanParameterMaster))]
    public class HumanParameterMaster : SingletonScriptableObject<HumanParameterMaster>
    {
        [SerializeField] private List<HumanParameterData> parameterDataList;
        public IReadOnlyList<HumanParameterData> ParameterDataList => parameterDataList;

        public HumanParameterData Find(string parameterId)
        {
            return parameterDataList.Find(x => x.ParameterId == parameterId);
        }

        public int FindIndex(string parameterId)
        {
            return parameterDataList.FindIndex(x => x.ParameterId == parameterId);
        }
    }
}