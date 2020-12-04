using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
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
        Vector3 enemyMoveDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(enemyMoveDirection * speed);
    }
}
