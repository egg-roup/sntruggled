using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject playerStatsUI;
    private bool isPaused = false;

    public GameObject gameOverMenu;

    public Button resumeButton;
    public Button menuButton;
    public InputActionReference pauseAction;

    // public GameObject menuContainer;


    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
       
        resumeButton.onClick.AddListener(ResumeGame);
        menuButton.onClick.AddListener(GoMainMenu);

        pauseAction.action.performed += PauseButtonPressed;
        pauseAction.action.Enable();
    }

    void OnDestroy()
    {
        pauseAction.action.performed -= PauseButtonPressed;
        pauseAction.action.Disable();
    }



    public void PauseButtonPressed(InputAction.CallbackContext context) {
        if (gameOverMenu.gameObject.activeSelf) {
            return;
        }

        if (isPaused) {
            ResumeGame();
        }
        else {
            PauseGame();
        }
    }

    public void ResumeGame() {
        pauseMenu.SetActive(false);
        playerStatsUI.SetActive(true);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void GoMainMenu() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        GameState.instance.isSceneTransitioning = true;
        SceneTransitionManager.singleton.GoToSceneAsync(0);
        Debug.Log("LOADING SCENE!");
    }

    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        playerStatsUI.SetActive(false);

        // PositionMenu();
    }

    // private void PositionMenu() {
    //     Vector3 vHeadPos = Camera.main.transform.position;
    //     Vector3 vGazeDir = Camera.main.transform.forward;
    //     menuContainer.transform.position = (vHeadPos + vGazeDir * 3.0f) + new Vector3(0.0f, -.40f, 0.0f);

    //     // Make the menu face the camera
    //     Vector3 vRot = Camera.main.transform.eulerAngles;
    //     vRot.z = 0;  // Optional: This ensures no tilting on the Z-axis, only yaw and pitch.
    //     menuContainer.transform.eulerAngles = vRot;
    // }
}
