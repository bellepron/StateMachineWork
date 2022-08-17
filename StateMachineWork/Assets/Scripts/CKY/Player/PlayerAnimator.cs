using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CKY.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        //[SerializeField] private float _minImpactForce = 20;

        //// Anim times can be gathered from the state itself, but 
        //// for the simplicity of the video...
        //[SerializeField] private float _landAnimDuration = 0.1f;
        //[SerializeField] private float _attackAnimTime = 0.2f;

        //private IInputHandler _player;
        [SerializeField] private Animator anim;

        private bool _grounded;
        private float _lockedTill;
        private bool _jumpTriggered;
        private bool _attacked;
        private bool _landed;

        #region Cached Properties

        private int _currentState;

        private static readonly int Idle = Animator.StringToHash("Idle BT");
        private static readonly int Run = Animator.StringToHash("Run BT");
        private static readonly int Jump = Animator.StringToHash("Jump BT");
        private static readonly int Attack = Animator.StringToHash("Attack BT");
        private static readonly int Crouch = Animator.StringToHash("Stand To Kneel BT");
        //private static readonly int Fall = Animator.StringToHash("Fall");
        //private static readonly int Land = Animator.StringToHash("Land");

        #endregion

        #region TARO
        //private void Awake()
        //{
        //    if (!TryGetComponent(out IInputHandler player))
        //    {
        //        Destroy(this);
        //        return;
        //    }

        //    _player = player;
        //    _anim = GetComponent<Animator>();
        //}


        //private void Start()
        //{
        //    _player.Jumped += () =>
        //    {
        //        _jumpTriggered = true;
        //    };
        //    _player.Attacked += () =>
        //    {
        //        _attacked = true;
        //    };
        //    _player.GroundedChanged += (grounded, impactForce) =>
        //    {
        //        _grounded = grounded;
        //        _landed = impactForce >= _minImpactForce;
        //    };
        //}

        //private void Update()
        //{
        //    //if (_player.Input.x != 0) _renderer.flipX = _player.Input.x < 0;

        //    var state = GetState();

        //    _jumpTriggered = false;
        //    _landed = false;
        //    _attacked = false;

        //    if (state == _currentState) return;
        //    _anim.CrossFade(state, 0, 0);
        //    _currentState = state;
        //}

        //private int GetState()
        //{
        //    if (Time.time < _lockedTill) return _currentState;

        //    // Priorities
        //    if (_attacked) return LockState(Attack, _attackAnimTime);
        //    if (_player.Crouching) return Crouch;
        //    if (_landed) return LockState(Land, _landAnimDuration);
        //    if (_jumpTriggered) return Jump;

        //    if (_grounded) return _player.Input.x == 0 ? Idle : Run;
        //    return _player.Speed.y > 0 ? Jump : Fall;

        //    int LockState(int s, float t)
        //    {
        //        _lockedTill = Time.time + t;
        //        return s;
        //    }
        //}
        #endregion

        private void Start()
        {
            anim.CrossFade(Idle, 0, 0);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                anim.CrossFade(Run, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                anim.CrossFade(Jump, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                anim.CrossFade(Attack, 0, 1);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                anim.CrossFade(Crouch, 0, 0);
            }
        }
    }
}

#region Taro Interface
//public interface IInputHandler
//{
//    public bool Crouching { get; }

//    public event Action<bool, float> GroundedChanged; // Grounded - Impact force
//    public event Action Jumped;
//    public event Action Attacked;
//}
#endregion