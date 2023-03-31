using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;
using Zenject;

namespace Sankusa.unity1week202303.Domain
{
    public class HumanCore : MonoBehaviour
    {
        [SerializeField] private Human human;
        public Human Human => human;
        [Inject] private ICommandMaster commandMaster;
        [Inject] private HumanManager humanManager;
        private List<HumanComponentBase> humanComponents = new List<HumanComponentBase>();

        void Awake()
        {
            human.Initialize(commandMaster);

            humanComponents.AddRange(GetComponentsInChildren<HumanComponentBase>());
            humanComponents.ForEach(x => x.Initialize(this));

            humanManager.Add(this);
        }

        void OnDestroy()
        {
            humanManager.Remove(this);
        }

        public T GetHumanComponent<T>() where T : HumanComponentBase
        {
            foreach(HumanComponentBase humanComponent in humanComponents)
            {
                T target = humanComponent as T;
                if(target != null) return target;
            }
            return null;
        }
    }
}