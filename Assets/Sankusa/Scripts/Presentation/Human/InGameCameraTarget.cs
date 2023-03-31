using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Sankusa.unity1week202303.Presentation
{
    public class InGameCameraTarget : MonoBehaviour
    {
        [Inject] private InGameCamera inGameCamera;
        void Awake()
        {
            inGameCamera.SetTraceTarget(transform);
        }
    }
}