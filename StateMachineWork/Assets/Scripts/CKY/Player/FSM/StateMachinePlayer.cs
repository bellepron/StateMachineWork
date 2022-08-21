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
        #region Variables
        [SerializeField] private BaseStatePlayer currentState;

        [HideInInspector] public Idle idleState;
        [HideInInspector] public Move moveState;
        [HideInInspector] public Jump jumpState;
        [HideInInspector] public MoveInTheAir moveInTheAirState;
        [HideInInspector] public Land landState;

        protected PlayerHealthController playerHealthController;
        public PlayerAnimator playerAnimator;
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

            StartCoroutine(JumpFirstFrame());

            EventSubscriber();

            GetComponents();
        }

        private IEnumerator JumpFirstFrame()
        {
            yield return null;

            this.OnCollisionEnterAsObservable().
                Subscribe(collision => OnCollision(collision));
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
                ChangeState(landState);
            }
        }
    }
}