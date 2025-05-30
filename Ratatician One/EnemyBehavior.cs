using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBehavior : MonoBehaviour
{
    public int health = 1;
    public float speed = 100.0f;
    public GameManager gameManager;
    public static int score = 0;
    public static bool Destroy = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.gameManager;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            score++;
            Destroy(gameObject);
            //gameManager.spawnRat();
            Destroy = true;
            //gameManager.score++;
            
        }
    }
}
