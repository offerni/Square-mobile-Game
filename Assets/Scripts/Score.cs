using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    private GameSession gameSession;
    private Text scoreText;

    void Start() {
        gameSession = FindObjectOfType<GameSession>();
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        ShowScore();
    }

    private void ShowScore() {
        scoreText.text = gameSession.GetScore().ToString();
    }
}
