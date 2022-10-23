using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastVector : MonoBehaviour
{
    //Raycast Shyt
    public float maxDistance = 5f;
    [SerializeField] LayerMask shadows;

    private Collider colliderHit = null;

    private GameObject target = null;

    MeshRenderer meshRender;
    
    [SerializeField] float DissolveAmount = 0.01f;
    void Start()
    {

    }
    void FixedUpdate()
    {
        FrontProcessViewing();
        BackProcessViewing();
    }

    // Update is called once per frame
    public void FrontProcessViewing()
    {
        if(Physics.SphereCast(transform.position, transform.lossyScale.x * 2, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, maxDistance, shadows)){
            meshRender =  hitinfo.collider.transform.gameObject.GetComponent<MeshRenderer>();
            hitinfo.transform.GetComponent<BlendShadows>();
            hitinfo.transform.GetComponent<BlendShadows>().DissolveAmount += DissolveAmount;
            meshRender.material.SetFloat("_DissolveAmount", hitinfo.transform.GetComponent<BlendShadows>().DissolveAmount);
        }
        else{
            Debug.Log("Look at nothing");
        }
    
    }
    public void BackProcessViewing()
    {
        if(Physics.SphereCast(transform.position, transform.lossyScale.x * 2,transform.TransformDirection(Vector3.back), out RaycastHit hitinfo, maxDistance, shadows)){
            meshRender =  hitinfo.collider.transform.gameObject.GetComponent<MeshRenderer>();
            meshRender.material.SetFloat("_DissolveAmount", -0.82f);
            hitinfo.transform.GetComponent<BlendShadows>().reset();
        }
    }
}
