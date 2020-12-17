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
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public AudioClip crashObject;
    public AudioClip crashDeath;
    public AudioClip powerUpPick;
    public AudioClip pointsPick;
    public AudioClip evil;
    public bool gameOver;
    public bool isOnGround = true;
    public bool hasGemPowerUp = false;
    public bool healthPickUp = false;
    public bool pickUpGood = false;
    public bool pickUpBad = false;
    public GameObject GempowerUpIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //sphere control method has been called
        SphereControll();

        //the powerup indicator will follow the spheres' position when it will have the powerup
        GempowerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        //if the sphere goes over the -10 on the y it will be set as inactve
        if (transform.position.y < -10)
        {
            gameObject.SetActive(false);
            gameManager.GameOver();
        }
        //calling the methods by reference
        PickPowerUp();
        PlayerHealth();
    }
    void SphereControll()
    {
        if (gameManager.isGameActive)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

            playerRb.AddForce(movement * speed);
        }
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
        //only if the game is still active do the rest
        if (gameManager.isGameActive)
        {
            //a lot if statements for conditions so that if y then x do...
            if (collision.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
            }
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                playerAudio.PlayOneShot(crashObject, 1.0f);
                gameOver = true;
                playerHealth = playerHealth - 20.0f;
                //if the player health is smaller or equal to 0 then game over
                if (playerHealth <= 0)
                {

                    explosionParticle.Play();
                    playerAudio.PlayOneShot(crashDeath, 1.0f);
                    Debug.Log("Too much damage.Game Over!");
                    gameManager.GameOver();

                }
            }

            if (collision.gameObject.CompareTag("Enemy") && hasGemPowerUp)
            {
                //add impulse when having the powerup
                Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
                enemyRigidbody.AddForce(awayFromPlayer * 10, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //only if the game is still active do the rest
        if (gameManager.isGameActive)
        {
            if (other.CompareTag("GemPowerUp"))
            {
                hasGemPowerUp = true;
                GempowerUpIndicator.gameObject.SetActive(true);
                playerAudio.PlayOneShot(powerUpPick, 1.0f);
                Destroy(other.gameObject);
                StartCoroutine(GemPowerUpCountDownRoutine());
            }
            if (other.CompareTag("Health"))
            {
                if (playerHealth < 100)
                {
                    healthPickUp = true;
                    playerAudio.PlayOneShot(powerUpPick, 1.0f);
                    playerHealth = playerHealth + 20.0f;
                    Destroy(other.gameObject);
                }
            }
            if (other.CompareTag("StarPoints"))
            {
                pickUpGood = true;
                playerAudio.PlayOneShot(pointsPick, 1.0f);
                Destroy(other.gameObject);
                gameManager.UpdateScore(20);
                gameManager.WinGame();
            }
            if (other.CompareTag("DollarPoints"))
            {
                pickUpGood = true;
                playerAudio.PlayOneShot(pointsPick, 1.0f);
                Destroy(other.gameObject);
                gameManager.UpdateScore(10);
                gameManager.WinGame();

            }
            if (other.CompareTag("FirePoints"))
            {
                pickUpGood = true;
                playerAudio.PlayOneShot(pointsPick, 1.0f);
                Destroy(other.gameObject);
                gameManager.UpdateScore(5);
                gameManager.WinGame();
            }
            if (other.CompareTag("BadPoints"))
            {
                pickUpBad = true;
                playerAudio.PlayOneShot(evil, 1.0f);
                Destroy(other.gameObject);
                gameManager.UpdateScore(-10);
            }

        }
        IEnumerator GemPowerUpCountDownRoutine()
        {
            yield return new WaitForSeconds(8);
            hasGemPowerUp = false;
            GempowerUpIndicator.gameObject.SetActive(false);
        }
    }
}
