using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomController : MonoBehaviour
{
    private Camera cam;

    private bool isAllowedToZoom;
    private int direction;
    public int Direction { get => direction; set => direction = value; }

    [SerializeField] float targetCameraSize;
    [SerializeField] float minCameraSize;
    [SerializeField] float speed;

    private float timeOnStartMove;

    public AudioSource introAudio;

    private bool triggerAudio;

    public Fade fader;
    private bool zooming;

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
            if (!zooming)
            {
                if (cam.orthographicSize < 2.5)
                {
                    float x = Time.fixedTime - timeOnStartMove;
                    float f = Mathf.Pow(x, 3) * Time.deltaTime * speed;
                    cam.orthographicSize += f * Direction;
                }
                else if (cam.orthographicSize > targetCameraSize)
                {
                    cam.orthographicSize = targetCameraSize;
                    isAllowedToZoom = false;
                }
                else
                {
                    float x = Time.fixedTime - timeOnStartMove + 0.001f;
                    float f = 1 / x * Time.deltaTime * 10;
                    cam.orthographicSize += f * Direction;
                }
            }
            else
            {
                if (cam.orthographicSize > 3.5)
                {
                    float x = Time.fixedTime - timeOnStartMove;
                    float f = Mathf.Pow(x, 3) * Time.deltaTime * speed * 2.0f;
                    cam.orthographicSize += f * Direction;
                }
                else if (cam.orthographicSize < minCameraSize)
                {
                    cam.orthographicSize = minCameraSize;
                    isAllowedToZoom = false;
                }
                else
                {
                    float x = Time.fixedTime - timeOnStartMove + 0.001f;
                    float f = 1 / x * Time.deltaTime * 15.0f;
                    cam.orthographicSize += f * Direction;
                }
            }
        }
    }

    void resetTime()
    {
        timeOnStartMove = Time.fixedTime;
    }

    public void startZoom()
    {
        zooming = (Direction < 0);
        fader.StartFade();
        isAllowedToZoom = true;
        resetTime();
        introAudio.Play();
    }

}

