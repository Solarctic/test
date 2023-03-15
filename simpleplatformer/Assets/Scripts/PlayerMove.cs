using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Space]
    [Header("Player Movement")]
    public float walkspeed = 8;
    public float jumpStrength = 10;

    [Space]
    [Header("Kill Barrier")]
    public bool killBarrierEnabled = true;
    public float killBarrier = -5f;
    
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private Vector2 startPos = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Go back to the start position if the player passes the kill barrier
        if (killBarrierEnabled && transform.position.y <= killBarrier) {
            transform.position = startPos;
        }

        // If the player is grounded and jump is pressed
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            // Get the current velocity
            Vector2 currentVelocity = rb.velocity;

            // Set the y velocity to the jump strength
            currentVelocity.y = jumpStrength;
            rb.velocity = currentVelocity;

            // Make the player not grounded
            isGrounded = false;
        }
    }

    void FixedUpdate() 
    {
        // Get the current velocity
        Vector2 currentVelocity = rb.velocity;

        // Get the horizontal axis and apply the walkspeed to it
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        currentVelocity.x = horizontalAxis * walkspeed;

        // Set the velocity of the rigidbody to the calculated velocity
        rb.velocity = currentVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player is moving up, then they can't be grounded.  Exit the function.
        if (rb.velocity.y > 0f) return;

        // If the player's grounded check is touching an object tagged as a ground object, then the player is grounded.
        if (collision.transform.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnDrawGizmosSelected() {
        // If the kill barrier isn't enabled, exit the function
        if (!killBarrierEnabled) return;

        // Calculate the size and position of the kill barrier rect
        Vector3 Center = new Vector3(transform.position.x, killBarrier - 50, 10);
        Vector3 Size = new Vector3(300, 100, 1);

        // Display the kill barrier preview
        Gizmos.color = new Color(1, 0.2f, 0.2f, 0.3f);
        Gizmos.DrawCube(Center, Size);

        Gizmos.color = new Color(1, 0, 0, 1);
        Gizmos.DrawWireCube(Center, Size);
    }
}
