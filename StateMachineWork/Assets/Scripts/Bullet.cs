using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10;
    [SerializeField] Rigidbody rb;
    [SerializeField] TrailRenderer tRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        tRenderer.emitting = true;
    }

    private void OnDisable()
    {
        tRenderer.Clear();
        tRenderer.emitting = false;
        rb.velocity = Vector3.zero;
    }

    public void Move(Transform gunTr)
    {
        rb.AddForce(gunTr.forward * speed, ForceMode.Impulse);
    }
}