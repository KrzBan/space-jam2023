using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyBullet : MonoBehaviour
{
    public float damage = 5;
    public ParticleSystem sparks;

    void Awake()
    {
        Destroy(gameObject, damage);
    }
 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent)){
            playerComponent.TakeDamage(damage);

            var contactPoint = collision.GetContact(0);
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
            var newSparks = Instantiate(sparks, contactPoint.point, rot) as ParticleSystem;
            newSparks.Play();

            Destroy(gameObject);
        }
    }
}