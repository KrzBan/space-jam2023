using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameAnimController : MonoBehaviour
{
    private GameManager gm;
    private TextManager tm;
    private Animator animator;
    private AudioSource audio;

    void Awake() {
        gm = GameManager.instance;
        tm = gm.GetComponent<TextManager>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void TriggerCutsceneStart()
    {
        animator.SetTrigger("StartCutScene");
    }
    public void TriggerCutsceneEnd()
    {
        animator.SetTrigger("EndCutScene");

    }

    public void TurnOffCombat() {
        gm.combatOn = false;
    }
    public void TurnOnCombat() {
        gm.combatOn = true;
    }

    public void ActivateWindow()
    {
        tm.windowActive = true;
    }
    public void DisableWindow()
    {
        tm.windowActive = false;
    }

    public void PlayNotification()
    {
        audio.Play();
    }
}
