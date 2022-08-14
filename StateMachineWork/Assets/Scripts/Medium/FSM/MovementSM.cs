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
        public Move moveState;
        [HideInInspector]
        public Jump jumpState;

        public Rigidbody rb;
        public float speed = 4f;
        public MeshRenderer mr;

        private void Awake()
        {
            idleState = new Idle(this);
            moveState = new Move(this);
            jumpState = new Jump(this);
        }

        protected override BaseState GetInitialState()
        {
            return idleState;
        }
    }
}