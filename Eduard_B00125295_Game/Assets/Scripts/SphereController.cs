using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SphereController : MonoBehaviour
{
    public float speed;
    public float playerHealth = 100.0f;
    public Slider slider;
    private Rigidbody playerRb;
    private GameManager gameManager;
    public bool gameOver;
    public bool isOnGround = true;
    public bool hasGemPowerUp = false;
    public bool pickUpStar = false;
    public GameObject GempowerUpIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        SphereControll();

        GempowerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        PickPowerUp();
        PlayerHealth();
    }
    void SphereControll()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        playerRb.AddForce(movement * speed);
    }
    void PickPowerUp()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hasGemPowerUp)
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }
    void PlayerHealth()
    {
        slider.value = playerHealth;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            playerHealth = playerHealth - 20.0f;
            if (playerHealth < 5)
            {
                Debug.Log("Too much damage.Game Over!");
                gameManager.GameOver();

            }
        }

        if (collision.gameObject.CompareTag("Enemy") && hasGemPowerUp)
        {
            Debug.Log("Collision Detected");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GemPowerUp"))
        {
            hasGemPowerUp = true;
            GempowerUpIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(GemPowerUpCountDownRoutine());
        }
        if (other.CompareTag("StarPoints"))
        {
            pickUpStar = true;
            Destroy(other.gameObject);
            gameManager.UpdateScore(20);

            gameManager.WinGame();

        }
    }
    IEnumerator GemPowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(8);
        hasGemPowerUp = false;
        GempowerUpIndicator.gameObject.SetActive(false);
    }
}
