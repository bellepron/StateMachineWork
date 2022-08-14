using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Medium.FSM
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
        private float _horizontalInput;
        private bool _jumpInput;

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
            _jumpInput = Input.GetKeyDown(KeyCode.Space);

            if (Mathf.Abs(_horizontalInput) > Mathf.Epsilon)
                stateMachine.ChangeState(_stateMachine.moveState);

            if (_jumpInput == true)
                stateMachine.ChangeState(_stateMachine.jumpState);
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
        private bool _jumpInput;

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
            _jumpInput = Input.GetKeyDown(KeyCode.Space);

            if (Mathf.Abs(_horizontalInput) < Mathf.Epsilon)
                stateMachine.ChangeState(_stateMachine.idleState);

            if (_jumpInput == true)
                stateMachine.ChangeState(_stateMachine.jumpState);
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
            _stateMachine.rb.AddForce(_jumpPower * Vector3.up, ForceMode.Impulse);
            _stateMachine.mr.material.color = Color.blue;
        }

        public override void UpdatePhysics()
        {
            base.UpdatePhysics();

            // Aþþa ray At.
        }
    }
}