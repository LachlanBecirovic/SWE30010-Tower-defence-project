using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gold : MonoBehaviour
{
    Text text;
    public int GoldAmount = 100;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent <Text>  ();

        GoldAmount = 100;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = " " + GoldAmount;
    }





















}
