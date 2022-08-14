using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterStartState : GameState
{
    public AfterStartState()
    {
        gameState = GAMESTATE.AFTERSTART;
    }

    public override void Enter()
    {
        GameEvents.gameStart += StartButtonClicked;
        base.Enter();
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        GameEvents.gameStart -= StartButtonClicked;
    }

    private void StartButtonClicked()
    {
        nextState = new ContinueState();
        stage = EVENT.EXIT;
    }
}