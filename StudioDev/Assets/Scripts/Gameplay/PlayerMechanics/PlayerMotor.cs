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

    public GameObject camHolder;
    public Camera camera;

    public Vector3 moveDirection;

    [Header("HeadBob Params")]
    [SerializeField] public float headBobSpeed = 14f;
    [SerializeField] public float walkBobAmount = 0.10f;
    private float defaultYPos = 0;
    private float timer;

    public AudioSource footsteps;
    private void Awake()
    {
        footsteps = GetComponent<AudioSource>();
        camHolder = GameObject.FindGameObjectWithTag("MainCamera");
        camera = camHolder.GetComponent<Camera>();
        defaultYPos = camera.transform.localPosition.y;
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        HandleBobbing();
    }
    // Receives the input from the InputManager.cs and apply them to our char controller
    public void ProcessMove(Vector2 input)
    {
        moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;   
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime); 
        if (isGrounded && playerVelocity.y < 0)
                playerVelocity.y = -2f;
        playerVelocity.y += gravity * Time.deltaTime; //Applying gravity to one Y axis 
        controller.Move(playerVelocity * Time.deltaTime); //Applying the gravity on our character

        if(moveDirection.x == 0 && moveDirection.z == 0)
        {
            footsteps.enabled = false;
        }
        else
        {
            footsteps.enabled = true;
        }
    }
    public void HandleBobbing()
    {
        if(Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (headBobSpeed);
            camera.transform.localPosition = new Vector3(camera.transform.localPosition.x, 
            defaultYPos + Mathf.Sin(timer) * (walkBobAmount), 
            camera.transform.localPosition.z);
        }
    }
}
