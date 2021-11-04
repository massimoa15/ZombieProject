using System;
using UnityEngine;
using UnityEngine.AI;

public class SimpleAIMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    private void Update()
    {
        //agent.SetDestination(target.position);
        if (GlobalData.PlayerObject != null)
        {
            agent.SetDestination(GlobalData.PlayerObject.transform.position);
        }
    }
}
