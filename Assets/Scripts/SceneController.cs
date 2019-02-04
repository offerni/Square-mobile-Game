using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private int numberOfScenes;
    private int currentSceneIndex;
    public bool gameIsPaused;


    // Start is called before the first frame update
    void Start() {
        numberOfScenes = SceneManager.sceneCountInBuildSettings;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene() {
        if (currentSceneIndex + 1 < numberOfScenes) {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void RestartGame() {
        LoadScene("Game");
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
