using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class flashlight : MonoBehaviour
{
    public float inversebatteryLevel = 0;
    public float batteryLevel = 100;
    public float drainRate = 2;
    public bool onoroff = true;
    Renderer rend;

    Light light;

    FieldOfView fieldOfView;

    public AudioSource click;

    public Volume cam;

    public Vignette vignite;

    public ClampedFloatParameter vignitteIndex;
    // Start is called before the first frame update
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Volume>();
        rend = GetComponent<Renderer>();
        light = GetComponentInParent<Light>();
        fieldOfView = GetComponentInParent<FieldOfView>();
        Vignette tempVig;
        if(cam.profile.TryGet<Vignette>(out tempVig))
        {
            vignite = tempVig;
        }
    }
    void Update()
    {  
        batteryLevel -= Time.deltaTime * (drainRate); 
        vignite.intensity.Override(inversebatteryLevel/100);

        if (batteryLevel < 0)
        {
            batteryLevel = 0;
            onoroff = false;
            rend.material.SetFloat("_Opacity", 0);
            light.enabled = false;
            fieldOfView.viewAngle = 0;
            inversebatteryLevel += Time.deltaTime * (drainRate);
        }
    }

    // Update is called once per frame
    public void ProcessLight()
    {
        if(onoroff)
        {
            click.Play();
            onoroff = false;
            rend.material.SetFloat("_Opacity", 0);
            light.enabled = false;
            fieldOfView.viewAngle = 0;
            drainRate = 0;

        }
        else if(!onoroff && batteryLevel > 0)
        {
            click.Play();
            onoroff = true;
            rend.material.SetFloat("_Opacity", 0.58f);
            light.enabled = true;
            fieldOfView.viewAngle = 25;
            drainRate = 2;



        }
        

    }
}
