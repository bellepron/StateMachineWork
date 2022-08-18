using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CKY.Player.FSM
{
    public class StateMachinePlayerController : StateMachinePlayer
    {
        #region Components
        [SerializeField] AbstractWeapon weapon;
        public PlayerAnimator playerAnimator;

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
        public override void EventSubscriber() => SubscribeEvents();

        private void OnDisable() => UnSubscribeEvents();

        private void OnDestroy() => UnSubscribeEvents();

        private void SubscribeEvents()
        {
            CKY.INPUT.InputHandler.JumpButtonPressed += Jump;
            CKY.INPUT.InputHandler.AttackButtonPressed += Attack;

            GameEvents.WeaponLoaded += GetLoadedWeapon;
        }

        private void UnSubscribeEvents()
        {
            CKY.INPUT.InputHandler.JumpButtonPressed -= Jump;
            CKY.INPUT.InputHandler.AttackButtonPressed -= Attack;

            GameEvents.WeaponLoaded -= GetLoadedWeapon;
        }

        private void GetLoadedWeapon(AbstractWeapon loadedWeapon)
        {
            print($"Holder gets loaded weapon {loadedWeapon}");
            this.weapon = loadedWeapon;
        }

        #endregion

        #region Jump
        private void Jump()
        {
            jumpTrigger = true;
            //playerAnimator.Jump();
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
            if (this.weapon == null) Debug.Log("Bok");
            Debug.Log($"Player bullet spawned. TODO: need id. And the loaded weapon {this.weapon}");
            //playerAnimator.Shoot();
            weapon.Shoot();
        }
        #endregion
    }
}