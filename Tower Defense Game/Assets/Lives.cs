using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    Text text;
    public int LifeAmount = 20;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();

        LifeAmount = 20;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = " " + LifeAmount;
    }
    public void LifeLoss()
    {
        Debug.Log("You lost a life");
        LifeAmount = LifeAmount - 1;
    }
    
}