using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{

    public float bpm;
    private float bps;
    public bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        bps = bpm / 60f; // tempo is in beats per minute, we need beats per second
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted) {
        } else {
            transform.position -= new Vector3(0f, bps * Time.deltaTime, 0f);
        }
    }
}
