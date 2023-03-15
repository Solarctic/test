using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Space]
    public Transform followObject;
    public float speed = 3f;

    // Original zOffset
    private Vector3 zOffset;

    private void Start() {
        zOffset = new Vector3(0, 0, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        /* Experimentive Version
        // Get the difference between the follow object, and the camera position
        Vector2 offset = followObject.position - transform.position;

        // Multiply the difference by speed and time change
        Vector2 velocity = offset * speed * Time.deltaTime;

        // Make sure the movement isn't too high or too low
        if (velocity.magnitude > offset.magnitude || offset.magnitude < 0.05f)
            transform.position = followObject.position + zOffset;
        else
            transform.Translate(velocity);*/

        // Old Version
        // Get the current position of the object being followed
        Vector3 currentPosition = followObject.position;

        // Offset the z position to the proper amount
        currentPosition.z += -5;
        transform.position = currentPosition;
        
    }
}
