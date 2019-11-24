using System;
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
    [SerializeField] private bool teleportToGoal = false;
    private Vector3 currentSpeed =  Vector3.zero;
    private Rigidbody2D rigidbody2d;
    private SlimeAvatar avatar;
    private GameManager gameManager;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        avatar = GetComponent<SlimeAvatar>();
        gameManager = GameManager.Instance;
        FindObjectOfType<GameManager>().PlayerMouseDownObserver += PlayerSelectedGoal;
        FindObjectOfType<GameManager>().PlayerMouseUpObserver += PlayerUnSelectedGoal;
    }

    // Update is called once per frame
    void Update()
    {
        if (teleportToGoal)
            TeleportToGoal();
        if (avatar.State == SlimeState.Living)
            IsGoalReached();
    }

    private void FixedUpdate()
    {
        if (avatar.State == SlimeState.Living)
            MoveTowardsGoal();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigidbody2d.AddForce(-500.0f * gravity * (Vector3.zero - transform.position).normalized);
    }

    public bool IsGoalReached()
    {
        // TODO : if the cube is less than goalThreshold from the goal, we teleport it and return true
        if ((transform.position - Goal).magnitude < goalThreshold)
        {
            rigidbody2d.velocity = Vector2.zero;
            transform.position = goal;
            transform.rotation = Quaternion.AngleAxis(EarthAvatar.Instance.GetAngle(goal), Vector3.back);
            avatar.Death();
            return true;
        }

        return false;
    }

    public void GetNewGoal()
    {
        // get a goal :
        // find random interest point
        Tuple<Vector3, Structure> res = GameManager.Instance.GetRandomInterestPoint();
        goal = res.Item1;
        avatar.RelStruct = res.Item2;
    }

    private void TeleportToGoal()
    {
        transform.position = goal;
        rigidbody2d.velocity = Vector2.zero;
    }

    private void MoveTowardsGoal()
    {
        // TODO : slimey movement
        // TODO : orientation of the slime (always with the gravity down)
        var position = transform.position;
        Vector3 toGoal = (Goal - position).normalized;
        currentSpeed = speed * toGoal + gravity * (Vector3.zero - position).normalized;
        //position = position + Time.deltaTime * currentSpeed;
        rigidbody2d.velocity = currentSpeed;
        //rigidbody.MovePosition(position);
    }

    public void PlayerSelectedGoal(object sender, EventArgs args)
    {
        if (avatar.RelStruct != null)
        {
            avatar.RelStruct.ReleasePoint(goal);
            avatar.RelStruct = null;
        }

        if (avatar.State == SlimeState.Living)
        {
            Goal = (args as PlayerObserverEventArgs).Position;
        }
    }

    public void PlayerUnSelectedGoal(object sender, EventArgs args)
    {
        if (avatar.State == SlimeState.Living)
        {
            GetNewGoal();
        }
    }
}
