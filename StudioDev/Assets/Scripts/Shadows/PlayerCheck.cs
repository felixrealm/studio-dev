using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public float coneRange = 5.0f;

    public LayerMask player;
    public LayerMask obstacles;

    public SphereCollider Collider;
    public float FieldOfView = 90;

    public delegate void GainSightEvent(Transform Target); 
    public GainSightEvent OnGainSight;
    public delegate void LseSightEvent(Transform Target); 
    public GainSightEvent OnLoseSight;

    private Coroutine CheckForLineOfSightCoroutine;

    void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    } 
    void Update()
    {
        if(CheckLineOfSight())
        {
            CheckForLineOfSightCoroutine = StartCoroutine(CheckForLineOfSight());
        }
        else
        {
            if(CheckForLineOfSightCoroutine != null)
            {
                StopCoroutine(CheckForLineOfSightCoroutine);
            }
        }
    }
    private bool CheckLineOfSight()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, player);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward * - 1, dirToTarget) < viewAngle)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, coneRange, obstacles))
                {
                    OnGainSight?.Invoke(target);
                    return true;
                    
                }
            }
            else
            {
                OnLoseSight?.Invoke(target);
                return false;
            }
        }
        return false;
            
    }
    private IEnumerator CheckForLineOfSight()
    {
        WaitForSeconds wait = new WaitForSeconds(0.5f);

        while(!CheckLineOfSight())
        {
            yield return wait;
        }
    }

}
