using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private Story currentStory;
    public bool dialogueActive { get; private set; }

    //Dialogue Manager Singleton
    private static DialogueManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than 1 Dialogue Manager in Scene");
        }

        instance = this;
    }
    public static DialogueManager GetInstance()
    {
        return instance;
    }

    //Dialogue Manager Functions
    private void Start()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false);
    }

    public void EnterDialogue(TextAsset inkJSON)
    {
        dialogueActive = true;
        dialoguePanel.SetActive(true);
        currentStory = new Story(inkJSON.text);

        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        dialogueActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
}
