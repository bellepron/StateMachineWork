using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static event Action AddressablesLoaded;
    public static event Action GameStart, GameSuccess, GameFail, GameRestart;
    public static event Action<AbstractWeapon> WeaponLoaded;

    private void Awake()
    {
        ResetEvents();

        GameRestart = null;
    }

    public void ResetEvents()
    {
        AddressablesLoaded = null;
        GameStart = null;
        GameSuccess = null;
        GameFail = null;

        WeaponLoaded = null;
    }

    #region Core
    public void AddressablesLoadedEvent()
    {
        AddressablesLoaded?.Invoke();
    }
    #endregion

    #region Basic Game Flow
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

    public void GameRestartEvent()
    {
        GameRestart?.Invoke();
    }
    #endregion

    public void WeaponLoadedEvent(AbstractWeapon loadedWeapon)
    {
        WeaponLoaded?.Invoke(loadedWeapon);
    }
}