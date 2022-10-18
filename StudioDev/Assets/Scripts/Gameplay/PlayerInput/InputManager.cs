using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerControls playerInput;
    private PlayerControls.OnFootActions onFoot;

    private PlayerLook playerlook;

    private PlayerMotor motor;
    void Awake()
    {
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
