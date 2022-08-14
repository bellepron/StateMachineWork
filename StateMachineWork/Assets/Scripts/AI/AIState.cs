using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIState : MonoBehaviour
{
    public enum AISTATE { IDLE, PATROL, QUEUE, ATTACK, DEAD }

    public enum EVENT { ENTER, UPDATE, EXIT }

    public AISTATE currentState;
    protected AIState nextState;
    protected EVENT stage;
    protected bool isUpdating = true;

    public void Do()
    {
        Enter();
        StartCoroutine(MyUpdateCoroutine());
    }

    public virtual void Enter()
    {
        print("Enter");
    }

    public virtual void MyUpdate()
    {
        print("Update");
    }

    private IEnumerator MyUpdateCoroutine()
    {
        while (isUpdating == true)
        {
            MyUpdate();

            yield return null;
        }
    }

    public virtual void Exit()
    {
        isUpdating = false;

        print("Exit");
    }
}