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
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        
    }
    public void reset()
    {
         DissolveAmount = Mathf.Lerp(-0.8f, 0.7f , Time.deltaTime); 
    }
}
