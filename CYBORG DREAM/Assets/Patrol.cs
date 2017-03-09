using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

    public Transform[] patrol;
    private int patrolIndex;
    private Vector3 currentPosition;
    private Vector3 targetPosition;
    public float moveSpeed;
    public bool isEnabled = true;



    // Use this for initialization
    void Start()
    {
        if (patrol.Length < 2)
            return;

        transform.position = patrol[0].position;
        currentPosition = transform.position;
        targetPosition = patrol[1].position;
        patrolIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (patrol.Length < 2)
            return;

        if (isEnabled)
        {
            currentPosition = transform.position;

            if ( Vector3.Distance(currentPosition, targetPosition) < 0.5 )
                patrolIndex++;
            if (patrolIndex >= patrol.Length)
                patrolIndex = 0;
            targetPosition = patrol[patrolIndex].position;
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
