using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    Node[] PathNode;
    public GameObject EnemyObject;
    public float MoveSpeed;
    float Timer;
    int CurrentNode;
    static Vector3 CurrentPositionHolder;

    // Start is called before the first frame update
    void Start()
    {
        PathNode = GetComponentsInChildren<Node>();
        CheckNode();
    }

    void CheckNode() {
        //Check if the end of the node list has been reached already
        if (CurrentNode < PathNode.Length)
        {
            Timer = 0;
            CurrentPositionHolder = PathNode[CurrentNode].transform.position;
        }
    }

    //Draws a debug-only visible line that identifies the path to be made by an enemy.
    void DrawLine()
    {
        for (int i = 0; i < PathNode.Length; i++)
        {
            if (i < PathNode.Length - 1)
            {
                Debug.DrawLine(PathNode[i].transform.position, PathNode[i + 1].transform.position, Color.green);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
        //Debug.Log("Current node number: " + CurrentNode + "/" + PathNode.Length);
        Timer += Time.deltaTime * MoveSpeed;
        if (EnemyObject.transform.position != CurrentPositionHolder)
        {
            EnemyObject.transform.position = Vector3.Lerp(EnemyObject.transform.position, CurrentPositionHolder, Timer);
        }
        else
        {
            if (CurrentNode < PathNode.Length)
            {
                CurrentNode++;
                CheckNode();
            }
        }
    }
}
