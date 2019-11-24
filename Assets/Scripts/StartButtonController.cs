using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonController : MonoBehaviour
{

    public AudioSource buttonSound;

    private void OnMouseOver()
    {
        Debug.Log("MouseOver");
        buttonSound.Play();
        buttonSound.Stop();
    }

    private void OnMouseExit()
    {
        Debug.Log("MouseExit");
        buttonSound.Play();
        buttonSound.Stop();
    }
}
