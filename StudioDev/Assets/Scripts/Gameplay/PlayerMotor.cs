using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f; 
    public float gravity = -9.8f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }
    // Receives the input from the InputManager.cs and apply them to our char controller
    public void ProcessMove(Vector2 input)
    {
      Vector3 moveDirection = Vector3.zero;
      moveDirection.x = input.x;
      moveDirection.z = input.y;   
      controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); 
      if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
      playerVelocity.y += gravity * Time.deltaTime; //Applying gravity to one Y axis 
      controller.Move(playerVelocity * Time.deltaTime); //Applying the gravity on our character
      Debug.Log(playerVelocity.y);
    }
}
