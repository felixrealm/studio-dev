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

    void FindVisibleTargets(){
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
                    meshRender = targetsInViewRadius[i].gameObject.GetComponent<MeshRenderer>();
                    targetsInViewRadius[i].transform.GetComponent<BlendShadows>();
                    targetsInViewRadius[i].transform.GetComponent<BlendShadows>().DissolveAmount += DissolveAmount;
                    meshRender.material.SetFloat("_DissolveAmount", targetsInViewRadius[i].transform.GetComponent<BlendShadows>().DissolveAmount);
                }
            }
        }
    }
    

    public Vector3 DirFromAngle(float angleCutOff, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleCutOff += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleCutOff * Mathf.Deg2Rad), 0, Mathf.Cos(angleCutOff * Mathf.Deg2Rad));
    }
}
