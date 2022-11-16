using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendShadows : MonoBehaviour
{
    private FieldOfView DissolveCheck; 

    public float DissolveAmount = -1.17f;
    public GameObject player;
    Renderer rend;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        DissolveCheck = player.GetComponentInChildren<FieldOfView>();
        rend = GetComponent<Renderer> ();

    }
    void FixedUpdate()
    {
        if(DissolveCheck.FindVisibleTargets())
        {
            if(DissolveAmount < 0.67f)
            {
                DissolveAmount += 0.1f;
            }
            Debug.Log($"Player seeing {this.gameObject}");
            rend.material.SetFloat("_DissolveAmount", DissolveAmount);
        }
        else
        {
            if(DissolveAmount > -1.17f)
            {
                if(DissolveAmount != 0.67f)
                {
                    DissolveAmount -= 0.1f;
                }
                
            }
            rend.material.SetFloat("_DissolveAmount", DissolveAmount);
        }
    }
    public void reset()
    {
         DissolveAmount = Mathf.Lerp(-0.8f, 0.7f , Time.deltaTime); 
    }
}
