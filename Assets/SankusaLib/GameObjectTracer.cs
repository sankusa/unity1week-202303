using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace SankusaLib {
    [ExecuteAlways]
    public class GameObjectTracer : MonoBehaviour
    {
        [SerializeField] Transform target;
        public Transform Target
        {
            set => target = value;
        }
#if UNITY_EDITOR
        [SerializeField] bool traceInEditor = false;
#endif
        [SerializeField] private bool traceX = true;
        [SerializeField] private bool traceY = true;
        [SerializeField] private bool traceZ = true;
        [SerializeField] Vector3 offset;
        public Vector3 Offset
        {
            get => offset;
            set => offset = value;
        }
        [SerializeField, Range(0, 1)] float moveRatePerSecond = 1f;

        void Update() {
            // エディタ上&エディットモードの場合、フラグが立っていればターゲットの位置に一致させる
#if UNITY_EDITOR
            if(traceInEditor && !Application.isPlaying && target != null) {
                transform.position = new Vector3(traceX ? target.position.x + offset.x : transform.position.x,
                                                 traceY ? target.position.y + offset.y : transform.position.y,
                                                 traceZ ? target.position.z + offset.z : transform.position.z);
            }
#endif
        }

        void FixedUpdate() {
            float exponent = 1f - Mathf.Pow(1f - moveRatePerSecond, 1f / 60f);
            if(target != null) {
                transform.position = Vector3.Lerp(transform.position,
                                                  new Vector3(traceX ? target.position.x + offset.x : transform.position.x,
                                                              traceY ? target.position.y + offset.y : transform.position.y,
                                                              traceZ ? target.position.z + offset.z : transform.position.z)
                                                 , exponent);
            }
        }
    }
}