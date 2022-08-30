using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using CKY.AI;

namespace CKY.AI.FSM
{
    public class StateMachineAI : MonoBehaviour
    {
        #region Variables
        [SerializeField] private BaseStateAI currentState;

        [HideInInspector] public Idle idleState;
        [HideInInspector] public Move moveState;
        [HideInInspector] public Jump jumpState;
        [HideInInspector] public MoveInTheAir moveInTheAirState;
        [HideInInspector] public Land landState;
        [HideInInspector] public Death deathState;

        protected AIHealthController playerHealthController;
        public AIAnimator aiAnimator;
        public Rigidbody rb;
        public float moveSpeed;
        public float jumpPower;

        public bool jumpTrigger;
        public bool attackTrigger;
        #endregion

        #region Preparing
        private void Awake()
        {
            idleState = new Idle(this);
            moveState = new Move(this);
            jumpState = new Jump(this);
            landState = new Land(this);
            moveInTheAirState = new MoveInTheAir(this);
            deathState = new Death(this);
        }
        #endregion

        private void Start()
        {
            currentState = GetInitialState();

            if (currentState != null)
                currentState.Enter();

            this.UpdateAsObservable().
                Subscribe(_ => MyUpdate());

            this.FixedUpdateAsObservable().
                Subscribe(_ => MyFixedUpdate());

            StartCoroutine(SkipFirstFrame());

            EventSubscriber();

            GetComponents();

            GameEvents.GameRestart += GameRestarted;
        }

        private void GameRestarted()
        {
            transform.position = Vector3.zero + Vector3.up * 2.0f;
        }

        private IEnumerator SkipFirstFrame()
        {
            yield return null;

            this.OnCollisionEnterAsObservable().
                Subscribe(collision => OnCollision(collision));
        }

        private void GetComponents()
        {
            playerHealthController = GetComponent<AIHealthController>();
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

        public void ChangeState(BaseStateAI newState)
        {
            currentState.Exit();

            currentState = newState;
            currentState.Enter();
        }

        protected virtual BaseStateAI GetInitialState()
        {
            return idleState;
        }

        private void OnCollision(Collision collision)
        {
            if (collision.gameObject.layer == 7) // Ground layer
            {
                ChangeState(landState);
            }
        }

        public void Death()
        {
            ChangeState(deathState);
        }
    }
}