using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    public string[] dialog;
    public float wordSpeed;

    public TextMeshProUGUI text;
    public bool windowActive = false;

    private int index;

    private bool blockInput = false;
    private GameManager gm;

    private void Awake()
    {
        gm = GameManager.instance;
    }

    private void Start()
    {
        NextLine();
    }
    void Update()
    {
        if(windowActive && blockInput == false && Input.GetMouseButtonDown(0))
        {
            NextLine();
        }
    }

    IEnumerator Typing(int id) {
        blockInput = true;
        foreach (char letter in dialog[id].ToCharArray()) {
            text.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
        blockInput = false;
    }

    public void NextLine() {

        if (index < dialog.Length) {
            text.text = "";
            StartCoroutine(Typing(index));

            index++;
        }
        else
        {
            TriggerEndDialog();
        }
    }

    public void TriggerEndDialog()
    {
        gm.TriggerCutsceneEnd();
    }

    


}
