using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    Node[] PathNode;
    public GameObject Player;
    public float MoveSpeed;
    float Timer;
    int CurrentNode;
    static Vector3 CurrentPositionHolder;
    // Start is called before the first frame update
    void Start()
    {
        PathNode = GetComponentsInChildren<Node> ();
        CheckNode();
    }
    void CheckNode() {
        if (CurrentNode < PathNode.Length - 1)
        {
            Timer = 0;
            CurrentPositionHolder = PathNode[CurrentNode].transform.position;
        }
    }
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
        Debug.Log(CurrentNode);
        Timer += Time.deltaTime * MoveSpeed;
        if (Player.transform.position != CurrentPositionHolder)
        {
            Player.transform.position = Vector3.Lerp(Player.transform.position, CurrentPositionHolder, Timer);
        }
        else
        {
            if (CurrentNode < PathNode.Length - 1)
            {
                CurrentNode++;
                CheckNode();
            }
        }
    }
}
