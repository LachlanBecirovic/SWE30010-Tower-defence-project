using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemyObject;
    [SerializeField] private Transform[] waypoints;
    private int waypointIndex = 0;
    private float movementSpeed;

    void Start()
    {
        enemyObject = GetComponent<Enemy>();

        waypoints = GameManager.Instance.wayPoints;
        transform.position = waypoints[waypointIndex].transform.position;
    }
    void Update()
    {
        Move();
        movementSpeed = enemyObject.moveSpeed;
        ReachedEnd();
    }
    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                waypoints[waypointIndex].transform.position,
                movementSpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }
    void ReachedEnd()
    {
        if (waypointIndex == waypoints.Length)
        {
            WaveSpawner.enemyAliveCount--;
            Destroy(gameObject);
        }
    }
}