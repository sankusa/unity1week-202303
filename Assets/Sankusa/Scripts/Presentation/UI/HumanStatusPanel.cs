using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;
using UniRx;
using TMPro;
using SankusaLib;

namespace Sankusa.unity1week202303.Presentation
{
    public class HumanStatusPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private TMP_Text faithProduceText;
        [SerializeField] private HumanParameterRow parameterRowPrefab;
        [SerializeField] private Transform parameterRowRoot;
        [SerializeField] private HumanThoughtRow thoughtRowPrefab;
        [SerializeField] private Transform thoughtRowRoot;
        private List<HumanParameterRow> parameterRows = new List<HumanParameterRow>();
        private List<HumanThoughtRow> thoughtRows = new List<HumanThoughtRow>();

        private Human human;
        public void SetModel(Human human)
        {
            this.human = human;
        }

        void Start()
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ => Repaint())
                .AddTo(this);
        }

        private void Repaint()
        {
            if(human == null)
            {
                gameObject.SetActive(false);
                return;
            }
            else
            {
                gameObject.SetActive(true);
            }

            nameText.text = human.Name;
            descriptionText.text = human.Description;
            faithProduceText.text = "信仰生産 " + human.FaithProduce.ToString() + " / s";

            parameterRows = TransformUtil.AdjustChildCount<HumanParameterRow>(parameterRowRoot, parameterRowPrefab.gameObject, human.Parameters.Count);
            for(int i = 0;  i < human.Parameters.Count; i++)
            {
                parameterRows[i].SetValue(human.Parameters[i]);
            }

            thoughtRows = TransformUtil.AdjustChildCount<HumanThoughtRow>(thoughtRowRoot, thoughtRowPrefab.gameObject, human.Thoughts.Count);
            for(int i = 0;  i < human.Thoughts.Count; i++)
            {
                thoughtRows[i].SetValue(human.Thoughts[i]);
            }
        }
    }
}