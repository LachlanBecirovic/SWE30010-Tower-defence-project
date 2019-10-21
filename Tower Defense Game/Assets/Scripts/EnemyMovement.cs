using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Node targetNode;
    private Enemy enemyObject;
    private int currentWavepointIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentWavepointIndex = 0;

        //Retrieve the object
        enemyObject = GetComponent<Enemy>();

        //Find target waypoint to move to
        targetNode = PathFollower.PathNodes[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = targetNode.transform.position - transform.position;

        transform.Translate(moveDirection.normalized * enemyObject.moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, targetNode.transform.position) <= 0.05f) //Use a greater than 0 value to avoid potential over-shooting for fast enemies
        {
            RetargetNextWaypoint();
        }
    }

    void RetargetNextWaypoint()
    {
        if (currentWavepointIndex < PathFollower.PathNodes.Length - 1)
        {
            //Enemy has not yet reached the end of the path.
            currentWavepointIndex++;
            targetNode = PathFollower.PathNodes[currentWavepointIndex];
        }
        else
        {
            //Enemy has now reached the end without dying!
            ReachedEnd();
        }
    }

    void ReachedEnd()
    {
        //TODO lower players lives by 1.

        //Lower enemies alive by one and then destroy self.
        WaveSpawner.enemyAliveCount--;
        Destroy(gameObject);
    }
}
