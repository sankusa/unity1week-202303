using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class PlayerHumanMoverController : HumanComponentBase
    {
        [SerializeField] private float speed = 1f;
        private float velocityY = 2;
        private Transform cameraTransform;
        private HumanMover mover;
        public override void Initialize(HumanCore humanCore)
        {
            base.Initialize(humanCore);
            cameraTransform = Camera.main.transform;
            mover = humanCore.GetHumanComponent<HumanMover>();
            
            Assert.IsNotNull(mover);
        }

        void Update()
        {
            if(humanCore.Human.State == HumanState.Battle)
            {
                mover.SetVelocity(Vector3.zero);
                return;
            }

            Vector3 direction = Vector3.zero;
            Quaternion cameraRotationY = Quaternion.Euler(0, cameraTransform.rotation.y, 0);
            if(Keyboard.current.wKey.isPressed)
            {
                direction += cameraRotationY * Vector3.forward;
            }
            if(Keyboard.current.sKey.isPressed)
            {
                direction += cameraRotationY * Vector3.back;
            }
            if(Keyboard.current.aKey.isPressed)
            {
                direction += cameraRotationY * Vector3.left;
            }
            if(Keyboard.current.dKey.isPressed)
            {
                direction += cameraRotationY * Vector3.right;
            }
            direction.Normalize();
            mover.SetVelocity(direction * speed + velocityY * Vector3.down);
        }
    }
}