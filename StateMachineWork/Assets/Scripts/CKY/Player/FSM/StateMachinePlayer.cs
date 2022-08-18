using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace CKY.Player.FSM
{
    public class StateMachinePlayer : MonoBehaviour
    {
        [SerializeField] BaseStatePlayer currentState;

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

            this.UpdateAsObservable().
                Subscribe(_ => MyUpdate());

            this.FixedUpdateAsObservable().
                Subscribe(_ => MyFixedUpdate());

            this.OnCollisionEnterAsObservable().
                Subscribe(collision => OnCollision(collision));

            EventSubscriber();
        }

        public virtual void EventSubscriber()
        {
            Debug.Log("There is something wrong, if you see this!");
        }

        private void MyUpdate()
        {
            if (currentState != null)
                currentState.UpdateLogic();
        }

        private void MyFixedUpdate()
        {
            if (currentState != null)
                currentState.UpdatePhysics();
        }

        public void ChangeState(BaseStatePlayer newState)
        {
            currentState.Exit();

            currentState = newState;
            currentState.Enter();
        }

        protected virtual BaseStatePlayer GetInitialState()
        {
            return idleState;
        }

        private void OnCollision(Collision collision)
        {
            if (collision.gameObject.layer == 7) // Ground layer
            {
                ChangeState(idleState);
            }
        }
    }
}