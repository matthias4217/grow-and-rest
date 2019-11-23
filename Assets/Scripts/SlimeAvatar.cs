using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlimeState
{
    Living,
    Dead
}

public class SlimeAvatar : MonoBehaviour
{
    [SerializeField] private SlimeState state;
    [SerializeField] private bool isChosen;
    private SlimeController _controller;
    public SlimeState State
    {
        get { return state; }
        set { state = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<SlimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.IsGoalReached())
        {
            state = SlimeState.Dead;
        }
    }

    private void OnMouseDown()
    {
        SlimeFactory.Release(this);
    }
}
