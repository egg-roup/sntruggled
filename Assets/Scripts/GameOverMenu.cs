using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;
    private bool isDead = false;

    public Button restartButton;
    public Button mainButton;

    public GameObject menuContainer;

    // Start is called before the first frame update
    void Start()
    {
        gameOverMenu.SetActive(false);
       
        restartButton.onClick.AddListener(RestartGame);
        mainButton.onClick.AddListener(GoMenu);

    }

    public void ShowGameOver() {
        isDead = true;
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);

        PositionMenu();
    }

    public void RestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu() {
        Time.timeScale = 1;
        SceneTransitionManager.singleton.GoToSceneAsync(0);
    }

    private void PositionMenu() {
        Vector3 vHeadPos = Camera.main.transform.position;
        Vector3 vGazeDir = Camera.main.transform.forward;
        menuContainer.transform.position = (vHeadPos + vGazeDir * 3.0f) + new Vector3(0.0f, -.40f, 0.0f);

        // Make the menu face the camera
        Vector3 vRot = Camera.main.transform.eulerAngles;
        vRot.z = 0;  // Optional: This ensures no tilting on the Z-axis, only yaw and pitch.
        menuContainer.transform.eulerAngles = vRot;
    }
}

