using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CKY.Player;

namespace CKY.AI.FSM
{
    public class StateMachineAIController : StateMachineAI
    {

        #region Events
        public override void EventSubscriber() => SubscribeEvents();

        private void OnDisable() => UnSubscribeEvents();

        private void OnDestroy() => UnSubscribeEvents();

        private void SubscribeEvents()
        {
            CKY.INPUT.InputHandler.JumpButtonPressed += Jump;
            CKY.INPUT.InputHandler.AttackButtonPressed += TriggerAttack;
        }

        private void UnSubscribeEvents()
        {
            CKY.INPUT.InputHandler.JumpButtonPressed -= Jump;
            CKY.INPUT.InputHandler.AttackButtonPressed -= TriggerAttack;
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
        private void TriggerAttack()
        {
            attackTrigger = true;
            StartCoroutine(AttackResetter());

            Attack();
        }
        IEnumerator AttackResetter()
        {
            yield return null;
            attackTrigger = false;
        }

        private void Attack()
        {
            aiAnimator.AttackAnim(); // TODO: Still working when dead;
        }
        #endregion
    }
}