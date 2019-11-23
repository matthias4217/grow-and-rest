﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{

    [SerializeField] private float speed = 1;
    [SerializeField] private Vector3 goal = Vector3.zero;
    public Vector3 Goal { get => goal; set => goal = value; }

    [SerializeField] private float gravity = 0.5f;
    [SerializeField] private float goalThreshold = 1;
    private Vector3 currentSpeed =  Vector3.zero;
    private Rigidbody2D rigidbody;
    private SlimeAvatar avatar;
    private GameManager gameManager;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        avatar = GetComponent<SlimeAvatar>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGoalReached();
    }

    private void FixedUpdate()
    {
        if (avatar.State == SlimeState.Living)
            MoveTowardsGoal();
    }

    public bool IsGoalReached()
    {
        // TODO : if the cube is less than goalThreshold from the goal, we teleport it and return true
        if ((transform.position - Goal).magnitude < goalThreshold)
        {
            rigidbody.velocity = Vector2.zero;
            rigidbody.MovePosition(goal);
            avatar.Death();
            return true;
        }

        return false;
        //throw new NotImplementedException();
    }

    public bool GetNewGoal()
    {
        // get a goal :
        // find nearest chosen one, and do stuff
        throw new NotImplementedException();
    }

    private void MoveTowardsGoal()
    {
        // TODO : slimey movement
        // TODO : orientation of the slime (always with the gravity down)
        var position = transform.position;
        Vector3 toGoal = (Goal - position).normalized;
        currentSpeed = speed * toGoal + gravity * (Vector3.zero - position).normalized;
        //position = position + Time.deltaTime * currentSpeed;
        rigidbody.velocity = currentSpeed;
        //rigidbody.MovePosition(position);
    }
}
