using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState currentGameState;

    [SerializeField] GameObject startPanel;

    private void Start()
    {
        currentGameState = new AfterStartState();
    }

    private void Update()
    {
        currentGameState = currentGameState.Process();
    }

    public void StartButton()
    {
        FindObjectOfType<GameEvents>().GameStart();
    }

    public void EndButton()
    {
        FindObjectOfType<GameEvents>().GameEnd();
    }
}