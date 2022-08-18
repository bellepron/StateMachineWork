using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static event Action AddressablesLoaded, GameStart, GameSuccess, GameFail;
    public static event Action<AbstractWeapon> WeaponLoaded;

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

        WeaponLoaded = null;
    }

    #region Core
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
    #endregion

    public void WeaponLoadedEvent(AbstractWeapon loadedWeapon)
    {
        print($"{loadedWeapon.transform.name} 123");
        WeaponLoaded?.Invoke(loadedWeapon);
    }
}