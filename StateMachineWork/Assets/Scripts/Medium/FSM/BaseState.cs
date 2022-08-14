using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Medium.FSM
{
    [System.Serializable]
    public class BaseState
    {
        public enum StateName { IDLE, MOVE }
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
        private float _horizontalInput;

        public Idle(MovementSM stateMachine) : base(StateName.IDLE, stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            _horizontalInput = 0f;
            _stateMachine.mr.material.color = Color.black;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            _horizontalInput = Input.GetAxis("Horizontal");

            if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
                stateMachine.ChangeState(_stateMachine.movingState);
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exit Ide");
        }
    }
    public class Move : BaseState
    {
        private readonly MovementSM _stateMachine;
        private float _horizontalInput;

        public Move(MovementSM stateMachine) : base(StateName.MOVE, stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            _horizontalInput = 0f;
            _stateMachine.mr.material.color = Color.red;
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            _horizontalInput = Input.GetAxis("Horizontal");

            if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
                stateMachine.ChangeState(_stateMachine.idleState);
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();
            Vector2 vel = _stateMachine.rb.velocity;
            vel.x = _horizontalInput * _stateMachine.speed;
            _stateMachine.rb.velocity = vel;
        }

        public override void Exit()
        {
            base.Exit();
            Debug.Log("Exit Move");
        }
    }
}