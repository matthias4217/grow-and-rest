using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonController : MonoBehaviour
{
    Animator anim;
    public AudioSource audio;
    public AudioSource startGameAudio;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HoverEnter()
    {
        anim.SetBool("hover", true);
        audio.Play();
    }

    public void HoverExit()
    {
        anim.SetBool("hover", false);
        audio.Play();
    }

    public void Click()
    {
        startGameAudio.Play();
        FindObjectOfType<ZoomController>().startZoom();
    }
}
