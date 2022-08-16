using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class BulletSpawner : Singleton<BulletSpawner>
{
    public Transform bulletPrefabTr;

    public void SpawnPistolBullet(float speed, Transform gunHeadTr)
    {
        Transform bulletTr = EZ_PoolManager.Spawn(bulletPrefabTr, gunHeadTr.position, gunHeadTr.rotation);
        Bullet bullet = bulletTr.GetComponent<Bullet>();
        bullet.speed = speed;
        bullet.Move(gunHeadTr);
    }
}