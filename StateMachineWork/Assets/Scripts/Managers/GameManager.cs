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
    private WaitForSeconds _wfs = new WaitForSeconds(1.5f);

    private void Start()
    {
        //LoadGameSettingsFromJSON();

        SetPanelsAtStart();

        SubscribeEvents();
    }

    #region Event Operations
    private void SubscribeEvents()
    {
        GameEvents.AddressablesLoaded += AddressablesLoaded;
        GameEvents.GameStart += GameStart;
        GameEvents.GameFail += GameFail;
    }

    private void UnSubscribeEvents()
    {
        GameEvents.AddressablesLoaded -= AddressablesLoaded;
        GameEvents.GameStart -= GameStart;
        GameEvents.GameFail -= GameFail;
    }

    private void OnDestroy()
    {
        UnSubscribeEvents();
    }
    #endregion

    //private void LoadGameSettingsFromJSON()
    //{
    //    GameSaveManager.Instance.LoadGame();
    //}

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

    private void GameFail()
    {
        StartCoroutine(FailPanelDelay());
    }
    private IEnumerator FailPanelDelay()
    {
        yield return _wfs;

        failPanel.SetActive(true);
    }

    public void Restart()
    {
        failPanel.SetActive(false);

        _gameEvents.GameRestartEvent();
    }

    public void SaveToJSON()
    {
        gameSaveManager.SaveGame();
    }
}