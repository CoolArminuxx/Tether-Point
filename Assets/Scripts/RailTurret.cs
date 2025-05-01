using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RailTurret : MonoBehaviour
{
    [SerializeField] NavMeshAgent TurretAgent;
    [SerializeField] GameObject[] waypoints;
    private int waypointsIndex;
    private Vector3 target;

    private void Start()
    {
        waypoints = GameObject.FindGameObjectsWithTag("Waypoint");//Store all waypoints in array
        TurretAgent = GetComponent<NavMeshAgent>();//Store navmesh component
        UpdateDestination();//Call on destination function which updates the next waypoint that the enemy must travel to
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1)//If turrest is next to the target / Then Update the next destination and iterate the next waypoint in line
        {
            UpdateDestination();
            IterateWaypointsIndex();
        }
    }
    void UpdateDestination()//Set target to current waypoint in waypoint index
    {
        target = waypoints[waypointsIndex].transform.position;
        TurretAgent.SetDestination(target);
    }
    void IterateWaypointsIndex()//Pick next waypoint in the waypoint array / Loop if end of array has been reached
    {
        waypointsIndex++;

        if (waypointsIndex >= waypoints.Length)
        {
            waypointsIndex = 0;
        }
    }
}
