using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security;
using UnityEngine;
using UnityEngine.AI;

public class TargetScript : MonoBehaviour
{
    public float dist;
    private Vector3 newDestination;
    public NavMeshAgent ShadowMesh;
    public NavMeshAgent TargetMesh;
    public PlayerCheck playerCheck;
    // Start is called before the first frame update
    void Awake()
    {
        playerCheck = GameObject.FindWithTag("PlayerCheck").GetComponent<PlayerCheck>();
        TargetMesh = GetComponent<NavMeshAgent>();
        newDestination = new Vector3(UnityEngine.Random.Range(-2, 34), 0, UnityEngine.Random.Range(-3, -47));
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(ShadowMesh.transform.position, gameObject.transform.position);
        RandomPosition();
    }

    void RandomPosition()
    {
        TargetMesh.destination = newDestination;


        if(playerCheck.CheckLineOfSight())
        {
            newDestination = new Vector3(UnityEngine.Random.Range(-2, 34), 0, UnityEngine.Random.Range(-3, -47));
            if (dist < 5)
            {
                gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-2, 34), 0, UnityEngine.Random.Range(-3, -47));
            }
            
        }

    }
}
