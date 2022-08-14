using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Medium.FSM
{
    public class MovementSM : StateMachine
    {
        [HideInInspector]
        public Idle idleState;
        [HideInInspector]
        public Move movingState;
        public Rigidbody rb;
        public float speed = 4f;
        public MeshRenderer mr;

        private void Awake()
        {
            idleState = new Idle(this);
            movingState = new Move(this);
        }

        protected override BaseState GetInitialState()
        {
            return idleState;
        }
    }
}