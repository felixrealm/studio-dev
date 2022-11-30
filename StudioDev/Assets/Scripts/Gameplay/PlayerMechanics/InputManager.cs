using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls Flashlightinput;
    private PlayerControls.FlashlightActions Flashlight;
    private PlayerControls playerInput;
    public PlayerControls.OnFootActions onFoot;

    private PlayerLook playerlook;

    private flashlight flashlight;

    private bool fire;

    private PlayerMotor motor;
    void Awake()
    {
        flashlight = GetComponentInChildren<flashlight>();
        Flashlightinput = new PlayerControls();
        Flashlight = Flashlightinput.Flashlight;

        playerlook = GetComponent<PlayerLook>();
        playerInput = new PlayerControls();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>(); 

    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
       //tell the playermotor to move using the value from our movement action 
       motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    void LateUpdate()
    {
        playerlook.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        Flashlight.OnOrOff.performed += ctx => flashlight.ProcessLight();
    }
    private void OnEnable()
    {
        onFoot.Enable();
        Flashlight.Enable();
    }
    void OnDisable()
    {
        onFoot.Disable();
        Flashlight.Disable();
    }


}
