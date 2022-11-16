using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;

    [Range(0, 360)]
    public float viewAngle;

    public float maxDistance = 5f;

    float DissolveAmount = 0.01f;

    public LayerMask shadows;
    public LayerMask obstacles;

    MeshRenderer meshRender;

    void Update()
    {
        FindVisibleTargets();
    }

    public bool FindVisibleTargets(){
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, shadows); 
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, maxDistance, obstacles))
                {
                    return true;
                }
                else
                {
                    return false;

                }
            }
        }
        return false;
    }
}
