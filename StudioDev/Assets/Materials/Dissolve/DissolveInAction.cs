using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveInAction : MonoBehaviour
{
    public float DissolveAmount;

    MeshRenderer meshRender;

    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        DissolveAmount = Mathf.Lerp(-0.8f, 0.7f , Time.deltaTime);
        meshRender.material.SetFloat("_DissolveAmount", DissolveAmount);
    }
}
