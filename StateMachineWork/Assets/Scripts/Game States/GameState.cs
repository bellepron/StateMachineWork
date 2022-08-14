using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public enum GAMESTATE
    {
        AFTERSTART, CONTINUE, END
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public GAMESTATE gameState;
    protected GameState nextState;
    protected EVENT stage;

    public GameState()
    {
        stage = EVENT.ENTER;
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public GameState Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }
}