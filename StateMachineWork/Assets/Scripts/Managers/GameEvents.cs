using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static event Action AddressablesLoaded, GameStart, GameEnd;

    private void Awake()
    {
        ResetEvents();
    }

    private void ResetEvents()
    {
        AddressablesLoaded = null;
        GameStart = null;
        GameEnd = null;
    }

    public void AddressablesLoadedEvent()
    {
        AddressablesLoaded?.Invoke();
    }

    public void GameStartEvent()
    {
        GameStart?.Invoke();
    }

    public void GameEndEvent()
    {
        GameEnd?.Invoke();
    }
}