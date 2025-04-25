using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager scoreManager;

    public float score = 0f;
    public float coins = 0f;
    public float highScore = 0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI highScoreText;

    void Awake() {
        scoreManager = this;
    }

    void Start() {
        Debug.Log("ScoreManager initialized");
        
        LoadHighScore();
        UpdateUI();
    }

    public float GetHighestScore() => highScore;
    public float GetCurrentScore() => score;
    public int GetCoinCount() => Mathf.FloorToInt(coins);

    public void AddKill() {
        score += 10;
        coins += 1;

        if (score > highScore) {
            UpdateHighScore();
        }

        UpdateUI();
    }

    public void ResetScore() {
        score = 0;
        coins = 0;
        UpdateUI();
    }

    private void UpdateUI() {
        if (scoreText) scoreText.text = "Score: " + Mathf.FloorToInt(score);
        if (coinsText) coinsText.text = Mathf.FloorToInt(coins).ToString();
        if (highScoreText) highScoreText.text = "Highest: " + Mathf.FloorToInt(highScore);
    } 

    public void LoadHighScore() {
        if (PlayerPrefs.HasKey("SavedHighScore")) {
            highScore = PlayerPrefs.GetInt("SavedHighScore");
        }
        highScoreText.text = "High Score: " + Mathf.FloorToInt(highScore);
    }

    public void UpdateHighScore() {
        // check for highscore
        if (PlayerPrefs.HasKey("SavedHighScore")){
            // check which is greater
            if (score > PlayerPrefs.GetInt("SavedHighScore")) {
                // set a new high score
                highScore = score;
                PlayerPrefs.SetInt("SavedHighScore", Mathf.FloorToInt(highScore));
                PlayerPrefs.Save();
            }
        }
        else {
            // set highscore
            PlayerPrefs.SetInt("SavedHighScore", Mathf.FloorToInt(score));
        }
    }
}
