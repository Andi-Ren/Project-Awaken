using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using RPGM.Gameplay;

public class DialogueParser : MonoBehaviour
{
    [SerializeField] private DialogueContainer dialogue;
    [SerializeField] public TextMeshProUGUI dialogueText;
    public GameObject dialogueTextGameObject;
    [SerializeField] private Button choicePrefab;
    [SerializeField] private Transform buttonContainer;
    private string exposedText;
    private List<NodeLinkData> exposedChoices;
    public float typeingSpeed;
    public GameObject doorsContainer;
    public GameObject fakeDoorsContainer;
    public Animator transition;
    public float transitionTime = 3f;
    public string iconTrigger = "Good, an icon for each color would provide more info and allow you to distinguish colors.";
    public string colorTextTrigger = "Good, labeling the doors would provide more info and allow you to distinguish colors.";
    public string biggerDanceFloorTrigger = "Great! *Snaps finger*";
    public string triggerWord = "Ready.";
    public static bool exploreMode = false;

    private void Start()
    {
        if (doorsContainer != null) {
            doorsContainer.SetActive(false);
        }
        var narrativeData = dialogue.NodeLinks.First(); //Entrypoint node
        ProceedToNarrative(narrativeData.TargetNodeGuid);
    }

    private void ProceedToNarrative(string narrativeDataGUID)
    {
        var text = dialogue.DialogueNodeData.Find(x => x.Guid == narrativeDataGUID).DialogueText;
        var choices = dialogue.NodeLinks.Where(x => x.BaseNodeGuid == narrativeDataGUID);
        exposedChoices = choices.ToList();
        dialogueText.text = "";

        // Gives a conversation effect.
        StartCoroutine(Type(text));
        exposedText = text;
        var buttons = buttonContainer.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            Destroy(buttons[i].gameObject);
        }

        foreach (var choice in choices)
        {
            var button = Instantiate(choicePrefab, buttonContainer);
            button.GetComponentInChildren<Text>().text = ProcessProperties(choice.PortName);
            button.onClick.AddListener(() => {
                exposedText = "  ";
                // Disable dialogue option container
                buttonContainer.gameObject.SetActive(false);
                
                if (choice.PortName == triggerWord && dialogueText.text.Contains("Tell me when you are ready.")) {
                    GameManager.instance.firstSong = true;
                }
                if (choice.PortName == triggerWord && dialogueText.text.Contains("Are you ready to dance?")) {
                    GameManager.instance.secondSong = true;
                }
                if (dialogueTextGameObject != null) {
                    if (choice.PortName == "Alright." && dialogueText.text.Contains("talk to them")) {
                        exploreMode = true;
                        dialogueTextGameObject.SetActive(false);
                    }
                }

                // Feed subsequent dialogue node (recursive)
                ProceedToNarrative(choice.TargetNodeGuid);
            });
        }

    }

    IEnumerator Type(string sentence) {
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeingSpeed);
        }
    }

    private string ProcessProperties(string text)
    {
        foreach (var exposedProperty in dialogue.ExposedProperties)
        {
            text = text.Replace($"[{exposedProperty.PropertyName}]", exposedProperty.PropertyValue);
        }
        return text;
    }

    // Check per frame to see if npc finish talking, if they do, enable player dialogue option
    void Update()
    {
        if (dialogueTextGameObject != null) {
            if (NPCController.globalConvCount >= 3) {
                exploreMode = false;
                dialogueTextGameObject.SetActive(true);
            }
        } 

        if ((dialogueText.text == exposedText && GameManager.instance == null) || (dialogueText.text == exposedText && !GameManager.instance.playing && !exploreMode)) {
            buttonContainer.gameObject.SetActive(true);

            // Disable teleport when there's remaining dialogues
            if (!exposedChoices.Any()) {
                if (doorsContainer != null) {
                    doorsContainer.SetActive(true);
                }
                if (fakeDoorsContainer != null) {
                    fakeDoorsContainer.SetActive(false);
                }
            }
        }

        if (dialogueText.text == iconTrigger) {
            // Call Scene Change to "RoomDoorScene - Symbol"
            StartCoroutine(LoadLevelByName("RoomDoorScene - Symbol"));
        } else if (dialogueText.text == colorTextTrigger) {
            // Call Scene Change to "RoomDoorScene - Tag"
            StartCoroutine(LoadLevelByName("RoomDoorScene - Tag"));
        } else if (dialogueText.text == biggerDanceFloorTrigger) {
            // Call Scene Change to "Challenge 2.2"
            StartCoroutine(LoadLevelByName("Challenge2.2"));
        }
    }

    IEnumerator LoadLevelByName(string levelName)
    {
        yield return new WaitForSeconds(3);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }
}