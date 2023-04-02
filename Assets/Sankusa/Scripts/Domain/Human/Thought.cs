using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Sankusa.unity1week202303.Domain
{
    [Serializable]
    public class Thought
    {
        public static readonly string thoughtNameForPlayer = "XXX";

        [SerializeField] private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }

        [SerializeField] private float value;
        public float Value
        {
            get => value;
            set => this.value = Mathf.Clamp(value, 0, 999);
        }

        public Thought(string name)
        {
            this.name = name;
        }
    }
}