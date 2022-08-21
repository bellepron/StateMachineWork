using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    public GameSettings gameSettings;
    [SerializeField] private GameSaveManager gameSaveManager;

    [SerializeField] private GameEvents _gameEvents;
    [SerializeField] private GameObject loadingPanel, startPanel, successPanel, failPanel;

    private void Start()
    {
        LoadGameSettingsFromJSON();

        SetPanelsAtStart();

        GameEvents.AddressablesLoaded += AddressablesLoaded;
        GameEvents.GameStart += GameStart;
    }

    private void LoadGameSettingsFromJSON()
    {
        GameSaveManager.Instance.LoadGame();
    }

    private void SetPanelsAtStart()
    {
        loadingPanel.SetActive(true);
        startPanel.SetActive(false);
        successPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    private void AddressablesLoaded()
    {
        startPanel.SetActive(true);
        loadingPanel.SetActive(false);
    }

    public void StartPanelClicked()
    {
        _gameEvents.GameStartEvent();
    }

    private void GameStart()
    {
        startPanel.SetActive(false);
    }

    public void SaveToJSON()
    {
        gameSaveManager.SaveGame();
    }
}