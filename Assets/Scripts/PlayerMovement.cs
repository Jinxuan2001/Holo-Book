using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    public float jumpForce = 5f; // Force applied when jumping
    public bool isGrounded = true; // To check if the player is on the ground

    private Vector3 moveDirection; // Direction of movement
    private Rigidbody rb; // Reference to the Rigidbody component

    void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input from WASD or arrow keys
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrow keys for X axis
        float moveZ = Input.GetAxis("Vertical"); // W/S or Up/Down arrow keys for Z axis

        // Set the movement direction based on input
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // Move the player
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Jump when the SPACE key is pressed
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    // Function to handle jumping
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false; // Player is in the air after jumping
    }

    // Detect collision with the ground
    private void OnCollisionEnter(Collision collision)
    {
        // If the player collides with the ground, they can jump again
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
