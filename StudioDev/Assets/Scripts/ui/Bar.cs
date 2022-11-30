using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public flashlight flashlightscript;
    private Image barImage;
    // Start is called before the first frame update
    private void Awake()
    {
        flashlightscript =  GameObject.FindWithTag("Lantern").GetComponent<flashlight>();

        barImage = gameObject.GetComponent<Image>();

        barImage.fillAmount = flashlightscript.batteryLevel/100;
    }

    // Update is called once per frame
    void Update()
    {
        barImage.fillAmount = flashlightscript.batteryLevel/100;
    }
}
