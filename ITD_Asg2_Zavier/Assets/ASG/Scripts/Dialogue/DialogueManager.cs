using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    public GameObject closeButton;

    private Story currentStory;
    public bool dialogueActive; //{ get; private set; }

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

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            choice.SetActive(false);
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueActive)
        {
            return;
        }
    }

    public void endDialogueBtn()
    {
        //When close button is clicked, end dialogue
        if (currentStory.currentChoices.Count == 0)
        {
            ContinueStory();
        }
    }

    public void EnterDialogue(TextAsset inkJSON)
    {
        if (!dialogueActive)
        {
            dialogueActive = true;
            dialoguePanel.SetActive(true);
            currentStory = new Story(inkJSON.text);

            ContinueStory();
        }
    }

    private IEnumerator ExitDialogue()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueActive = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        //closeButton.SetActive(false);
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            //display choices, if any
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogue());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        int index = 0;
        //display choices
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        //hide unused choices
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }
}
