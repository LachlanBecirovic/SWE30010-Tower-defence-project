using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Enemy enemyObject;
    [SerializeField] private Transform[] waypointArray;
    private int waypointIndex = 0;
    private float movementSpeed;
    public int Health = 3;
    public Lives Script;

    // Start is called before the first frame update
    void Start()
    {
        enemyObject = GetComponent<Enemy>();

        waypointArray = GameManager.Instance.waypointArray; //Load waypoints from GameManager
        transform.position = waypointArray[waypointIndex].transform.position; //Teleport enemy to first waypoint when spawned
    }

    // Update is called once per frame
    void Update()
    {
        //If the enemy has not reached the end of the waypoint array, move it to the next
        if (waypointIndex <= waypointArray.Length - 1)
        {
            //For testing purposes (and to carry over code from Ryan's now redundant script)
            //Draw a line to the next node targeted by the enemy
            DrawLine(waypointIndex);

            //Move enemy towards targeted waypoint
            transform.position = Vector3.MoveTowards(transform.position, waypointArray[waypointIndex].transform.position, enemyObject.moveSpeed * Time.deltaTime);

            //If the enemy has reached the waypoint, set it's goal to the next in the list
            if (transform.position == waypointArray[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }

        //If the enemy has reached the end without dying it should lower the player's lives by 1 and destroy itself.
        if (waypointIndex == waypointArray.Length)
        {
            //TODO lower players lives by 1.

            //Lower enemies alive by one and then destroy self.
            WaveSpawner.enemyAliveCount--;
            Destroy(gameObject);
            Script.LifeLoss();

            //Lives.LifeAmount = Lives.LifeAmount - 1;
        }
    }

    //Draw next node the enemy is targeting
    //Visible in editor only for debug purposes
    void DrawLine(int i)
    {
        Debug.DrawLine(waypointArray[i].transform.position, enemyObject.transform.position, Color.green);
    }
}