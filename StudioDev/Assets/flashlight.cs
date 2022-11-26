using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight : MonoBehaviour
{
    public float batteryLevel = 100;
    public float drainRate = 10;
    public bool onoroff = true;
    Renderer rend;

    Light light;

    FieldOfView fieldOfView;
    // Start is called before the first frame update
    void Awake()
    {
        rend = GetComponent<Renderer>();
        light = GetComponentInParent<Light>();
        fieldOfView = GetComponentInParent<FieldOfView>();
    }
    void Update()
    {
        batteryLevel -= Time.deltaTime * (drainRate);

        if (batteryLevel < 0)
        {
            batteryLevel = 0;
            onoroff = false;
            rend.material.SetFloat("_Opacity", 0);
            light.enabled = false;
            fieldOfView.viewAngle = 0;
        }
    }

    // Update is called once per frame
    public void ProcessLight()
    {
        if(onoroff)
        {
            onoroff = false;
            rend.material.SetFloat("_Opacity", 0);
            light.enabled = false;
            fieldOfView.viewAngle = 0;
            drainRate = 0;

        }
        else if(!onoroff && batteryLevel > 0)
        {
            onoroff = true;
            rend.material.SetFloat("_Opacity", 0.58f);
            light.enabled = true;
            fieldOfView.viewAngle = 25;
            drainRate = 10;


        }
        

    }
}
