using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardAcceleration = 1.0f;   
    public float lateralAcceleration = 0.5f;
    public float turnSpeed = 10.0f;
    public float bodyTurnSpeed = 10.0f;
    
    public float maxVelocity = 10.0f;
    public float maxLateralStrength = 200;

    public float minRotTreshhold = 1.0f;
    public Transform body;

    private float horizontalInput = 0.0f;
    private float verticalInput = 0.0f;

    private float lateralForce = 0.0f;

    private Rigidbody2D rb;                          

    public float health = 100f;
    public float maxHealth = 100f;

    private GameManager gm;

    private Material mat;
    [Range(-1.0f, 1.0f)]
    public float cutoffPoint = 1.0f;

    public AudioSource engineAudio;
    public float engineVolume = 1.0f;

    private bool dead = false;
    void Awake() {
        rb = GetComponent<Rigidbody2D>();   
        health = maxHealth;

        gm = GameManager.instance;
        mat = GetComponentInChildren<SpriteRenderer>().material;
    }

    void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        lateralForce = Mathf.Clamp(horizontalInput * lateralAcceleration, -maxLateralStrength, maxLateralStrength);

        if(dead == false)
            engineAudio.volume = (rb.velocity.magnitude / maxVelocity) * engineVolume;
    }

    void FixedUpdate() {
        if (dead || gm.combatOn == false) return;

        Vector2 forwardForceVec = transform.up * verticalInput * forwardAcceleration * Time.fixedDeltaTime;
        float rotationStrength = lateralForce * Time.fixedDeltaTime;

        Debug.DrawRay(transform.position, forwardForceVec, Color.blue, 0, false);
        Debug.DrawRay(transform.position, transform.right * rotationStrength, Color.yellow, 0, false);

        Vector2 minRot = minRotTreshhold * transform.right * Mathf.Sign(rotationStrength);
        Debug.DrawRay(transform.position, minRot, Color.green, 0, false);

        // Body rotate
        float bodyAngle = 0;
        if (Mathf.Abs(rotationStrength) > minRotTreshhold) {
            bodyAngle = -90.0f * Mathf.Sign(rotationStrength);
        }
        Quaternion bodyTargetRotation = Quaternion.Euler(new Vector3(0, 0, bodyAngle));
        body.localRotation = Quaternion.Slerp(body.localRotation, bodyTargetRotation, bodyTurnSpeed * Time.fixedDeltaTime);

        rb.AddForce(forwardForceVec);
        var targetVelocity = Quaternion.Euler(0, 0, -rotationStrength) * rb.velocity;
        rb.velocity = Vector2.ClampMagnitude(targetVelocity, maxVelocity);

        if (Vector2.Dot(rb.velocity, transform.up) > 0) {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
        }
    }

    public void TakeDamage(float damage){
        health -= damage;

        if(health <= 0 && dead == false){
            dead = true;
            engineAudio.volume = 0;
            gm.TriggerGameOver();
            StartCoroutine(DeathRoutine());
        }
    }

    public IEnumerator DeathRoutine()
    {
        for (float alpha = 1f; alpha >= -0.5f; alpha -= 0.025f)
        {
            cutoffPoint = alpha;
            mat.SetFloat("_Cutoff_Height", cutoffPoint);
            yield return new WaitForSeconds(.05f);
        }
    }
}
