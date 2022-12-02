using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

    public List<GameObject> targetCaught = new List<GameObject>();

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

                if(!Physics.Raycast(transform.position, dirToTarget, out RaycastHit  hit,maxDistance, obstacles))
                {
                    target.gameObject.GetComponentInParent<BlendShadows>().reset();
                    if(!targetCaught.Contains(target.gameObject))
                    {
                       targetCaught.Add(target.gameObject);
                    }
                    return true;

                }
                else
                {
                    return false;

                }
            }
        }
        shadowReset();
        return false;
    }
    public void shadowReset()
    {
        foreach (GameObject i in targetCaught)
        {
            if(i != null)
            {
                i.GetComponentInParent<BlendShadows>().scaleIncrease();
            }
            else
            {
              targetCaught.Remove(i);
            }
            
        }
    }
}
