using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Bed : Interactable
{
    public Animator transition;
    public float transitionTime = 3f;
    
    public override void Interact() 
    {
        StartCoroutine(LoadLevelByName("RoomDoorScene"));
    }

    IEnumerator LoadLevelByName(string levelName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }

}
