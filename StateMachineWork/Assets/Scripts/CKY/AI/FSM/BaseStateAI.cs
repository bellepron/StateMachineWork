using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.AI.FSM
{
    [System.Serializable]
    public class BaseStateAI
    {
        public enum State { IDLE, MOVE, JUMP }
        public State state;
        protected StateMachineAIController stateMachineAIController;


        public BaseStateAI(State stateName, StateMachineAIController stateMachineController)
        {
            this.state = stateName;
            this.stateMachineAIController = stateMachineController;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }
    }

    public class Idle : BaseStateAI
    {
        private readonly StateMachineAIController _stateMachineAIController;

        public Idle(StateMachineAIController stateMachineAIController) : base(State.IDLE, stateMachineAIController)
        {
            _stateMachineAIController = stateMachineAIController;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_stateMachineAIController.jumpTrigger == true)
                stateMachineAIController.ChangeState(_stateMachineAIController.jumpState);

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true ||
                CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true)
                stateMachineAIController.ChangeState(_stateMachineAIController.moveState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Move : BaseStateAI
    {
        private readonly StateMachineAIController _stateMachineAIController;

        public Move(StateMachineAIController stateMachineAICOntroller) : base(State.MOVE, stateMachineAICOntroller)
        {
            _stateMachineAIController = stateMachineAICOntroller;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_stateMachineAIController.jumpTrigger == true)
                stateMachineAIController.ChangeState(_stateMachineAIController.jumpState);

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == false &&
                CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == false)
                stateMachineAIController.ChangeState(_stateMachineAIController.idleState);
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            Vector2 vel = _stateMachineAIController.rb.velocity;
            float moveValue;
            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true) moveValue = 1;
            else if (CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true) moveValue = -1;
            else moveValue = 0;
            vel.x = moveValue * _stateMachineAIController.speed;
            _stateMachineAIController.rb.velocity = vel;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Jump : BaseStateAI
    {
        private readonly StateMachineAIController _stateMachineAICOntroller;
        private float _jumpPower = 10.0f;

        public Jump(StateMachineAIController stateMachineAICOntroller) : base(State.JUMP, stateMachineAICOntroller)
        {
            _stateMachineAICOntroller = stateMachineAICOntroller;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Zýpla!");
            _stateMachineAICOntroller.rb.AddForce(_jumpPower * Vector3.up, ForceMode.Impulse);
        }
    }
}