using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public string sceneName;
    public int conditionalCounter = 0;
    public string conditionalSceneName;
    public static int counter = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        counter += 1;
        yield return new WaitForSeconds(transitionTime);
        if (conditionalCounter != 0 && counter > conditionalCounter) {
            SceneManager.LoadScene(conditionalSceneName);
        } else {
            SceneManager.LoadScene(sceneName);
        }
    }
}
