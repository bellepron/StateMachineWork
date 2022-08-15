using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.FSM
{
    [System.Serializable]
    public class BaseState
    {
        public enum StateName { IDLE, MOVE, JUMP }
        public StateName stateName;
        protected StateMachine stateMachine;


        public BaseState(StateName stateName, StateMachine stateMachine)
        {
            this.stateName = stateName;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysics() { }
        public virtual void Exit() { }
    }

    public class Idle : BaseState
    {
        private readonly MovementSM _stateMachine;

        public Idle(MovementSM stateMachine) : base(StateName.IDLE, stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            _stateMachine.mr.material.color = Color.black;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_stateMachine.jumpTrigger == true)
                stateMachine.ChangeState(_stateMachine.jumpState);

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true ||
                CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true)
                stateMachine.ChangeState(_stateMachine.moveState);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Move : BaseState
    {
        private readonly MovementSM _stateMachine;

        public Move(MovementSM stateMachine) : base(StateName.MOVE, stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            _stateMachine.mr.material.color = Color.red;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();

            if (_stateMachine.jumpTrigger == true)
                stateMachine.ChangeState(_stateMachine.jumpState);

            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == false &&
                CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == false)
                stateMachine.ChangeState(_stateMachine.idleState);
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            Vector2 vel = _stateMachine.rb.velocity;
            float moveValue;
            if (CKY.INPUT.InputHandler.Instance.forwardButton.Pressed == true) moveValue = 1;
            else if (CKY.INPUT.InputHandler.Instance.backwardButton.Pressed == true) moveValue = -1;
            else moveValue = 0;
            vel.x = moveValue * _stateMachine.speed;
            _stateMachine.rb.velocity = vel;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }

    public class Jump : BaseState
    {
        private readonly MovementSM _stateMachine;
        private float _jumpPower = 10.0f;

        public Jump(MovementSM stateMachine) : base(StateName.JUMP, stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Zýpla!");
            _stateMachine.rb.AddForce(_jumpPower * Vector3.up, ForceMode.Impulse);
            _stateMachine.mr.material.color = Color.blue;
        }
    }
}