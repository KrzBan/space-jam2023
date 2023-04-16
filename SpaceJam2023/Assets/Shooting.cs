using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Shooting : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public float bulletDamage = 10;

    private GameManager gm;
    public AudioSource fireAudio;

    void Awake() { 
        gm = GameManager.instance;
    }
    void Update()
    {
        if (gm.combatOn == false) return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            fireAudio.PlayOneShot(fireAudio.clip);
            
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
        }
    }
}