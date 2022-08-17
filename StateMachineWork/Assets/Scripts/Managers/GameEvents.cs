using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static event Action AddressablesLoaded, GameStart, GameSuccess, GameFail;

    private void Awake()
    {
        ResetEvents();
    }

    private void ResetEvents()
    {
        AddressablesLoaded = null;
        GameStart = null;
        GameSuccess = null;
        GameFail = null;
    }

    public void AddressablesLoadedEvent()
    {
        AddressablesLoaded?.Invoke();
    }

    public void GameStartEvent()
    {
        GameStart?.Invoke();
    }

    public void GameSuccessEvent()
    {
        GameSuccess?.Invoke();
    }

    public void GameFailEvent()
    {
        GameFail?.Invoke();
    }
}