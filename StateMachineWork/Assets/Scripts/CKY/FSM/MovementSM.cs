using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.FSM
{
    public class MovementSM : StateMachine
    {
        [HideInInspector] public Rigidbody rb;
        [HideInInspector] public MeshRenderer mr;
        public float speed = 4f;
        public float moveValue;
        public bool jumpTrigger;
        public bool attackTrigger;

        private void Awake()
        {
            idleState = new Idle(this);
            moveState = new Move(this);
            jumpState = new Jump(this);

            GetComponents();
            SubscribeEvents();
        }

        private void GetComponents()
        {
            rb = GetComponent<Rigidbody>();
            mr = GetComponent<MeshRenderer>();
        }

        #region Events

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputHandler.ForwardButtonPressing += Forward;
            InputHandler.BackwardButtonPressing += Backward;
            InputHandler.JumpButtonPressed += Jump;
            InputHandler.AttackButtonPressed += Attack;
        }

        private void UnSubscribeEvents()
        {
            InputHandler.ForwardButtonPressing -= Forward;
            InputHandler.BackwardButtonPressing -= Backward;
            InputHandler.JumpButtonPressed -= Jump;
            InputHandler.AttackButtonPressed -= Attack;
        }

        #endregion

        private void Forward()
        {
            moveValue = 1;
        }

        private void Backward()
        {
            moveValue = -1;
        }

        private void Jump()
        {
            jumpTrigger = true;
            StartCoroutine(JumpResetter());
        }
        IEnumerator JumpResetter()
        {
            yield return null;
            jumpTrigger = false;
        }

        private void Attack()
        {
            attackTrigger = true;
            SpawnBullet();
            StartCoroutine(AttackResetter());
        }
        IEnumerator AttackResetter()
        {
            yield return null;
            attackTrigger = false;
        }

        private void SpawnBullet()
        {
            Debug.Log("Bullet spawned");
        }

        //private void Update()
        //{
        //    if (moveValue > 0.1f)
        //        moveValue -= 1 * Time.deltaTime;
        //    else if (moveValue < 0.1f)
        //        moveValue += 1 * Time.deltaTime;
        //    else
        //        moveValue = 0;
        //}
    }
}