using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SankusaLib.SingletonLib;

namespace Sankusa.unity1week202303.Domain
{
    [CreateAssetMenu(fileName =nameof(StageMaster), menuName = nameof(unity1week202303) + "/" + nameof(StageMaster))]
    public class StageMaster : SingletonScriptableObject<StageMaster>
    {
        [SerializeField] private List<StageDataContainer> stageDataContainers;
        public IReadOnlyList<StageDataContainer> StageDataContainers => stageDataContainers;
    }
}