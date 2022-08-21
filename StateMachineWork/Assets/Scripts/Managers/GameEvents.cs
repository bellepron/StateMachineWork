using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static event Action AddressablesLoaded;
    public static event Action GameStart, GameSuccess, GameFail;
    public static event Action<AbstractWeapon> WeaponLoaded;

    private void Awake()
    {
        ResetEvents();
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
    #endregion

    public void WeaponLoadedEvent(AbstractWeapon loadedWeapon)
    {
        WeaponLoaded?.Invoke(loadedWeapon);
    }
}