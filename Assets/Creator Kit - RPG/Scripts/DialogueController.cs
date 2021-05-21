
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    DialogueParser DP;
    Wilberforce.Colorblind colorblindScript;
    public string targetDialogue;

    public GameObject toggleObject;

    public int colorblindSwitch;

    void Awake()
    {
        if (toggleObject != null) {
            toggleObject.SetActive(false);
        }
        colorblindScript = Camera.main.GetComponent<Wilberforce.Colorblind>();
        DP = Camera.main.GetComponent<DialogueParser>();
    }

    // Update is called once per frame
    void Update()
    {
        checkDialogue(DP.dialogueText.text);
    }

    private void checkDialogue(string dialogue) {
        if (dialogue == targetDialogue)
        {
            colorblindScript.Type = colorblindSwitch;
            if (toggleObject != null){
                toggleObject.SetActive(true);
            }
            
        }
    }
}
