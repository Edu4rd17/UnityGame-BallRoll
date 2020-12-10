using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyPlayerFollow();

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
    void EnemyPlayerFollow()
    {
        if (gameManager.isGameActive)
        {
            Vector3 enemyMoveDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(enemyMoveDirection * speed);
        }
    }
}
