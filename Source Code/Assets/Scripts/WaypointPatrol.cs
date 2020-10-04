using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    //Components
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    //Variables
    int currentWaypointIndex;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}
