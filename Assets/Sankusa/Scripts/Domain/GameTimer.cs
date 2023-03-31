using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace Sankusa.unity1week202303.Domain
{
    public class GameTimer : IDisposable
    {
        private readonly ReactiveProperty<float> elapsedTime = new ReactiveProperty<float>(0);
        public float ElapsedTime => elapsedTime.Value;
        public IObservable<float> OnElapsedTimeChanged => elapsedTime;

        private readonly ReactiveProperty<float> timeLimit = new ReactiveProperty<float>(0);
        public float TimeLimit => timeLimit.Value;
        public IObservable<float> OnTimeLimitChanged => timeLimit;

        public float RemainingTime => timeLimit.Value - elapsedTime.Value;

        private IObservable<Unit> OnTimeUp =>
            Observable
                .Merge(elapsedTime, timeLimit)
                .Where(_ => timeLimit.Value != 0 && elapsedTime.Value >= timeLimit.Value)
                .AsUnitObservable();

        private readonly CompositeDisposable disposable = new CompositeDisposable();

        public void Start()
        {
            Observable.EveryUpdate()
                .Subscribe(_ => Update())
                .AddTo(disposable);
        }

        private void Update()
        {
            SetElapsedTime(ElapsedTime + Time.deltaTime);
        }

        public void Stop()
        {
            disposable.Clear();
        }

        public void SetElapsedTime(float elapsedTime)
        {
            if(timeLimit.Value == 0)
            {
                this.elapsedTime.Value = Mathf.Min(elapsedTime, 0);
            }
            else
            {
                this.elapsedTime.Value = Mathf.Clamp(elapsedTime, 0, timeLimit.Value);
            }
        }

        public void SetTimeLimit(float timeLimit)
        {
            this.timeLimit.Value = timeLimit;
        }

        public void Reset()
        {
            SetElapsedTime(0);
            timeLimit.Value = 0;
        }

        public void ResetElapsedTime()
        {
            SetElapsedTime(0);
        }

        public void Dispose()
        {
            disposable.Dispose();
        }
    }
}