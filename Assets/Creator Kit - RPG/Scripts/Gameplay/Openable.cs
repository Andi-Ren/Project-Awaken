
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Openable : Interactable
{
    public Sprite open;
    public Sprite closed;
    private CircleCollider2D cld;
    private SpriteRenderer sr;
    private bool isOpen;
    
    public override void Interact() 
    {
        if(isOpen)
            sr.sprite = closed;
        else
            sr.sprite = open;
        isOpen = !isOpen;
        cld.enabled = !cld.enabled;
    }

    private void Start() {

        cld = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = closed;
    }
}
