﻿using System.Collections;
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

    private void OnMouseDown()
    {
        if (ToGenerate > 0)
        {
            GenerateSlime();
        }
    }

    void Derelict()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
        Destroy(gameObject, 10.0f);
    }
}
