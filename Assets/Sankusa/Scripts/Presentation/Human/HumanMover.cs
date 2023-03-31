using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sankusa.unity1week202303.Domain;

namespace Sankusa.unity1week202303.Presentation
{
    public class HumanMover : HumanComponentBase
    {
        [SerializeField] private CharacterController characterController;
        private Vector3 velocity;
        public void SetVelocity(Vector3 velocity)
        {
            this.velocity = velocity;
        }

        void Update()
        {
            characterController.Move(velocity * Time.deltaTime);
        }
    }
}