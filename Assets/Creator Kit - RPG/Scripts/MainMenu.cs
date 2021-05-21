using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Debug.Log("Quitted");
        Application.Quit();
    }

    public void Intro() {
        SceneManager.LoadScene("introScene");
    }

    public void Challenge1() {
        SceneManager.LoadScene("RoomDoorScene");
    }

    public void Challenge2() {
        SceneManager.LoadScene("Challenge2.1");
    }
}
