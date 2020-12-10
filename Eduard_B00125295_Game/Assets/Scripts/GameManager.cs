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
    public Button restartButton;
    public bool isGameActive;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore(0);
        isGameActive = true;
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
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
