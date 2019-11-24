using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    private Camera cam;

    private bool isAllowedToZoom;
    private int direction;

    [SerializeField] float targetCameraSize;
    [SerializeField] float speed;

    private float timeOnStartMove;

    public AudioSource introAudio;

    private bool triggerAudio;

    public Fade fader;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(isAllowedToZoom)
        {
            if(cam.orthographicSize < 2.5)
            {
                float x = Time.fixedTime - timeOnStartMove;
                float f = Mathf.Pow(x, 3) * Time.deltaTime * speed;
                cam.orthographicSize += f * direction;
            }
            else if(cam.orthographicSize > targetCameraSize)
            {
                cam.orthographicSize = targetCameraSize;
                isAllowedToZoom = false;
            }
            else
            {
                float x = Time.fixedTime - timeOnStartMove + 0.001f;
                float f = 1 / x * Time.deltaTime * 10;
                cam.orthographicSize += f * direction;
            }
        }
    }

    void resetTime()
    {
        timeOnStartMove = Time.fixedTime;
    }

    public void startZoom()
    {
        fader.StartFade();
        isAllowedToZoom = true;
        resetTime();
        introAudio.Play();
    }

}

