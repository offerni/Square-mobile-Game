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
    private MusicPlayer musicPlayer;

    [SerializeField] GameObject pauseMenu;


    private void Awake() {
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

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
            gameSession.SetLeaderBoardBestScore();
            //gameSession.ResetDeadCount();
            gameSession.RandomizeAdCount();
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
        musicPlayer.changeVolume(0.25f);
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        musicPlayer.changeVolume(0.75f);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ToggleMusic() {

        musicPlayer.ToggleMusic();
    }
}
