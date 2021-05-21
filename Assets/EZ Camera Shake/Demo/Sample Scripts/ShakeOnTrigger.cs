using UnityEngine;
using EZCameraShake;

/*
 * This script begins shaking the camera when the player enters the trigger, and stops shaking when the player leaves.
 */
public class ShakeOnTrigger : MonoBehaviour
{
    //Our saved shake instance.
    private CameraShakeInstance shake;
    void Start()
    {
        //We make a single shake instance that we will fade in and fade out when the player enters and leaves the trigger area.
        shake = CameraShaker.Instance.StartShake(2f,2f,0.1f);

        //Immediately make the shake inactive.  
        shake.StartFadeOut(0);

        //We don't want our shake to delete itself once it stops shaking.
        shake.DeleteOnInactive = false;
    }

    //When the player enters the trigger, begin shaking.
    private void OnTriggerEnter2D(Collider2D c)
    {
        //Check to make sure the object that entered the trigger was the player.
        if (c.CompareTag("Player"))
            shake.StartFadeIn(0);
    }

    //When the player exits the trigger, stop shaking.
    private void OnTriggerExit2D(Collider2D c)
    {
        //Check to make sure the object that exited the trigger was the player.
        if (c.CompareTag("Player"))
        {
            //Fade out the shake over 3 seconds.
            shake.StartFadeOut(0);
        }
            
    }
}
