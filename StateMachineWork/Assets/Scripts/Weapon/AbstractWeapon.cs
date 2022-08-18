using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class AbstractWeapon : MonoBehaviour
{
    public float speed = 10;
    public float damage = 50;
    [SerializeField] Transform gunHeadTr;

    public void Shoot()
    {
        BulletSpawner.Instance.SpawnPistolBullet(speed, gunHeadTr);
    }
}