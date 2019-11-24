using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private bool isAllowedToFade;
    private float opacity = 255;
    public float fadeSpeed;
    private Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAllowedToFade && img != null)
        {
            opacity -= fadeSpeed;
            if(opacity < 0)
            {
                isAllowedToFade = false;
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("cc");
                img.color = new Color32(255, 255, 255, (byte)opacity);
            }
        }
    }

    public void StartFade()
    {
        isAllowedToFade = true;
    }
}
