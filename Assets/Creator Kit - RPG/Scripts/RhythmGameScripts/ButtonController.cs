using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer sr;
    public Sprite defaultImage;
    public Sprite pressedImage;
    public PressButton groundButton;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(groundButton.AskIsPressed()) {
            sr.sprite = pressedImage;
        }
        if(!groundButton.AskIsPressed()) {
            sr.sprite = defaultImage;
        }
    }
}
