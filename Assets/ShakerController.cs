using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class ShakerController : MonoBehaviour
{
    // Start is called before the first frame update
    private CameraShakeInstance shake;
    //public GameObject shakeTrigger;
    void Start()
    {
        shake = CameraShaker.Instance.StartShake(1f,1f,0.1f);
        //shake.StartFadeOut(0);
        //shake.DeleteOnInactive = true;
    }

    /*void OnTriggerEnter(Collider c) {
        if (c.CompareTag("ShakeTrigger"))
            shake.StartFadeIn(1);
            Debug.Log("AAA");
    }

    void OnTriggerExit(Collider c) {
        if (c.CompareTag("ShakeTrigger"))
        {
            //Fade out the shake over 3 seconds.
            shake.StartFadeOut(3f);        
        }
    }*/
}
