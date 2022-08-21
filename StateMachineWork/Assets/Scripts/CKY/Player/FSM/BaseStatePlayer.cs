using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.Player.FSM
{
    [System.Serializable]
    public class BaseStatePlayer
    {
        public enum State { IDLE, MOVE, JUMP, MOVEINTHEAIR, LAND }
        public State state;
        protected StateMachinePlayer stateMachinePlayer;


        public BaseStatePlayer(State state, StateMachinePlayer _stateMachinePlayer)
        {
            this.state = state;
            this.stateMachinePlayer = _stateMachinePlayer;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }
    }

    public class Idle : BaseStatePlayer
    {
        private readonly StateMachinePlayer _stateMachinePlayer;

        public Idle(StateMachinePlayer stateMachinePlayer) : base(State.IDLE, stateMachinePlayer)
        {
            _stateMachinePlayer = stateMachinePlayer;
        }

        public override void Enter()
        {
            if (_stateMachinePlayer.playerAnimator != null)
                _stateMachinePlayer.playerAnimator.IdleAnim();
            _stateMachinePlayer.rb.velocity = Vector3.zero;
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_stateMachinePlayer.jumpTrigger == true)
                stateMachinePlayer.ChangeState(_stateMachinePlayer.jumpState);

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true ||
                CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true)
                stateMachinePlayer.ChangeState(_stateMachinePlayer.moveState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Move : BaseStatePlayer
    {
        private readonly StateMachinePlayer _stateMachinePlayer;

        public Move(StateMachinePlayer stateMachinePlayer) : base(State.MOVE, stateMachinePlayer)
        {
            _stateMachinePlayer = stateMachinePlayer;
        }

        public override void Enter()
        {
            _stateMachinePlayer.playerAnimator.RunAnim();

            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_stateMachinePlayer.jumpTrigger == true)
                stateMachinePlayer.ChangeState(_stateMachinePlayer.jumpState);

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == false &&
                CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == false)
                stateMachinePlayer.ChangeState(_stateMachinePlayer.idleState);
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            Vector2 vel = _stateMachinePlayer.rb.velocity;
            float moveValue;

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true)
                moveValue = 1;
            else if (CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true)
                moveValue = -1;
            else moveValue = 0;


            //_stateMachinePlayerCOntroller.playerAnimator.Move(moveValue);

            vel.x = moveValue * _stateMachinePlayer.moveSpeed;
            _stateMachinePlayer.rb.velocity = vel;
        }
    }

    public class Jump : BaseStatePlayer
    {
        private readonly StateMachinePlayer _stateMachinePlayer;
        private float _jumpTime = 0.4f;
        private float _jumpTimeCounter = 0.0f;
        private bool _jumped;

        public Jump(StateMachinePlayer stateMachinePlayer) : base(State.JUMP, stateMachinePlayer)
        {
            _stateMachinePlayer = stateMachinePlayer;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Jump!");
            _jumpTimeCounter = 0.0f;
            _jumped = false;

            _stateMachinePlayer.playerAnimator.JumpAnim();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_jumped == true)
            {
                if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true ||
                    CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true)
                    stateMachinePlayer.ChangeState(_stateMachinePlayer.moveInTheAirState);

                return;
            }

            _jumpTimeCounter += Time.deltaTime;

            if (_jumpTimeCounter >= _jumpTime)
            {
                _jumped = true;

                _stateMachinePlayer.rb.AddForce(_stateMachinePlayer.jumpPower * Vector3.up, ForceMode.Impulse);
            }
        }
    }


    public class MoveInTheAir : BaseStatePlayer
    {
        private readonly StateMachinePlayer _stateMachinePlayer;
        private float _moveSpeedPercentInTheAir = 0.75f;

        public MoveInTheAir(StateMachinePlayer stateMachinePlayer) : base(State.MOVEINTHEAIR, stateMachinePlayer)
        {
            _stateMachinePlayer = stateMachinePlayer;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            // Double jump maybe *****
            //if (_stateMachinePlayer.jumpTrigger == true)
            //    StateMachinePlayer.ChangeState(_stateMachinePlayerController.jumpState);
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            Vector2 vel = _stateMachinePlayer.rb.velocity;
            float moveValue;

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true)
                moveValue = 1;
            else if (CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true)
                moveValue = -1;
            else moveValue = 0;

            vel.x = moveValue * _stateMachinePlayer.moveSpeed * _moveSpeedPercentInTheAir;
            _stateMachinePlayer.rb.velocity = vel;
        }
    }

    public class Land : BaseStatePlayer
    {
        private readonly StateMachinePlayer _stateMachinePlayer;
        private float _landAnimTime = 0.3f;
        private float _landAnimTimeCounter = 0.0f;

        public Land(StateMachinePlayer stateMachinePlayer) : base(State.LAND, stateMachinePlayer)
        {
            _stateMachinePlayer = stateMachinePlayer;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Land!");
            _landAnimTimeCounter = 0.0f;

            _stateMachinePlayer.playerAnimator.LandAnim();

            if (_stateMachinePlayer.rb.velocity.y < -1)
            {
                Debug.Log("Make an Explosion!");
            }
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            _landAnimTimeCounter += Time.deltaTime;

            if (_landAnimTimeCounter >= _landAnimTime)
            {
                _stateMachinePlayer.ChangeState(_stateMachinePlayer.idleState);
            }
        }
    }
}