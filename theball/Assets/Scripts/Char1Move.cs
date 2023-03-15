using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char1Move : MonoBehaviour
{
    public Transform camTracker;
    public Rigidbody rb;
    float xRotation = 0; 
    public float verticalSensitivity = 200, horizontalSensitivity = 200;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float yDirection = Input.GetAxis("Vertical");
        float xDirection = Input.GetAxis("Horizontal");

        Vector3 velocity = yDirection * transform.forward + xDirection * transform.right;
        velocity.Normalize();

        rb.velocity = velocity * 3f;
        CharacterLook();
    }

    void CharacterLook()
    {
        Vector2 mouseMove = new Vector2(Input.GetAxis("Mouse X") * horizontalSensitivity,
                                        Input.GetAxis("Mouse Y") * verticalSensitivity);

        mouseMove *= Time.deltaTime;
        xRotation -= mouseMove.y;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        //camTracker.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.Rotate(Vector3.right * mouseMove.y);
        transform.Rotate(Vector3.up * mouseMove.x);
    }
}
