using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float horizontal, vertical; 
    float speed = 15f;
    float gravity = -19.42f;
    float groundDistance = 0.4f;
    float jumpHeight = 3f;

    Vector3 velocity;

    bool isGrounded;
    bool doubleJumped = false;

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask; 

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            doubleJumped = false; 
            velocity.y =  -3f; 
        }

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (Input.GetButtonDown("Jump") && !isGrounded)
        {
            if (!doubleJumped)
            {
                doubleJumped = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else if (Input.GetButtonUp("Fire3"))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
