using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class BallControl : MonoBehaviour
{
    [SerializeField] private float chargeTime = 1.5f;
    [SerializeField] private float jumpStrength = 10f;
    [SerializeField] private AnimationCurve jumpStrengthCurve;
    [SerializeField] private Material ballMaterial;

    private float jumpTimer = 0f;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(jumpTimer);
        if (Input.GetKey(KeyCode.Space)) {
            jumpTimer += Time.deltaTime / chargeTime;
            jumpTimer = Mathf.Clamp01(jumpTimer);

            if (jumpTimer > 0.9f) ballMaterial.SetFloat("_IsBlinking", 1f);

            transform.localScale = new Vector3(1f, Mathf.Lerp(0.3f, 1f, jumpStrengthCurve.Evaluate(1 - jumpTimer)));

        } else {

            transform.localScale = new Vector3(1f, Mathf.Lerp(1f, 3f, Mathf.InverseLerp(0f, 15f, Mathf.Abs(rigidbody.velocity.y))));

            if (Input.GetKeyUp(KeyCode.Space)) {
                float finalJumpPower = jumpStrengthCurve.Evaluate(jumpTimer) * jumpStrength;
                finalJumpPower = Mathf.Max(2f, finalJumpPower);

                rigidbody.AddForce(Vector3.up * finalJumpPower, ForceMode.Impulse);
                jumpTimer = 0f;
                ballMaterial.SetFloat("_IsBlinking", 0f);
            }
        }

        
    }
}
