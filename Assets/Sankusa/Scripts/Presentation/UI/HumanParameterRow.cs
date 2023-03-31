using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class HumanParameterRow : MonoBehaviour
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text valueText;
        [SerializeField] private TMP_Text additionText;

        public void SetValue(HumanParameter parameter)
        {
            nameText.text = parameter.Data.DisplayName;
            valueText.text = parameter.BaseValue.ToString("0");
            additionText.text = parameter.AdditionalRate != 0 ? "(" + parameter.AdditionalRate.ToString("+#;-#;") + "%)" : "";
        }
    }
}