using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using LevelEditingProjectAI;

 namespace LevelEditingProjectAI { 
public enum AIState { None, Patrol, Chaise }
[System.Serializable]
public class WaypointBase
{
    [SerializeField] Transform destination;
    [SerializeField] float waitTime;
    public Transform Destination { get => destination; set => destination = value; }
    public float WaitTime { get => waitTime; set => waitTime = value; }
}
[System.Serializable]
public class PatrolSettings
{
    public WaypointBase[] waypoints;
}


[System.Serializable]
public class SightSettings
{
    [SerializeField] float sightRange = 10f;
    [SerializeField] float fieldOfView = 60f;
    [SerializeField] LayerMask sightLayers;

    public float SightRange { get => sightRange; set => sightRange = value; }
    public float FieldOfView { get => fieldOfView; set => fieldOfView = value; }
    public LayerMask SightLayers { get => sightLayers; set => sightLayers = value; }
}

}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyPatrolling : MonoBehaviour
{
    private NavMeshAgent navMeshAgent { get => GetComponent<NavMeshAgent>(); }
    [SerializeField] AIState aiState;
    [SerializeField] PatrolSettings patrolSetting;
    [SerializeField] SightSettings sightSetting;
    [SerializeField] GameObject player;
    private float currentWaitTime;
    private int waypointIndex = 0;
    private bool setDist;
    private float remainingDistance;
    private Transform target;
    private bool caught;

    void Start()
    {
        setDist = true;
        aiState = AIState.Patrol;
    }

    

    // Update is called once per frame
    void Update()
    {
        //if target is in range.. ro7lo w gameoverPanel b2a 
        if (!caught && IsTargetInRange())
        {
            aiState = AIState.Chaise;
        }

        switch (aiState)
        {
            case AIState.Patrol:
                Patrol();
                break;
            case AIState.Chaise:
                Chaise(target);
                if (target.GetComponent<playerMovement>())
                {
                    target.GetComponent<playerMovement>().enabled = false;
                    target.GetComponent<Animator>().enabled = false;
                }
                break;
            case AIState.None:
                UIManager.Instance.ActivateGameOverPanel();
                Debug.Log("Game Over");
                break;
        }
    }

    bool IsTargetInRange()
    {
        bool detected;
        float sightAngle; 

        Vector3 start = transform.position + (transform.up / 2); //sight eyeheight
        Vector3 dir = (player.transform.position) - start;

        Debug.DrawRay(start, dir.normalized * sightSetting.SightRange, Color.red);

        sightAngle = Vector3.Angle(dir, transform.forward);
        detected = Physics.Raycast(start, dir, out RaycastHit hit, sightSetting.SightRange, sightSetting.SightLayers);
       
        if (detected && sightAngle < sightSetting.FieldOfView / 2)
        {
            target = hit.transform;
            return true;
        }
        else {
            return false;
        }
    }

    void Patrol()
    {
        if (target == null)
        {
            if (!navMeshAgent.isOnNavMesh)
                return;

            if (patrolSetting.waypoints.Length == 0)
                return;


            if (setDist)
            {
                if (patrolSetting.waypoints[waypointIndex].Destination != null)
                {
                    navMeshAgent.SetDestination(patrolSetting.waypoints[waypointIndex].Destination.position);
                    setDist = false;
                }
            }
            remainingDistance = Vector3.Distance(transform.position, navMeshAgent.destination);
            if (remainingDistance <= navMeshAgent.stoppingDistance)
            {
                currentWaitTime -= Time.deltaTime; // or coroutine insted of currentWaitTime

                if (currentWaitTime <= 0)
                {
                    if (waypointIndex < patrolSetting.waypoints.Length)
                    {
                        waypointIndex = (waypointIndex + 1) % patrolSetting.waypoints.Length;
                        setDist = true;
                    }
                }
            }
            else
            {
                currentWaitTime = patrolSetting.waypoints[waypointIndex].WaitTime;
            }
        }
    }


    private void Chaise(Transform target)
    {
        navMeshAgent.stoppingDistance =3;
        navMeshAgent.SetDestination(target.position);
        caught = true;
        aiState = AIState.None;
    }


    void OnDrawGizmosSelected()
    {
        Color c = Color.red;
        c.a = 0.2f;
        UnityEditor.Handles.color = c;
        UnityEditor.Handles.DrawSolidDisc(transform.position, Vector3.up, sightSetting.SightRange);

        Vector3 fovLine1 = Quaternion.AngleAxis(sightSetting.FieldOfView / 2, transform.up) * transform.forward * sightSetting.SightRange;
        Vector3 fovLine2 = Quaternion.AngleAxis(-sightSetting.FieldOfView / 2, transform.up) * transform.forward * sightSetting.SightRange;

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position , fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
    }
}
