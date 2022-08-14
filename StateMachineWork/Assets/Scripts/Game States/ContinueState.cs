using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueState : GameState
{
    public ContinueState()
    {
        gameState = GAMESTATE.CONTINUE;
    }

    public override void Enter()
    {
        GameEvents.gameEnd += GameEnd;
        base.Enter();
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        GameEvents.gameEnd -= GameEnd;
    }

    private void GameEnd()
    {
        nextState = new EndState();
        stage = EVENT.EXIT;
    }
}