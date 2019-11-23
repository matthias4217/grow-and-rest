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
            SlimeController slime = SlimeFactory.GetSlime(transform.position).gameObject.GetComponent<SlimeController>();
            slime.Goal = -transform.position;
        }
    }
}
