using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShadows : MonoBehaviour
{
    const float maxDst = 10f;
    
    [SerializeField] LayerMask shadows;

    public float DissolveAmount;

    void Start()
    {
        DissolveAmount = Mathf.Lerp(-0.8f, 0.7f , Time.deltaTime); 
    }
    void FixedUpdate()
    {
        RayCastlength();
    }

    // Update is called once per frame
    void RayCastlength()
    {
       if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hitinfo,maxDst, shadows)){
            smin(transform.position.z, hitinfo.transform.position.z, 1.00f);
       }
    }
    float smin(float dstA, float dstB, float strength)
    {
        float h = Mathf.Max(strength - Mathf.Abs(dstB-dstA), 0)/ strength;
        return (Mathf.Min(dstA, dstB) - strength*strength*strength*1/6.0f);
    }
    public void reset(){
         DissolveAmount = Mathf.Lerp(-0.8f, 0.7f , Time.deltaTime); 
    }
}
