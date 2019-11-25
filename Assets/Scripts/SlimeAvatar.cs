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
    [SerializeField] private float erodeChance;
    private Structure relStruct = null;
    public Structure RelStruct
    {
        get => relStruct;
        set => relStruct = value;
    }

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
        _controller = GetComponent<SlimeController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        ownerColor = spriteRenderer.color;
        DeathObserver.Subscribe(this);
    }

    public void Death()
    {
        State = SlimeState.Dead;
        spriteRenderer.color = Color.grey;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        rb2d.simulated = false;
        DeathObserver.Unsubscribe(this);
    }

    public void ResetState()
    {
        State = SlimeState.Living;
        //spriteRenderer.color = ownerColor;
        rb2d.constraints = RigidbodyConstraints2D.None;
    }

    public void Erode()
    {
        if (((transform.position - EarthAvatar.Instance.transform.position).magnitude + Random.Range(0.0f, 100.0f)) >= erodeChance)
        {
            SlimeFactory.Release(this);
        }
    }

    /*
    private void OnMouseDown()
    {
        SlimeFactory.Release(this);
    }
    */
}
