
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractableNPC : Interactable
{
    private bool isTalking;
    
    public override void Interact() 
    {
        isTalking = !isTalking;
    }

    private void Start() {
        isTalking = false;
    }
}
