using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEditor.PackageManager;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera activeCam;
    [SerializeField] private CinemachineVirtualCamera firstCam, gameCam, successCam, failCam;
    [SerializeField] private CinemachineImpulseSource impulseSource;

    private void Start()
    {
        GameEvents.AddressablesLoaded += AdressablesLoaded;
    }

    private void AdressablesLoaded()
    {
        activeCam = firstCam;
        ChangeCamera(firstCam);

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        GameEvents.GameStart += OnStart;
        GameEvents.GameSuccess += SuccessCam;
        GameEvents.GameFail += FailCam;
    }

    private void UnSubscribeEvents()
    {
        GameEvents.GameStart -= OnStart;
        GameEvents.GameSuccess -= SuccessCam;
        GameEvents.GameFail -= FailCam;
    }

    private void OnStart()
    {
        GameCam();
    }

    public void FirstCam()
    {
        ChangeCamera(firstCam);
    }

    public void GameCam()
    {
        ChangeCamera(gameCam);
    }

    IEnumerator Delay0(CinemachineVirtualCamera v)
    {
        v.Follow = null;
        v.LookAt = null;

        yield return new WaitForSeconds(1f);

    }

    public void SuccessCam()
    {
        StartCoroutine(SuccesCamDelay(activeCam));
    }

    IEnumerator SuccesCamDelay(CinemachineVirtualCamera v)
    {
        yield return new WaitForSeconds(0.75f);

        ChangeCamera(successCam);
    }

    public void FailCam()
    {
        ChangeCamera(failCam);
    }

    private void ChangeCamera(CinemachineVirtualCamera x)
    {
        if (activeCam == x) return;

        activeCam.Priority = 0;
        activeCam = x;

        activeCam.Priority = 10;
    }

    public void Shake(float veloX, float veloY, float veloZ)
    {
        impulseSource.m_DefaultVelocity = new Vector3(veloX, veloY, veloZ);
        impulseSource.GenerateImpulse();
    }
}