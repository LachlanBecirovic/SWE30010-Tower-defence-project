using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public static Node[] PathNodes;
    static Vector3 CurrentPositionHolder;

    // Start is called before the first frame update
    void Start()
    {
        PathNodes = GetComponentsInChildren<Node>();
    }

    //Draws a debug-only visible line that identifies the path to be made by an enemy.
    void DrawLine()
    {
        for (int i = 0; i < PathNodes.Length; i++)
        {
            if (i < PathNodes.Length - 1)
            {
                Debug.DrawLine(PathNodes[i].transform.position, PathNodes[i + 1].transform.position, Color.green);
            }
        }
    }
}
