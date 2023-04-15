using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;
    public float currentHealth;
    public GameObject playerSpaceship;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = playerSpaceship.GetComponent<PlayerController>().health;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth =  playerSpaceship.GetComponent<PlayerController>().health;
        fillImage.fillAmount = currentHealth/100;
    }
}
