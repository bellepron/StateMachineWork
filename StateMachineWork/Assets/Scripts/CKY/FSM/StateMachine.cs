using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.FSM
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] BaseState currentState;

        [HideInInspector]
        public Idle idleState;
        [HideInInspector]
        public Move moveState;
        [HideInInspector]
        public Jump jumpState;

        private void Start()
        {
            currentState = GetInitialState();

            if (currentState != null)
                currentState.Enter();
        }

        private void Update()
        {
            if (currentState != null)
                currentState.UpdateLogic();
        }

        private void LateUpdate()
        {
            if (currentState != null)
                currentState.UpdatePhysics();
        }

        public void ChangeState(BaseState newState)
        {
            currentState.Exit();

            currentState = newState;
            currentState.Enter();
        }

        protected virtual BaseState GetInitialState()
        {
            return idleState;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 7) // Ground layer
            {
                ChangeState(idleState);
            }
        }
    }
}