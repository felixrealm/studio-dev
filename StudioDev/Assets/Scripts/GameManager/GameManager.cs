using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public flashlight flashlight;

    public int GhostsCaptured;

    public TMP_Text GhostCapturedText;

    // Start is called before the first frame update
    void Start()
    {
        
        flashlight = GameObject.FindGameObjectWithTag("Lantern").GetComponent<flashlight>();
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        GhostCapturedText.text = GhostsCaptured.ToString();
        if (flashlight.inversebatteryLevel > 100)
        {
            SceneManager.LoadScene("GameOver");
        } 
    }
}
