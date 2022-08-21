using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;

public class Spikes : MonoBehaviour
{
    [SerializeField] float damage = 50.0f;

    private void Start()
    {
        this.OnCollisionEnterAsObservable().Subscribe(collision => OnCollision(collision));
    }

    private void OnCollision(Collision collision)
    {
        IDamageable iDamageable = collision.transform.GetComponent<IDamageable>();

        if (iDamageable == null) return;

        iDamageable.GetDamage(damage, this.transform);
    }
}