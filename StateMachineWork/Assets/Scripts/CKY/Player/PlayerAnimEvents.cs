using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    #region Variables
    [SerializeField] AbstractWeapon weapon;
    #endregion

    private void OnEnable() => SubscribeEvents();

    private void OnDisable() => UnSubscribeEvents();

    private void OnDestroy() => UnSubscribeEvents();

    private void SubscribeEvents()
    {
        GameEvents.WeaponLoaded += GetLoadedWeapon;
    }

    private void UnSubscribeEvents()
    {
        GameEvents.WeaponLoaded -= GetLoadedWeapon;
    }
    private void GetLoadedWeapon(AbstractWeapon loadedWeapon)
    {
        print($"Holder gets loaded weapon {loadedWeapon}");
        this.weapon = loadedWeapon;
    }

    public void PistolShootAnimEvent()
    {
        weapon.Shoot();
    }
}