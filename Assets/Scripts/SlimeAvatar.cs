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
    public Color ownerColor;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        ownerColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        State = SlimeState.dead;
        spriteRenderer.color = Color.grey;
    }

    public void ResetState()
    {
        State = SlimeState.living;
        spriteRenderer.color = ownerColor;
    }

    private void OnMouseDown()
    {
        SlimeFactory.Release(this);
    }
}
