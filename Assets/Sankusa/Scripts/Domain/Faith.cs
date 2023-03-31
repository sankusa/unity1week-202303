using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using System;

namespace Sankusa.unity1week202303.Domain
{
    public class Faith
    {
        // 値
        private readonly ReactiveProperty<float> value = new ReactiveProperty<float>(0);
        public float Value => value.Value;
        public IObservable<float> OnValueChanged => value;

        // 最大値
        private readonly ReactiveProperty<float> max = new ReactiveProperty<float>(0);
        public float Max => max.Value;
        public IObservable<float> OnMaxChanged => max;

        // 満タン通知
        private Subject<Unit> onFilled = new Subject<Unit>();
        public IObservable<Unit> OnFilled => onFilled;

        public bool IsFilled
        {
            get => value.Value >= max.Value;
        }

        public void Initialize(float max)
        {
            Assert.IsTrue(max > 0);

            value.Value = 0;
            this.max.Value = max;
        }

        public void ResetValue()
        {
            value.Value = 0;
        }

        public void AddValue(float addition)
        {
            value.Value = Mathf.Max(value.Value + addition, 0);
            if(value.Value >= max.Value) onFilled.OnNext(Unit.Default);
        }
    }
}