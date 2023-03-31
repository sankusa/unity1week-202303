using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SankusaLib.SingletonLib;
using SankusaLib;

namespace Sankusa.unity1week202303.Presentation
{
    [CreateAssetMenu(fileName = nameof(EffectPrefabMaster), menuName = nameof(unity1week202303)+ "/" + nameof(EffectPrefabMaster))]
    public class EffectPrefabMaster : SingletonScriptableObject<EffectPrefabMaster>
    {
        [SerializeField] private TextEffect parameterTextEffectPrefab;
        public TextEffect ParameterTextEffectPrefab => parameterTextEffectPrefab;

        [SerializeField] private TextEffect thoughtTextEffectPrefab;
        public TextEffect ThoughtTextEffectPrefab => thoughtTextEffectPrefab;

        [SerializeField] private TextEffect talkTextEffectPrefab;
        public TextEffect TalkTextEffectPrefab => talkTextEffectPrefab;
    }
}