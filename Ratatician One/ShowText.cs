using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowText : MonoBehaviour
{
    public string textValue;
    private Text textElement;
    public int coefOne, coefTwo, coefThree;
    public float RandNum = 0f;

    // Start is called before the first frame update
    void Start()
    {
        textElement.text = textValue;
        //Random rnd = new Random();
        //coefOne  = rnd.Next(1, 10);  // creates a number between 1 and 12
        //coefTwo   = rnd.Next(1, 7);   // creates a number between 1 and 6
        //coefThree   = rnd.Next(1, 19);     // creates a number between 0 and 51

    }


    // Update is called once per frame
    void Update()
    {
        // RandNum = Random.Range(1, 10);
        // textElement.text = RandNum.ToString();
        //textElement.text = coefOne;

    }
}
