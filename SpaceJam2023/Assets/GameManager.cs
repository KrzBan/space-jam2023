using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerSpaceship;

    public List<EnemyController> enemies = new List<EnemyController>();

    public bool combatOn = false;

    public FrameAnimController frameAc;

    void Awake()
    {
        instance = this;

        enemies = new List<EnemyController>();
        enemies.AddRange(FindObjectsByType<EnemyController>(FindObjectsSortMode.None));

        Invoke("TriggerCutsceneStart", 3);
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
        enemies.Remove(enemy);
        if(enemies.Count == 0)
        {
            Invoke("TriggerSceneEnd", 4);
        }
    }

    [ContextMenu("Do Something")]
    public void TriggerSceneEnd()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; 

        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void TriggerCutsceneStart()
    {
        frameAc.TriggerCutsceneStart();
    }

    public void TriggerCutsceneEnd()
    {
        frameAc.TriggerCutsceneEnd();
    }


}
