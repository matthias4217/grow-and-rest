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
    public Color ownerColor;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<SlimeController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ownerColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.IsGoalReached())
        {
            state = SlimeState.Dead;
        }
    }

    public void Death()
    {
        State = SlimeState.Dead;
        spriteRenderer.color = Color.grey;
    }

    public void ResetState()
    {
        State = SlimeState.Living;
        spriteRenderer.color = ownerColor;
    }

    private void OnMouseDown()
    {
        SlimeFactory.Release(this);
    }
}
