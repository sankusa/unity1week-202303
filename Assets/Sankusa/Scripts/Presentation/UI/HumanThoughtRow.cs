using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class HumanThoughtRow : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text valueText;

        public void SetValue(Thought thought)
        {
            nameText.text = thought.Name;
            valueText.text = thought.Value.ToString("0");
        }
    }
}