using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static event Action gameStart, gameEnd;

    private void Awake()
    {
        ResetEvents();
    }

    private void ResetEvents()
    {
        gameStart = null;
        gameEnd = null;
    }

    public void GameStart()
    {
        gameStart?.Invoke();
    }

    public void GameEnd()
    {
        gameEnd?.Invoke();
    }
}