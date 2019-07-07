using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoC;

public class ViewController : MonoBehaviour
{
    Vector3 CamPos;
    Vector3 StartTouch;
    public float ZoomSize;
    public Coord ZoomLimit;
    public float ZoomingSpeed;
    
    void Start()
    {
        ZoomLimit = new Coord(1,5);
       
        CamPos = transform.position;
        Camera.main.orthographicSize = ZoomSize;
    }

    // Update is called once per frame
    void Update()
    {
        var scroll_pos = Input.GetAxis("Mouse ScrollWheel"); // latest scroll position
        //if (scroll_pos != ScrollPos)
        //{
            ChangeZoomSize(scroll_pos);
            ChangeCamPos();
        //}
            
    }

    void ChangeZoomSize(float scroll_pos)
    {
        float zoom_size = 0;
        zoom_size = Camera.main.orthographicSize;
        zoom_size += -scroll_pos * ZoomingSpeed * Time.deltaTime;
        ZoomSize = Mathf.Clamp(zoom_size, ZoomLimit.x, ZoomLimit.y);
        Camera.main.orthographicSize = ZoomSize;
    }

    void ChangeCamPos(){
        //Vector2 cam_pos;
        if (Input.GetMouseButtonDown(0))
        {
            StartTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            var mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var displacement = StartTouch - mouse_pos;
            Camera.main.transform.position += displacement;
        }
}

   
