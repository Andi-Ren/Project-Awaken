
using UnityEngine;

public class DialogueMovementLock : MonoBehaviour
{
    DialogueParser DP;
    public string targetDialogue;
    public GameObject Character;

    // Update is called once per frame
     void Awake()
    {
        //RPGM.Gameplay.CharacterController2D movement = Character.GetComponent<RPGM.Gameplay.CharacterController2D>();
        DP = Camera.main.GetComponent<DialogueParser>();
    }   
    void Update()
    {
        checkDialogue(DP.dialogueText.text);
    }

    private void checkDialogue(string dialogue) {
        if (dialogue == targetDialogue)
        {
            Character.GetComponent<RPGM.Gameplay.CharacterController2D>().enabled = true;
        }
    }
}
