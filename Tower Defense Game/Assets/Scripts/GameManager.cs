using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public  Transform[] waypointArray;

    static GameManager gameInstance;

    //GameInstance Property
    public static GameManager Instance
    {
        get
        {
            if (gameInstance == null)
            {
                gameInstance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }
            return gameInstance;
        }
        set { gameInstance = value; }
    }
    
    void Awake()
    {
        Instance = this;
    }
}