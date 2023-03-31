using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using DG.Tweening;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class HumanTurner : HumanComponentBase
    {
        private ReactiveProperty<bool> right = new ReactiveProperty<bool>(false);
        private Vector3 positionOld;
        private Tweener turnTweener;

        public override void Initialize(HumanCore humanCore)
        {
            base.Initialize(humanCore);

            right
                .Skip(1)
                .Subscribe(x =>
                {
                    Turn(x);
                })
                .AddTo(this);

            positionOld = transform.position;
        }

        void Update()
        {
            if(transform.position.x - positionOld.x > 0)
            {
                right.Value = true;
            }
            else if(transform.position.x - positionOld.x < 0)
            {
                right.Value = false;
            }
            positionOld = transform.position;
        }

        public void Turn(bool turnRight)
        {
            if(turnTweener != null && turnTweener.IsActive() && turnTweener.IsPlaying())
            {
                turnTweener.Kill();
                turnTweener = null;
            }
            turnTweener = transform.DORotate(new Vector3(0, turnRight ? 180 : 0), 0.4f);
        }
    }
}