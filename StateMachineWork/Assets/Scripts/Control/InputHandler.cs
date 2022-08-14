using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour, IPointerUpHandler
{
    //[SerializeField] Button forwardButton, backwardButton, jumpButton, attackButton;

    public static event Action ForwardButtonPressing;
    public static event Action BackwardButtonPressing;
    public static event Action JumpButtonPressed, AttackButtonPressed;

    public void ForwardButton()
    {
        ForwardButtonPressing?.Invoke();
    }
    public void BackwardButton()
    {
        BackwardButtonPressing?.Invoke();
    }
    public void JumpButton()
    {
        JumpButtonPressed?.Invoke();
    }
    public void AttackButton()
    {
        AttackButtonPressed?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Parmak çekildi");
    }
}