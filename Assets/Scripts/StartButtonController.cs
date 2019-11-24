using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonController : MonoBehaviour
{

    public AudioSource buttonSound;
    public Canvas ui;

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

    public void MyJobIsDone()
    {
        //ui.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
