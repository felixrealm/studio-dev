using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeleteTest : MonoBehaviour
{
    private RaycastVector tester = null;
    void Start()
    {
        tester = GameObject.FindObjectOfType<RaycastVector>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       /* if(tester.ViewRest(this.transform.position) == false){
            gameObject.GetComponent<MeshCollider>().enabled = true;
        }
        else{
            return; */
        }
    }
