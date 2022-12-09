using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCheck : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float coneRange = 5.0f;

    public LayerMask player;
    public LayerMask obstacles;

    public NavMeshAgent agent;


    void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
    } 
    void Update()
    {
    }
    public bool CheckLineOfSight()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, player);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward * -1, dirToTarget) < viewAngle)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacles))
                {
                    agent.speed = 8f;
                    return true;
                    

                }
            }
            else
            {
                agent.speed = 5f;
                return false;
            }
        }
        agent.speed = 5f;
        return false;
    }

}
