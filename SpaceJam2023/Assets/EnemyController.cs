using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static event System.Action<EnemyController> OnEnemyKilled;
    public Transform player;
    public float moveStrength = 1.0f;
    public float stoppingStrength = 0.5f;
    public float health = 100f;
    public float maxHealth = 100f;

    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireTimer = 0f;
    public float bulletSpeed = 10;
    public float bulletDamage = 10;

    private Material mat;
    private Rigidbody2D rb;

    [Range(-1.0f, 1.0f)]
    public float cutoffPoint = 1.0f;

    private void Awake() {
        mat = GetComponent<SpriteRenderer>().material;
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
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

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            fireTimer = 0f;
            Shoot();
        }

    }

    private void Shoot()
    {
        Debug.Log("SHOOTING");

        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
    }

    public void TakeDamage(float damage){
        health -= damage;

        if(health <= 0){
            Destroy(gameObject);
            OnEnemyKilled?.Invoke(this);
        }
    }

}
