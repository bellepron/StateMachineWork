using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.Player.FSM
{
    [System.Serializable]
    public class BaseStatePlayer
    {
        public enum State { IDLE, MOVE, JUMP }
        public State state;
        protected StateMachinePlayerController StateMachinePlayerController;


        public BaseStatePlayer(State state, StateMachinePlayerController _stateMachinePlayerController)
        {
            this.state = state;
            this.StateMachinePlayerController = _stateMachinePlayerController;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }
    }

    public class Idle : BaseStatePlayer
    {
        private readonly StateMachinePlayerController _stateMachinePlayerController;

        public Idle(StateMachinePlayerController stateMachinePlayerController) : base(State.IDLE, stateMachinePlayerController)
        {
            _stateMachinePlayerController = stateMachinePlayerController;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_stateMachinePlayerController.jumpTrigger == true)
                StateMachinePlayerController.ChangeState(_stateMachinePlayerController.jumpState);

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true ||
                CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true)
                StateMachinePlayerController.ChangeState(_stateMachinePlayerController.moveState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Move : BaseStatePlayer
    {
        private readonly StateMachinePlayerController _stateMachinePlayerController;

        public Move(StateMachinePlayerController stateMachinePlayerController) : base(State.MOVE, stateMachinePlayerController)
        {
            _stateMachinePlayerController = stateMachinePlayerController;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_stateMachinePlayerController.jumpTrigger == true)
                StateMachinePlayerController.ChangeState(_stateMachinePlayerController.jumpState);

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == false &&
                CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == false)
                StateMachinePlayerController.ChangeState(_stateMachinePlayerController.idleState);
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            Vector2 vel = _stateMachinePlayerController.rb.velocity;
            float moveValue;

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true)
                moveValue = 1;
            else if (CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true)
                moveValue = -1;
            else moveValue = 0;


            //_stateMachinePlayerCOntroller.playerAnimator.Move(moveValue);

            vel.x = moveValue * _stateMachinePlayerController.moveSpeed;
            _stateMachinePlayerController.rb.velocity = vel;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Jump : BaseStatePlayer
    {
        private readonly StateMachinePlayerController _stateMachinePlayerCOntroller;
        private float _jumpPower = 10.0f;

        public Jump(StateMachinePlayerController stateMachinePlayerCOntroller) : base(State.JUMP, stateMachinePlayerCOntroller)
        {
            _stateMachinePlayerCOntroller = stateMachinePlayerCOntroller;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Zýpla!");
            _stateMachinePlayerCOntroller.rb.AddForce(_jumpPower * Vector3.up, ForceMode.Impulse);
        }
    }
}