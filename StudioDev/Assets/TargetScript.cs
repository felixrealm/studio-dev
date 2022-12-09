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

    public AudioSource ghostLaugh;

    public float currentTime = 0f;

    public float StartingTime = 10f;
    // Start is called before the first frame update
    void Awake()
    {
        currentTime = StartingTime;
        playerCheck = GameObject.FindWithTag("PlayerCheck").GetComponent<PlayerCheck>();
        TargetMesh = GetComponent<NavMeshAgent>();
        newDestination = new Vector3(UnityEngine.Random.Range(-2, 34), 0.83f, UnityEngine.Random.Range(-3, -40));
    }

    // Update is called once per frame
    void Update()
    {
        StartingTime -= 1 * Time.deltaTime;
        dist = Vector3.Distance(ShadowMesh.transform.position, gameObject.transform.position);
        RandomPosition();
        if (StartingTime <= 0)
        {
            ghostLaugh.Play();
            StartingTime = 10f;
        }
        
    }

    void RandomPosition()
    {
        TargetMesh.destination = newDestination;


        if(playerCheck.CheckLineOfSight())
        {
            newDestination = new Vector3(UnityEngine.Random.Range(-2, 30), 0.83f, UnityEngine.Random.Range(-3, -40));
            if (dist < 5)
            {
                gameObject.transform.position = new Vector3(UnityEngine.Random.Range(-2, 34), 0.83f, UnityEngine.Random.Range(-3, -40));
            }
            
        }

    }
}
