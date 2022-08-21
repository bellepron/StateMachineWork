using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Scriptable Objects/Game/New Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Player")]
    [Space]
    public float playerMoveSpeed = 10.0f;
    public float playerJumpPower = 10.0f;
    public float maxHealth = 100.0f;
    public float currentHealth = 100.0f;
}