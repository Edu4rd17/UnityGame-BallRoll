using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI pickTrophy;
    public GameObject trophyPrefab;
    public Button restartButton;
    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        isGameActive = true;
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void WinGame()
    {
        if (score >= 100)
        {
            winText.gameObject.SetActive(true);
            pickTrophy.gameObject.SetActive(true);
            Instantiate(trophyPrefab, GenerateTrophyRandom(), trophyPrefab.transform.rotation);
        }
    }
    public Vector3 GenerateTrophyRandom()
    {
        float trophySpawnPosX = Random.Range(-45, 45);
        float trophySpawnPosZ = Random.Range(-45, 45);

        Vector3 randomLocation = new Vector3(trophySpawnPosX, 0, trophySpawnPosZ);

        return randomLocation;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
