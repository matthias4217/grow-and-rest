using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{

    [SerializeField] private float speed = 1;
    [SerializeField] private Vector3 goal = Vector3.zero;
    [SerializeField] private float gravity = 0.5f;
    [SerializeField] private float goalThreshold = 1;
    private Vector3 currentSpeed =  Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        MoveTowardsGoal();
        IsGoalReached();
    }

    public bool IsGoalReached()
    {
        // TODO : if the cube is less than goalThreshold from the goal, we teleport it and return true
        if ((transform.position - goal).magnitude < goalThreshold)
        {
            // TODO : move the color change to the Avatar
            GetComponent<SpriteRenderer>().color = Color.grey;
            return true;
        }

        return false;
        //throw new NotImplementedException();
    }

    private void MoveTowardsGoal()
    {
        // TODO : slimey movement
        // TODO : orientation of the slime (always with the gravity down)
        var position = transform.position;
        Vector3 toGoal = (goal - position).normalized;
        currentSpeed = speed * toGoal + gravity * (Vector3.zero - position).normalized;
        position = position + Time.deltaTime * currentSpeed;
        transform.position = position;
    }
}
