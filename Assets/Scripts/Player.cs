using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 10.0f;
    public float sprintSpeed = 20.0f;
    public float rotationSpeed = 30.0f;
    public float jumpSpeed = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private bool canSprint = false;
    private bool canJump = false;
    private float speed , rotX , rotY;

    // component references
    private CharacterController characterController;
    private Animator animator;
    private HealthSystem healthSystem;
    private HungerSystem hungerSystem;
    private Backpack backpack;
    public  UIManager UIManager;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();
        hungerSystem = GetComponent<HungerSystem>();
        backpack = GetComponent<Backpack>();
    }

    void Update()
    {
        speed = walkSpeed;
        canSprint = false;
        canJump = false;

        if(characterController.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpSpeed;
                canJump = true;
            
        }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
            canSprint = true;
        }

        transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * 
            rotationSpeed * Time.deltaTime, 0));
        moveDirection.y -= 9.8f * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);

        var magnitude = new Vector2(characterController.velocity.x, characterController.velocity.z).magnitude;
        animator.SetFloat("speed", magnitude);
        animator.SetBool("canSprint", canSprint);
        animator.SetBool("canJump", canJump);

        rotX -= Input.GetAxis("Mouse Y") * Time.deltaTime * rotationSpeed;
        rotY += Input.GetAxis("Mouse X") * Time.deltaTime * rotationSpeed;

        if (rotX < -90)
        {
            rotX = -90;
        }
        else if (rotX > 90)
        {
            rotX = 90;
        }

        transform.rotation = Quaternion.Euler(0, rotY, 0);
        GameObject.FindWithTag("MainCamera").transform.rotation = Quaternion.Euler(rotX, rotY, 0);

    

        if(Input.GetKeyDown(KeyCode.B))
        {
            UIManager.ToggleInventory();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Obstacle"))
        {
            var obstacle = hit.gameObject.GetComponent<Obstacle>();
            if(obstacle)
            {
                healthSystem.DecreaseHealth(obstacle.health);
            }
        }

        if(hit.gameObject.CompareTag("Item"))
        {
            if(backpack.AddItem(hit.gameObject))
            {
                Destroy(hit.gameObject);
            }
        }

        /*if(hit.gameObject.CompareTag("Food"))
        {
            var food = hit.gameObject.GetComponent<Food>();
            if(food)
            {
                Destroy(hit.gameObject);
                healthSystem.IncreaseHealth(food.health);
                hungerSystem.DecreaseHungerLevel(food.hunger);
            }
        }*/
    }
}
