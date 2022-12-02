using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerLook : MonoBehaviour
{
    public PlayerMotor playerMotor;

    public Vector3 moveDirection;
    public Camera camera;
    private float xRotation = 0f;

    private float xSensivity = 30f;
    private float ySensivity = 30f;

    

    private void Awake()
    {
        camera = gameObject.GetComponentInChildren<Camera>();
        playerMotor = GetComponent<PlayerMotor>();
        moveDirection = playerMotor.moveDirection;
    }
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime) * ySensivity;
        xRotation = Mathf.Clamp(xRotation, -22, 22);

        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);


        transform.Rotate(Vector3.up * ((mouseX * Time.deltaTime) * xSensivity));
    } 
    
}
