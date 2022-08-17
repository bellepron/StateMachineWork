using UnityEngine;
using System;

namespace CKY.INPUT
{
    public class InputHandler : Singleton<InputHandler>/*, IInputHandler*/
    {
        public FixedButton forwardButton, backwardButton;
        public FixedButton crouchButton;
        public static event Action JumpButtonPressed, AttackButtonPressed;

        //public bool Crouching;

        //public event Action<bool, float> GroundedChanged;
        //public event Action Jumped;
        //public event Action Attacked;

        //private void Update()
        //{
        //    Crouching = crouchButton.Pressed;
        //}

        public void JumpButton()
        {
            JumpButtonPressed?.Invoke();
        }
        public void AttackButton()
        {
            AttackButtonPressed?.Invoke();
        }
    }
}