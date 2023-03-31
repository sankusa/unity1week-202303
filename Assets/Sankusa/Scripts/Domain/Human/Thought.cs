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
        public string Name => name;

        [SerializeField] private float value;
        public float Value
        {
            get => value;
            set => this.value = value;
        }

        public Thought(string name)
        {
            this.name = name;
        }
    }
}