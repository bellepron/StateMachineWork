using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CKY.FSM.AI
{
    public class StateMachineAIController : StateMachineAI
    {
        #region Components
        [SerializeField] AbstractGun gun;

        [HideInInspector] public Rigidbody rb;

        public float speed = 4f;
        public bool jumpTrigger;
        public bool attackTrigger;
        #endregion

        #region Preparing
        private void Awake()
        {
            idleState = new Idle(this);
            moveState = new Move(this);
            jumpState = new Jump(this);

            GetComponents();
        }

        private void GetComponents()
        {
            rb = GetComponent<Rigidbody>();
        }
        #endregion

        #region Events
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnDestroy()
        {
            UnSubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CKY.INPUT.InputHandler.JumpButtonPressed += Jump;
            CKY.INPUT.InputHandler.AttackButtonPressed += Attack;
        }

        private void UnSubscribeEvents()
        {
            CKY.INPUT.InputHandler.JumpButtonPressed -= Jump;
            CKY.INPUT.InputHandler.AttackButtonPressed -= Attack;
        }

        #endregion

        #region Jump
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
        #endregion

        #region Attack
        private void Attack()
        {
            attackTrigger = true;
            StartCoroutine(AttackResetter());

            Shoot();
        }
        IEnumerator AttackResetter()
        {
            yield return null;
            attackTrigger = false;
        }

        private void Shoot()
        {
            Debug.Log("Enemy bullet spawned. TODO: need id.");
            gun.Shoot();
        }
        #endregion
    }
}