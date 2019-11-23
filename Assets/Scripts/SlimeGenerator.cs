using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGenerator : MonoBehaviour
{
    [SerializeField] private float spawnCooldown = 3.0f;
    private float spawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnCooldown)
        {
            GenerateSlime();
        }
    }

    void GenerateSlime()
    {
        SlimeController slime = SlimeFactory.GetSlime(transform.position).gameObject.GetComponent<SlimeController>();
        slime.Goal = -transform.position;
        spawnTimer = 0.0f;
    }

    private void OnMouseDown()
    {
        GenerateSlime();
    }
}
