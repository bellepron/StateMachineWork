using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Transform gunTr)
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(gunTr.forward * speed, ForceMode.Impulse);
    }
}