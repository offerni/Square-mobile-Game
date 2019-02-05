using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    private int numberOfScenes;
    private int currentSceneIndex;
    public bool gameIsPaused = false;
    private GameSession gameSession;
    private Rectangle[] rectangles;

    [SerializeField] GameObject pauseMenu;

    

    // Start is called before the first frame update
    void Start() {
        numberOfScenes = SceneManager.sceneCountInBuildSettings;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        ResumeGame();
        pauseMenu.SetActive(false);
        gameSession = FindObjectOfType<GameSession>();
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);

        if (sceneName.Equals("StartScreen")) {
            gameSession.ResetScore();
            gameSession.ResetBestScore();
        }
    }

    public void LoadNextScene() {
        if (currentSceneIndex + 1 < numberOfScenes) {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    public void RestartGame() {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        LoadScene("Level" + sceneIndex);
        gameSession.ResetScore();
        rectangles = FindObjectsOfType<Rectangle>();
        foreach (var rectangle in rectangles) {
            Destroy(rectangle);
        }
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        gameIsPaused = true;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenu.SetActive(false);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
