using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class PressButton : MonoBehaviour
{
    public Sprite notPressed;
    public Sprite pressed;
    private SpriteRenderer sr;
    private bool IsPressed;
    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = notPressed;
    }

    public bool AskIsPressed() {
        return IsPressed;
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        //Check to make sure the object that entered the trigger was the player.
        if (c.CompareTag("Player"))
            sr.sprite = pressed;
            IsPressed = true;
    }


    private void OnTriggerExit2D(Collider2D c)
    {
        //Check to make sure the object that exited the trigger was the player.
        if (c.CompareTag("Player"))
        {   
            sr.sprite = notPressed;
            IsPressed = false;
        }
            
    }
}
