using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSquare : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite defaultImage;
    public Sprite pressedImage;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        sr.sprite = pressedImage;
    }

    private void OnCollisionExit2D(Collision2D other) {
        sr.sprite = defaultImage;
    }
}
