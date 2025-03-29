using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using TMPro;
using TMP_Text = TMPro.TextMeshProUGUI;
using System;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public GameObject player;
    public GameObject waterPrefab;
    public GameObject[] ratPrefabs;
    public GameObject pointTextBox;
    public GameObject questionTextBox;
    public GameObject scoreTextBox;
    public GameObject livesTextBox;
    int hearts = 3;
    int a = 0;
    int b = 0;
    int d = 0;  
    bool water = false;
    bool over = false;
    bool first_time = true;

    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
    }

    public void spawnRat() 
    {
        int ratIndex = UnityEngine.Random.Range(0, ratPrefabs.Length);
        int x;
        int y;
        water = false;
        a = UnityEngine.Random.Range(4, 15);
        do {
            b = UnityEngine.Random.Range(4, 15);
        } while (a == b);

        switch(ratIndex)
        {
                //brown rat
                case 0:
                    x = 10;
                    y = 3;
                    break;
                //black rat
                case 1:
                    x = 8;
                    y = 0;
                    break;
                //green rat
                case 2:
                    x = 10;
                    y = -3;
                    break;
                default:
                    x = 10;
                    y = 0;
                    break;
        }

        //set d equal to a,b, or c
        switch(UnityEngine.Random.Range(0, 2))
        {
            case 0:
                d = a;
                break;
            case 1:
                d = b;
                break;
            default:
                d = a;
                break;
        }

        // convert to binary
        string binary = Convert.ToString(d, 2);

        // spawn new enemy
        Instantiate(ratPrefabs[ratIndex], new Vector3(x, y), ratPrefabs[ratIndex].transform.rotation);
        
        questionTextBox.GetComponent<TMP_Text>().text = binary;
        pointTextBox.GetComponent<TMP_Text>().text = "1: " + a + " or 2: " + b;
        scoreTextBox.GetComponent<TMP_Text>().text = "Score: " + EnemyBehavior.score;
        livesTextBox.GetComponent<TMP_Text>().text = "Lives: " + hearts;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        hearts = 0;
        first_time = true;
        spawnRat();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hearts < 0)
        {
            hearts = 0;
        }
        if (EnemyBehavior.Destroy == true)
        {
            spawnRat();
            EnemyBehavior.Destroy = false;
            first_time = false;
        }
        if (water == false)
        {
            livesTextBox.GetComponent<TMP_Text>().text = "Lives: " + hearts;
            if (d == a)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    water = true;
                    Instantiate(waterPrefab, player.transform.position, waterPrefab.transform.rotation);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    hearts--;
                }
            }
            else if (d == b)
            {
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    water = true;
                    Instantiate(waterPrefab, player.transform.position, waterPrefab.transform.rotation);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    hearts--;
                }
            }
        }
        
        if (hearts == 0  || DestroyOutOfBounds.lose)
        {
            if (first_time == false)
            {
                pointTextBox.GetComponent<TMP_Text>().text = "Game Over";
            }
            else 
            {
                pointTextBox.GetComponent<TMP_Text>().text = "Start Game";
            }
            questionTextBox.GetComponent<TMP_Text>().text = "Press Space";
            Destroy(GameObject.FindWithTag("Enemy"));
            Time.timeScale = 0;
            over = true;
        }
        if (EnemyBehavior.score == 25)
        {
            pointTextBox.GetComponent<TMP_Text>().text = "You Win!";
            questionTextBox.GetComponent<TMP_Text>().text = "Press Space";
            Destroy(GameObject.FindWithTag("Enemy"));
            Time.timeScale = 0;
            over = true;
        }
        if (over == true && Input.GetKeyDown(KeyCode.Space))
        {
            EnemyBehavior.score = 0;
            DestroyOutOfBounds.lose = false;
            hearts = 3;
            spawnRat();
            over = false;
            Time.timeScale = 1;
        }
    }
    
}
