using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerInput;
    private PlayerControls.OnFootActions onFoot;

    private PlayerMotor motor;

    private PlayerLook look;
    void Awake()
    {
        playerInput = new PlayerControls();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>(); 
        look = GetComponent<PlayerLook>();
        Cursor.visible = false;
    }

    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled. Good for physics since is constant.
    void FixedUpdate()
    {
       //tell the playermotor to move using the value from our movement action 
       motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    ///Late Update because we need to wait for the camera update movement to reach the levels before calling the input.
    void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable()
    {
        onFoot.Enable();
    }
    void OnDisable()
    {
        onFoot.Disable();
    }

}
