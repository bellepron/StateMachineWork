using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEvents _gameEvents;
    [SerializeField] private GameObject loadingPanel, startPanel, successPanel, failPanel;

    private void Start()
    {
        loadingPanel.SetActive(true);
        startPanel.SetActive(false);
        successPanel.SetActive(false);
        failPanel.SetActive(false);

        GameEvents.AddressablesLoaded += AddressablesLoaded;
        GameEvents.GameStart += GameStart;
    }

    private void AddressablesLoaded()
    {
        startPanel.SetActive(true);
        loadingPanel.SetActive(false);
    }

    public void StartPanelClicked()
    {

    }

    private void GameStart()
    {
        startPanel.SetActive(false);
    }
}