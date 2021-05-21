using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    private SpriteRenderer sr;
    public PressButton groundButton;

    public GameObject hitEffect;
    public GameObject missEffect;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (groundButton.AskIsPressed()) { // ground button is pressed
            if (canBePressed) {
                gameObject.SetActive(false); // arrow disappeared
                GameObject newHitEffect = Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                newHitEffect.transform.SetParent(GameObject.Find("World").transform);
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "DisplayArrow") {
            sr.enabled = true;
        }
        if (other.tag == "Activator") {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (gameObject.activeSelf) {
            if (other.tag == "Activator") {
                canBePressed = false;
                GameManager.instance.NoteMissed();
                GameObject newMissEffect = Instantiate(missEffect, transform.position, hitEffect.transform.rotation);
                newMissEffect.transform.SetParent(GameObject.Find("World").transform);
            }
            if (other.tag == "DisplayArrow") {
            sr.enabled = false;
            }
        }
    }
}
