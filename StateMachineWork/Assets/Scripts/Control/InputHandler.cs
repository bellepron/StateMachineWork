using UnityEngine;
using System;

namespace CKY.INPUT
{
    public class InputHandler : Singleton<InputHandler>
    {
        public FixedButton forwardButton, backwardButton;
        public static event Action JumpButtonPressed, AttackButtonPressed;

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