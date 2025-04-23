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
    // public float highScore = 0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    // public TextMeshProUGUI highScoreText;

    void Awake()
    {
        // Singleton setup
        if (scoreManager == null)
        {
            scoreManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //LoadHighScore();
        UpdateUI();
    }

    // public float GetHighestScore() => highestScore;
    public float GetCurrentScore() => score;
    public int GetCoinCount() => Mathf.FloorToInt(coins);

    public void AddKill() {
        score += 10;
        coins += 1;

        // if (score > highScore) {
        //     UpdateHighScore();
        // }

        UpdateUI();
    }

    public void ResetScore() {
        score = 0;
        coins = 0;
        UpdateUI();
    }

    private void UpdateUI() {
        if (scoreText) scoreText.text = "Points: " + Mathf.FloorToInt(score);
        if (coinsText) coinsText.text = "Coins: " + Mathf.FloorToInt(coins);
    } 

    // public void LoadHighScore() {
    //     if (PlayerPrefs.HasKey("SavedHighScore")) {
    //         highestScore = PlayerPrefs.GetInt("SavedHighScore");
    //     }
    //     highestScoreText.text = "High Score: " + Mathf.FloorToInt(highestScore);
    // }

    // public void UpdateHighScore() {
    //     // check for highscore
    //     if (PlayerPrefs.HasKey("SavedHighScore")){
    //         // check which is greater
    //         if (score > PlayerPrefs.GetInt("SavedHighScore")) {
    //             // set a new high score
    //             highestScore = score;
    //             PlayerPrefs.SetInt("SavedHighScore", Mathf.FloorToInt(highestScore));
    //             PlayerPrefs.Save();
    //         }
    //     }
    //     else {
    //         // set highscore
    //         PlayerPrefs.SetInt("SavedHighScore", Mathf.FloorToInt(score));
    //     }
    // }
}
