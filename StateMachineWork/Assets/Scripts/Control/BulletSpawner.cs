using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Singleton<BulletSpawner>
{
    [SerializeField] Bullet bulletPrefab;

    public void SpawnPistolBullet(float speed, Transform gunHeadTr)
    {
        Bullet bullet = Instantiate(bulletPrefab, gunHeadTr.position, gunHeadTr.rotation);
        bullet.speed = speed;
        bullet.Move(gunHeadTr);
    }
}