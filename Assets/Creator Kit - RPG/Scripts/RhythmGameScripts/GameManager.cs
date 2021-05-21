using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPGM.Gameplay;
using EZCameraShake;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public HealthBar healthBar;
    public AudioClip ogSong;
    public AudioClip song;
    public bool startPlaying;
    public BeatScroller scroller;
    public GameObject dialogueText;
    public TextMeshProUGUI dialogueTextTMP;
    public GameObject controller;
    private AudioSource audioSource;
    public static GameManager instance;
    public Animator transition;
    public float transitionTime = 3f;
    public bool firstSong = false;
    public bool secondSong = false;
    public bool playing = false;
    private bool finishedSong = false;
    public int lives = 10;
    private Coroutine lastRoutine = null;

    [SerializeField] private Button choicePrefab;
    [SerializeField] private Transform buttonContainer;

    public float shakeIntensity = 2f;
    public float shakeFrequency = 2f;
    public float shakeFadein = 0.1f;

    private CameraShakeInstance shake;


    // Start is called before the first frame update
    void Start()
    {
        shake = CameraShaker.Instance.StartShake(shakeIntensity, shakeFrequency, shakeFadein);
        shake.StartFadeOut(0);
        shake.DeleteOnInactive = false;
        healthBar.SetMaxHealth(lives);

        instance = this;
        audioSource = controller.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (firstSong && !scroller.hasStarted) {
            lastRoutine = StartCoroutine(PlaySong(scroller));
        } 
        else if (secondSong && !scroller.hasStarted) {
            var buttons = buttonContainer.GetComponentsInChildren<Button>();
            for (int i = 0; i < buttons.Length; i++)
            {
                Destroy(buttons[i].gameObject);
            }
            lastRoutine = StartCoroutine(PlaySong(scroller));
        }
        if (finishedSong) {
            audioSource.clip = ogSong;
            audioSource.Play();
            audioSource.loop = true;
            if (firstSong) {  // Hmmm... how? If you see this, congradulation, you are a dance god!
                firstSong = false;
                dialogueText.SetActive(true);
                shake.StartFadeOut(0);
                scroller.hasStarted = false;
                scroller.gameObject.SetActive(false);
            } else if (secondSong) { // Successfully passed! Send them to the credit scene
                secondSong = false;
                dialogueTextTMP.text = "Congradulations! You have completed this challenge!";
                dialogueText.SetActive(true);
                shake.StartFadeOut(0);
                scroller.hasStarted = false;
                scroller.gameObject.SetActive(false);
                StartCoroutine(LoadLevelByName(8, "Credit"));
            }

        }
    }
    
    public void NoteMissed() {
        lives -= 1;
        healthBar.SetHealth(lives);
        if (lives <= 0) {
            StopCoroutine(lastRoutine);
            if (firstSong) {
                firstSong = false;
            }
            playing = false;
            shake.StartFadeOut(0);
            audioSource.Stop();
            scroller.hasStarted = false;
            scroller.gameObject.SetActive(false);
            audioSource.clip = ogSong;
            audioSource.Play();
            audioSource.loop = true;
            dialogueText.SetActive(true);
            if (secondSong) {
                // Let them try again.
                secondSong = false;

                lives = 10;

                // Change ghost text
                dialogueTextTMP.text = "Come on, let's try again!";

                StartCoroutine(LoadLevelByName(3 ,"Challenge2.2"));
            }
        }
    }

    IEnumerator LoadLevelByName(int secToWait, string levelName)
    {
        yield return new WaitForSeconds(secToWait);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }

    IEnumerator PlaySong(BeatScroller scroller) {
        playing = true;
        audioSource.Stop();
        dialogueText.SetActive(false);
        scroller.hasStarted = true;
        yield return new WaitForSeconds(0.97f);
        shake.StartFadeIn(0);

        audioSource.clip = song;
        audioSource.Play();
        audioSource.loop = false;
        yield return new WaitForSeconds(song.length);
        finishedSong = true;
    }
}
