using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace CKY.FSM.AI
{
    [System.Serializable]
    public abstract class AbstractAI : MonoBehaviour
    {
        protected NavMeshAgent agent;
        protected float agentSpeed = 10;
        [SerializeField] protected Transform targetTr;

        private void Start()
        {
            GetComponents();
        }

        private void GetComponents()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            agent.destination = targetTr.position;
        }
    }
}