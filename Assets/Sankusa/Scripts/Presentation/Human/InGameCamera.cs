using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SankusaLib;
using DG.Tweening;

namespace Sankusa.unity1week202303.Presentation
{
    public class InGameCamera : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObjectTracer tracer;
        [Header("Shake設定")]
        [SerializeField, Range(0, 1)] private float strength;
        private Tweener zoomTweener;
        private Tweener shakeTwener;
        public void SetTraceTarget(Transform transform)
        {
            tracer.Target = transform;
        }

        public void Zoom(bool zoomIn)
        {
            if(zoomTweener != null && zoomTweener.IsActive() && zoomTweener.IsPlaying())
            {
                zoomTweener.Kill();
            }
            zoomTweener = DOTween.To(() => tracer.Offset.z, value => tracer.Offset = new Vector3(tracer.Offset.x, tracer.Offset.y, value), zoomIn ? -6 : -9, 0.3f).SetLink(gameObject);
        }

        public void Shake()
        {
            if(shakeTwener != null && shakeTwener.IsActive() && shakeTwener.IsPlaying())
            {
                shakeTwener.Kill();
                mainCamera.transform.localPosition = Vector3.zero;
            }
            shakeTwener = mainCamera.transform.DOShakePosition(0.3f, strength, 10, 90, false, true);
        }
    }
}