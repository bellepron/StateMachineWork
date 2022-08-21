using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace CKY.Player.FSM
{
    [RequireComponent(typeof(PlayerHealthController))]
    public class StateMachinePlayer : MonoBehaviour
    {
        [SerializeField] private BaseStatePlayer currentState;

        [HideInInspector]
        public Idle idleState;
        [HideInInspector]
        public Move moveState;
        [HideInInspector]
        public Jump jumpState;

        protected PlayerHealthController playerHealthController;
        public Rigidbody rb;
        public float moveSpeed;
        public float jumpPower;

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

            GetComponents();
        }

        private void GetComponents()
        {
            playerHealthController = GetComponent<PlayerHealthController>();
            rb = GetComponent<Rigidbody>();

            GameSettings gameSettings = GameManager.Instance.gameSettings;
            moveSpeed = gameSettings.playerMoveSpeed;
            jumpPower = gameSettings.playerJumpPower;
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