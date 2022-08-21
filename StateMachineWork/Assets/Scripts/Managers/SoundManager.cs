using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource audioSource;
    [HideInInspector] public AudioClip fireSound; // TODO: Get via Addressable manager.

    public void Fire(Vector3 pos)
    {

    }
}