using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{

    [SerializeField] private float speed;
    private Vector3 goal;
    private Vector3 currentSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        goal = Vector3.zero;
        currentSpeed = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsGoal();
    }

    private void MoveTowardsGoal()
    {
        // TODO : slimey movement
        var position = transform.position;
        Vector2 toGoal = goal - position;
        currentSpeed = speed * toGoal;
        position = position + Time.deltaTime * currentSpeed;
        transform.position = position;
    }
}
