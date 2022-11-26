using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class shadowscriptdemo : MonoBehaviour
{
    public Transform movePositionTransform;
    private NavMeshAgent Agent;

    // Start is called before the first frame update
    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Agent.destination = movePositionTransform.position;
    }
}
