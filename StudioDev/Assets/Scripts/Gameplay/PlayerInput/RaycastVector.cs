using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastVector : MonoBehaviour
{
    public float maxDistance = 5f;
    [SerializeField] LayerMask shadows;

    private Collider colliderHit = null;

    public float cutoff = 60f;

    private GameObject target = null;
    // Start is called before the first frame update
    void FixedUpdate()
    {
        FrontProcessViewing();
        BackProcessViewing();
    }

    // Update is called once per frame
    public void FrontProcessViewing()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, maxDistance, shadows)){
            hitinfo.collider.transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else{
            Debug.Log("Look at nothing");
        }
    
    }
    public void BackProcessViewing()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out RaycastHit hitinfo, maxDistance, shadows)){
            hitinfo.collider.transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
