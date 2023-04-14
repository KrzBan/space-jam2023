using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardAcceleration = 1.0f;   
    public float lateralAcceleration = 0.5f;
    public float turnSpeed = 10.0f;

    public float maxVelocity = 10.0f;
    public float maxLateralStrength = 200;

    private float horizontalInput = 0.0f;
    private float verticalInput = 0.0f;

    private float lateralForce = 0.0f;

    private Rigidbody2D rb;                          

    void Start() {
        rb = GetComponent<Rigidbody2D>();   
    }

    void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        lateralForce = Mathf.Clamp(lateralForce + horizontalInput, -maxLateralStrength, maxLateralStrength);
    }

    void FixedUpdate() {
        Vector2 forwardForceVec = transform.up * verticalInput * forwardAcceleration * Time.fixedDeltaTime;
        float rotationStrength = lateralForce * lateralAcceleration * Time.fixedDeltaTime;


        Debug.DrawRay(transform.position, forwardForceVec, Color.blue, 0, false);
        Debug.DrawRay(transform.position, transform.right * rotationStrength, Color.yellow, 0, false);

        rb.AddForce(forwardForceVec);
        rb.velocity = Quaternion.Euler(0, 0, -rotationStrength) * rb.velocity;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);

        if (Vector2.Dot(rb.velocity, transform.up) > 0) {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
        }
        
    }
}
