using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiallogCollider : MonoBehaviour
{
    public GameObject dialogPanel;
    public GameObject contButton;
    public Text dialogText;
    public string[] dialog;
    private int index;

    public float wordSpeed;
    public bool isPlayerClose;

    void Update()
    {
        //Debug.Log(isPlayerClose);
       if(Input.GetKeyDown(KeyCode.E) && isPlayerClose) {
        if(dialogPanel.activeInHierarchy){
            resetText();
        }else {
            dialogPanel.SetActive(true);
            StartCoroutine(Typing());
        }
       }

       if(dialogText.text == dialog[index]){
        contButton.SetActive(true);
       }
    }

    IEnumerator Typing(){
        foreach(char letter in dialog[index].ToCharArray()){
            dialogText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine(){
        contButton.SetActive(false);
        if(index < dialog.Length - 1){
            index++;
            dialogText.text = "";
            StartCoroutine(Typing());
        } else {
            resetText();
        }
    }

    public void resetText()
    {
        dialogText.text = "";
        index = 0;
        dialogPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("TIGGERENTER");
        if(other.CompareTag("Player")){
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("TIGGERLEAVBE");
        if(other.CompareTag("Player")){
            isPlayerClose = false;
            resetText();
        }
    }

}
