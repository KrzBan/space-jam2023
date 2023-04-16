using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerSpaceship;

    public List<EnemyController> enemies = new List<EnemyController>();

    public bool combatOn = false;

    private FrameAnimController frameAc;
    private GameOverAnimController gameOverAc;
    private CameraAnimController cameraAc;

    void Awake()
    {
        instance = this;

        enemies = new List<EnemyController>();
        enemies.AddRange(FindObjectsByType<EnemyController>(FindObjectsSortMode.None));


        frameAc = FindAnyObjectByType<FrameAnimController>();
        gameOverAc = FindAnyObjectByType<GameOverAnimController>();
        cameraAc = FindAnyObjectByType<CameraAnimController>();

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

    private void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void TriggerCutsceneStart()
    {
        frameAc.TriggerCutsceneStart();
    }

    public void TriggerCutsceneEnd()
    {
        frameAc.TriggerCutsceneEnd();
    }

    public void TriggerGameOver()
    {
        gameOverAc.TriggerGameOver();
        cameraAc.TriggerCameraZoom();
        Invoke("RestartScene", 8);
    }

}
