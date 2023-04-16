using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimController : MonoBehaviour
{

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerGameOver()
    {
        anim.SetTrigger("GameOver");
    }
}
