using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform player;
    public float moveStrength = 1.0f;
    public float stoppingStrength = 0.5f;

    private Material mat;
    private Rigidbody2D rb;

    [Range(-1.0f, 1.0f)]
    public float cutoffPoint = 1.0f;

    private void Awake() {
        mat = GetComponent<SpriteRenderer>().material;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector3 norTar = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 90);
        transform.rotation = rotation;

        var randomDir = Random.insideUnitCircle;
        rb.AddForce(randomDir * moveStrength * Time.fixedDeltaTime);
        rb.AddForce(-rb.velocity.normalized * stoppingStrength * Time.fixedDeltaTime);

        mat.SetFloat("_Cutoff_Height", cutoffPoint);

    }
}
