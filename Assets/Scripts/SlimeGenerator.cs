using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time % 5.0f <= Time.deltaTime)
        {
            SlimeFactory.GetSlime(new Vector2(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f)));
        }
    }
}
