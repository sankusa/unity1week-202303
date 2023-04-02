using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class TeamText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        void Start()
        {
            text.text = PlayerSetting.targetname + "教";
        }
    }
}