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

    private void FixedUpdate()
    {
        //Only pick a target if there are players in game - determined by whether there are player objects in the list of objects
        if (GlobalData.PlayerObjects.Count > 0)
        {
            agent.SetDestination(GetClosestPlayer().transform.position);
        }
    }

    public GameObject GetClosestPlayer()
    {
        //Save the current position of this agent
        Vector3 curPos = transform.position;

        //By default, set the first player as the closest player
        int closestPlayerIndex = 0;
        float curClosestDist = Vector3.Distance(curPos, GlobalData.PlayerObjects[0].transform.position);

        //Loop through all of the player objects and find which one is closest
        for (int i = 1; i < GlobalData.PlayerObjects.Count; i++)
        {
            float tempDist;
            if ((tempDist = Vector3.Distance(curPos, GlobalData.PlayerObjects[i].transform.position)) < curClosestDist)
            {
                closestPlayerIndex = i;
                curClosestDist = tempDist;
            }
        }

        return GlobalData.PlayerObjects[closestPlayerIndex];
    }
}
