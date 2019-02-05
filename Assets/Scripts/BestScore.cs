using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScore : MonoBehaviour {

    private GameSession gameSession;
    private Text bestScoreText;

    private void Start() {
        gameSession = FindObjectOfType<GameSession>();
        bestScoreText = GetComponent<Text>();
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update() {
        ShowBestScore();
    }

    private void ShowBestScore() {
        bestScoreText.text = gameSession.GetBestScore().ToString();
        if (gameSession.GetBestScore() > gameSession.GetScore()) {
            gameObject.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
