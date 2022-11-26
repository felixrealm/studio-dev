
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class shadowsscript : MonoBehaviour
{
    public PlayerCheck playerCheck;
    //FieldOfView Player CHECK
    public LayerMask player;
    public LayerMask obstacles;

    //Dot Product
    [Range(-1, 1)]
    public float HideSensitivity = 0f;

     [Range(0, 10)]
    public float MinDistancePlayer = 30f;

    //Character Attributes
    public float speed = 3.0f;

    //NavMesh
    public NavMeshAgent Agent;

    //Courotines
    private Coroutine MovementCoroutine;
    private Collider[] Colliders = new Collider[10]; //Amount of obstacles the shadows consider. The more, the more expensive it gets.

    void Awake()
    {  
        Agent = GetComponent<NavMeshAgent>();
    }
    void OnDestroy()
    {
    }
    void HandleGainSight(Transform Target)
    {
        if(MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
        MovementCoroutine = StartCoroutine(Hide(Target));
    }
    void HandleLossSight(Transform Target)
    {
         if(MovementCoroutine != null)
        {
            StopCoroutine(MovementCoroutine);
        }
    }
    private IEnumerator Hide(Transform Target)
    {
        while (true)
        {
           int hits = Physics.OverlapSphereNonAlloc(Agent.transform.position, playerCheck.viewRadius, Colliders, obstacles);

           int hitReduction = 0;
           for(int i = 0; i > hits; i++)
           {
                if(Vector3.Distance(Colliders[i].transform.position, Target.position) < MinDistancePlayer)
                {
                    Colliders[i] = null;
                    hitReduction++;
                }
           }
            hits -= hitReduction;

           System.Array.Sort(Colliders, ColliderArraySortCompare);

           for(int i  = 0; i < hits; i++)
           {
                if(NavMesh.SamplePosition(Colliders[i].transform.position, out NavMeshHit hit, 2f, Agent.areaMask))
                {
                   if(!NavMesh.FindClosestEdge(hit.position, out hit, Agent.areaMask)) 
                   {
                    UnityEngine.Debug.LogError($"Unable to find an edge to hide to {hit.position}");
                   }
                   if(Vector2.Dot(hit.normal, (Target.position - hit.position).normalized) < HideSensitivity)
                   {
                        Agent.SetDestination(hit.position);
                        break;
                   }
                   else
                   {
                        if(NavMesh.SamplePosition(Colliders[i].transform.position - (Target.position - hit.position).normalized * 2, out NavMeshHit hit2, 2f, Agent.areaMask))
                        {
                            if(!NavMesh.FindClosestEdge(hit2.position, out hit2, Agent.areaMask)) 
                            {
                                UnityEngine.Debug.LogError($"Unable to find an edge to hide to {hit2.position} (second attempt)");
                            }
                            if(Vector2.Dot(hit2.normal, (Target.position - hit2.position).normalized) < HideSensitivity)
                            {
                                Agent.SetDestination(hit2.position);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    UnityEngine.Debug.LogError($"Unable to find a NavMesh near ob");
                }   
            }
            yield return null;
        }
    }
    private int ColliderArraySortCompare(Collider A, Collider B)
    {
        if(A == null && B != null)
        {
            return 1;

        }
        else if(A != null && B == null)
        {
            return -1;
        }
        else if(A == null && B == null)
        {
            return 0;
        } 
        else
        {
            return Vector3.Distance(Agent.transform.position, A.transform.position).CompareTo(Vector3.Distance(Agent.transform.position, B.transform.position));
        }
    }
}