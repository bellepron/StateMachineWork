using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private float transitionTime = 0.25f;
        private int _currentState;

        #region Cached Properties
        private static readonly int Idle = Animator.StringToHash("Idle BT");
        private static readonly int Run = Animator.StringToHash("Run BT");
        private static readonly int Jump = Animator.StringToHash("Jump BT");
        private static readonly int Fall = Animator.StringToHash("Fall BT");
        private static readonly int Land = Animator.StringToHash("Land BT");
        private static readonly int Crouch = Animator.StringToHash("Stand To Kneel BT");
        private static readonly int CrouchIdle = Animator.StringToHash("Kneeling Idle BT");
        private static readonly int CrouchToStand = Animator.StringToHash("Kneel to Stand BT");

        private static readonly int NotAttack = Animator.StringToHash("Not Attack");
        private static readonly int Attack = Animator.StringToHash("Attack BT");
        #endregion


        private void Start()
        {
            transform.parent.GetComponent<FSM.StateMachinePlayer>().playerAnimator = this;

            IdleAnim();

            GameEvents.GameRestart += GameRestarted;
        }

        private void GameRestarted()
        {
            IdleAnim();
            NotAttackAnim();
        }

        public void IdleAnim()
        {
            anim.CrossFade(Idle, transitionTime, 0);
        }

        public void RunAnim()
        {
            anim.CrossFade(Run, 0, 0);
        }
        public void JumpAnim()
        {
            anim.CrossFade(Jump, 0, 0);
        }
        public void FallAnim()
        {
            anim.CrossFade(Fall, transitionTime, 0);
        }
        public void LandAnim()
        {
            anim.CrossFade(Land, transitionTime, 0);
        }
        public void CrouchAnim()
        {
            anim.CrossFade(Crouch, transitionTime, 0);
        }
        public void CrouchIdleAnim()
        {
            anim.CrossFade(CrouchIdle, transitionTime, 0);
        }
        public void CrouchToStandAnim()
        {
            anim.CrossFade(CrouchToStand, transitionTime, 0);
        }
        public void NotAttackAnim()
        {
            anim.CrossFade(NotAttack, 0, 1);
        }
        public void AttackAnim()
        {
            anim.CrossFade(Attack, 0, 1);
        }
    }
}