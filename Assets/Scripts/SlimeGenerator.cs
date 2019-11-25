using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{
    [SerializeField] private float spawnCooldown = 3.0f;
    private float spawnTimer;
    public int ToGenerate;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0.0f;
        DeathObserver.Subscribe(this);
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnCooldown && ToGenerate > 0)
        {
            GenerateSlime();
            spawnTimer = 0f;
        }
    }

    void GenerateSlime()
    {
        SlimeController slime = SlimeFactory.GetSlime(transform.position).gameObject.GetComponent<SlimeController>();
        //slime.Goal = -transform.position;
        slime.GetNewGoal();
        //spawnTimer = 0.0f;
        ToGenerate--;
        if (ToGenerate == 0)
        {
            Derelict();
        }
    }

    // TODO : uncomment and fix bug
    private void OnMouseDown()
    {
        if (ToGenerate > 0)
        {
            TreeGenObserver.Notify();
            GenerateSlime();
        }
    }

    void Derelict()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
        DeathObserver.Unsubscribe(this);
        Destroy(gameObject, 10.0f);
    }
}
