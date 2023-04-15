using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerSpaceship;

    public List<EnemyController> enemies = new List<EnemyController>();

    void Awake()
    {
        //enemies = GameObject.FindObjectsOfType<EnemyController>().ToList();
        enemies = new List<EnemyController>();
        enemies.Add(GameObject.FindObjectOfType<EnemyController>());
        instance = this;
    }

    void OnEnable()
    {
        EnemyController.OnEnemyKilled += HandleEnemyKilled;
    }

    void OnDisable()
    {
        EnemyController.OnEnemyKilled -= HandleEnemyKilled;
    }

    void HandleEnemyKilled(EnemyController enemy){
        // enemies.Remove(enemy);
    }

}
