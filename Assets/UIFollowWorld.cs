using UnityEngine;

public class UIFollowWorld : MonoBehaviour
{
    [Header("Tweaks")]
    [SerializeField] public Transform followFocus;
    [SerializeField] public Vector3 offset;
    
    [Header("Logic")]
    public Camera cam;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(followFocus.position + offset);
        if(transform.position != pos)
            transform.position = pos; 
    }
}
