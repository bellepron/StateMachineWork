using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class EffectManager : Singleton<EffectManager>
{
    private WaitForSeconds _short = new WaitForSeconds(1.0f), _medium = new WaitForSeconds(1.5f), _long = new WaitForSeconds(2.0f);
    public Transform weaponMuzzleTr0, weaponExplosionTr0;
    public List<Transform> landingEffectTrs = new List<Transform>();
    public List<Transform> bloodDirectionalTrs = new List<Transform>();
    public List<Transform> bloodExplosionTrs = new List<Transform>();
    public Transform bloodWide0Tr;

    int bloodDirectionalsCount, bloodExplosionsCount;

    int landing;

    private void Start()
    {
        GameEvents.AddressablesLoaded += AddressablesLoaded;
    }

    private void AddressablesLoaded()
    {
        bloodDirectionalsCount = bloodDirectionalTrs.Count;
        bloodExplosionsCount = bloodExplosionTrs.Count;
    }

    public void PistolMuzzle(Transform gunHeadTr)
    {
        Transform effTr = EZ_PoolManager.Spawn(weaponMuzzleTr0, gunHeadTr.position, Quaternion.Euler(gunHeadTr.eulerAngles));
        effTr.parent = gunHeadTr;

        StartCoroutine(DeSpawnEff(effTr, _short));
    }

    public void PistolExplosion(Vector3 pos, Vector3 euler)
    {
        Transform effTr = EZ_PoolManager.Spawn(weaponExplosionTr0, pos, Quaternion.Euler(euler));

        StartCoroutine(DeSpawnEff(effTr, _long));
    }

    #region Landing
    public void LandingEffect(Vector3 pos, Vector3 euler)
    {
        landing = landing == 0 ? 1 : 0;
        Transform effTr = EZ_PoolManager.Spawn(landingEffectTrs[landing], pos, Quaternion.Euler(euler));

        StartCoroutine(DeSpawnEff(effTr, _long));
    }
    #endregion

    #region Blood
    public void BloodDirectional(Vector3 pos, Vector3 euler)
    {
        int random = Random.Range(0, bloodDirectionalsCount);
        Transform effTr = EZ_PoolManager.Spawn(bloodDirectionalTrs[random], pos, Quaternion.Euler(euler));

        StartCoroutine(DeSpawnEff(effTr, _long));
    }
    public void BloodExplosion(Vector3 pos, Vector3 euler)
    {
        int random = Random.Range(0, bloodExplosionsCount);
        Transform effTr = EZ_PoolManager.Spawn(bloodExplosionTrs[random], pos, Quaternion.Euler(euler));

        StartCoroutine(DeSpawnEff(effTr, _long));
    }
    public void BloodWide(Vector3 pos, Vector3 euler)
    {
        Transform effTr = EZ_PoolManager.Spawn(bloodWide0Tr, pos, Quaternion.Euler(euler));

        StartCoroutine(DeSpawnEff(effTr, _long));
    }
    #endregion

    private IEnumerator DeSpawnEff(Transform effTr, WaitForSeconds waitForSec)
    {
        yield return waitForSec;

        EZ_PoolManager.Despawn(effTr);
    }
}