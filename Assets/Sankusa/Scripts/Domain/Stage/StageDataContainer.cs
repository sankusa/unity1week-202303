using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SankusaLib.CustomPopupLib;

namespace Sankusa.unity1week202303.Domain
{
    [CreateAssetMenu(fileName = nameof(StageDataContainer), menuName = nameof(unity1week202303) + "/" + nameof(StageDataContainer))]
    public class StageDataContainer : ScriptableObject
    {
        [SerializeField] private string stageName;
        public string StageName => stageName;

        [SerializeField, AssetPathPopup(typeof(GameObject), true)] private string stagePrefabPath;
        public string StagePrefabPath => stagePrefabPath;

        [SerializeField, Min(0)] private float timeLimit;
        public float TimeLimit => timeLimit;

        [SerializeField, Min(0)] private float targetFaith;
        public float TargetFaith => targetFaith;
    }
}