using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Bullet : MonoBehaviour
{
    public float damage = 50;
 
    void Awake()
    {
        Destroy(gameObject, damage);
    }
 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent<EnemyController>(out EnemyController enemyComponent)){
            enemyComponent.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}