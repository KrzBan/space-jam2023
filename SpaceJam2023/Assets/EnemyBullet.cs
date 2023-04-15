using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyBullet : MonoBehaviour
{
    public float damage = 5;
 
    void Awake()
    {
        Destroy(gameObject, damage);
    }
 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent)){
            playerComponent.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}