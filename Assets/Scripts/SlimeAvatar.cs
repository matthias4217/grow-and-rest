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
    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
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
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void ResetState()
    {
        State = SlimeState.living;
        spriteRenderer.color = ownerColor;
        rb2d.constraints = RigidbodyConstraints2D.None;
    }

    private void OnMouseDown()
    {
        SlimeFactory.Release(this);
    }
}
