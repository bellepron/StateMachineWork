using CKY.Player.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.AI.FSM
{
    [System.Serializable]
    public class BaseStateAI
    {
        public enum State { IDLE, MOVE, JUMP, MOVEINTHEAIR, LAND, DEATH }
        public State state;
        protected StateMachineAI stateMachineAI;


        public BaseStateAI(State state, StateMachineAI _stateMachineAI)
        {
            this.state = state;
            this.stateMachineAI = _stateMachineAI;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }

        private bool ForwardButtonPressing()
        {
            return CKY.INPUT.InputHandler.Instance.forwardButton.Pressed;
        }
        private bool BackwardButtonPressing()
        {
            return CKY.INPUT.InputHandler.Instance.backwardButton.Pressed;
        }
        protected bool IsMoving()
        {
            return CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true ||
                    CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true;
        }
        protected int ReturnMoveValue()
        {
            if (ForwardButtonPressing() == true)
                return 1;
            else if (BackwardButtonPressing() == true)
                return -1;
            else
                return 0;
        }
        protected bool IsJumping()
        {
            return stateMachineAI.jumpTrigger == true;
        }
    }

    public class Idle : BaseStateAI
    {
        private readonly StateMachineAI _stateMachineAI;

        public Idle(StateMachineAI stateMachineAI) : base(State.IDLE, stateMachineAI)
        {
            _stateMachineAI = stateMachineAI;
        }

        public override void Enter()
        {
            if (_stateMachineAI.aiAnimator != null)
                _stateMachineAI.aiAnimator.IdleAnim();

            _stateMachineAI.rb.velocity = Vector3.zero;

            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (IsJumping() == true)
                stateMachineAI.ChangeState(_stateMachineAI.jumpState);

            if (IsMoving() == true)
                stateMachineAI.ChangeState(_stateMachineAI.moveState);
        }
    }

    public class Move : BaseStateAI
    {
        private readonly StateMachineAI _stateMachineAI;

        public Move(StateMachineAI stateMachineAI) : base(State.MOVE, stateMachineAI)
        {
            _stateMachineAI = stateMachineAI;
        }

        public override void Enter()
        {
            _stateMachineAI.aiAnimator.RunAnim();

            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (IsJumping() == true)
                stateMachineAI.ChangeState(_stateMachineAI.jumpState);

            if (IsMoving() == false)
                stateMachineAI.ChangeState(_stateMachineAI.idleState);
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();

            Vector2 vel = _stateMachineAI.rb.velocity;

            vel.x = ReturnMoveValue() * _stateMachineAI.moveSpeed;
            _stateMachineAI.rb.velocity = vel;
        }
    }

    public class Jump : BaseStateAI
    {
        private readonly StateMachineAI _stateMachineAI;
        private float _jumpTime = 0.4f;
        private float _jumpTimeCounter = 0.0f;
        private bool _jumped;

        public Jump(StateMachineAI stateMachineAI) : base(State.JUMP, stateMachineAI)
        {
            _stateMachineAI = stateMachineAI;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Jump!");
            _jumpTimeCounter = 0.0f;
            _jumped = false;

            _stateMachineAI.aiAnimator.JumpAnim();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_jumped == true)
            {
                if (IsMoving() == true)
                    stateMachineAI.ChangeState(_stateMachineAI.moveInTheAirState);

                return;
            }

            _jumpTimeCounter += Time.deltaTime;

            if (_jumpTimeCounter >= _jumpTime)
            {
                _jumped = true;

                _stateMachineAI.rb.AddForce(_stateMachineAI.jumpPower * Vector3.up, ForceMode.Impulse);
            }
        }
    }


    public class MoveInTheAir : BaseStateAI
    {
        private readonly StateMachineAI _stateMachineAI;
        private float _moveSpeedPercentInTheAir = 0.75f;

        public MoveInTheAir(StateMachineAI stateMachineAI) : base(State.MOVEINTHEAIR, stateMachineAI)
        {
            _stateMachineAI = stateMachineAI;
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

            Vector2 vel = _stateMachineAI.rb.velocity;

            vel.x = ReturnMoveValue() * _stateMachineAI.moveSpeed * _moveSpeedPercentInTheAir;
            _stateMachineAI.rb.velocity = vel;
        }
    }

    public class Land : BaseStateAI
    {
        private readonly StateMachineAI _stateMachineAI;
        private float _landAnimTime = 0.3f;
        private float _landAnimTimeCounter = 0.0f;

        public Land(StateMachineAI stateMachineAI) : base(State.LAND, stateMachineAI)
        {
            _stateMachineAI = stateMachineAI;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Land!");
            _landAnimTimeCounter = 0.0f;

            _stateMachineAI.aiAnimator.LandAnim();

            if (_stateMachineAI.rb.velocity.y < -1)
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
                _stateMachineAI.ChangeState(_stateMachineAI.idleState);
            }
        }
    }

    public class Death : BaseStateAI
    {
        private readonly StateMachineAI _stateMachineAI;
        private float _landAnimTime = 0.3f;
        private float _landAnimTimeCounter = 0.0f;

        public Death(StateMachineAI stateMachineAI) : base(State.DEATH, stateMachineAI)
        {
            _stateMachineAI = stateMachineAI;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Death!");
        }
    }
}