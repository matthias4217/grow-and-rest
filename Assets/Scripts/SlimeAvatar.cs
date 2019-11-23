using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeState
{
    living,
    dead
}

public class SlimeAvatar : MonoBehaviour
{
    [SerializeField] private SlimeState state;
    public SlimeState State
    {
        get { return state; }
        set { state = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        SlimeFactory.Release(this);
    }
}
