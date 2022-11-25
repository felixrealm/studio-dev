using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShadows : MonoBehaviour
{
    private FieldOfView DissolveCheck; 

    private float DissolveAmount = -1.17f;

    private float DissolveSpeed = 0.005f;
    public Vector3 scaleChange;
    public GameObject player;
    Renderer rend;
    void Update()
    {
        if(DissolveAmount > 0.60f)
        {
            Destroy(gameObject);
        }
    }
    void Awake()
    {
        rend = gameObject.GetComponent<Renderer> ();

        scaleChange = new Vector3(0.0001f, 0.0001f, 0.0001f);

    }
    public void reset()
    {
        if(DissolveAmount < 0.67f)
        {
            DissolveAmount += DissolveSpeed;
        }
        rend.material.SetFloat("_DissolveAmount", DissolveAmount);
    }
    public void scaleIncrease()
    {
        gameObject.transform.localScale += scaleChange;
    }
}
