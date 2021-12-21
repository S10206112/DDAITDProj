using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Ink JSON Text")]
    [SerializeField] private TextAsset[] inkText;

    [Header("Dialogue Trigger")]
    private bool playerInRange;
    public int textIndex = 0;

    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    private void Update()
    {
        //Visual Cue Display
        if (/*playerInRange &&*/!DialogueManager.GetInstance().dialogueActive)
        {
            //display visual cue
            visualCue.SetActive(true);

            //Trigger
            //TriggerDialogue();
        }
        else
        {
            visualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.transform.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider player)
    {
        if (player.transform.tag == "Player")
        {
            playerInRange = false;
        }
    }

    public void TriggerDialogue()
    {
        /*if (playerInRange && !DialogueManager.GetInstance().dialogueActive)
        {
            DialogueManager.GetInstance().EnterDialogue(inkText);
        }*/

        //Initiate Player's Current Dialogue Progress Tree
        DialogueManager.GetInstance().EnterDialogue(inkText[textIndex]);
        //textIndex++;
    }
}
