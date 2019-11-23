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
            SlimeController slime = SlimeFactory.GetSlime(transform.position).gameObject.GetComponent<SlimeController>();
            slime.Goal = -transform.position;
            // TODO : make the slime be the chosen one with a certain probability
            // the chosen one will have a particular random goal
            //slime.GetNewGoal();
            spawnTimer = 0.0f;
        }
    }
}
