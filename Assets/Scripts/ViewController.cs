using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoC;

public class ViewController : MonoBehaviour
{
    Camera Cam;
    Vector2 CamPos;
    float ScrollPos;
    public Limit ZoomLimit;
    public float ZoomingSpeed;
    
    void Start()
    {
        Cam = gameObject.GetComponent<Camera>();
        ZoomLimit = new Limit(1,5);
        CamPos = transform.position;
        ScrollPos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var scroll_pos = Input.GetAxis("Mouse ScrollWheel"); // latest scroll position
        //if (scroll_pos != ScrollPos)
        //{
            ChangeZoomSize(scroll_pos);
        //}
            
    }

    void ChangeZoomSize(float scroll_pos)
    {
        ScrollPos = scroll_pos;
        float zoom_size = 0;
        zoom_size = -scroll_pos * ZoomingSpeed * Time.deltaTime;
        zoom_size += Cam.orthographicSize;
        Cam.orthographicSize = Mathf.Clamp(zoom_size, ZoomLimit.x, ZoomLimit.y);
    }
}
