using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {
    private int score;
    private int bestScore;
    private int scoreAux;

    private void Awake() {
        SetUpSingleton();
    }

    public int GetScore() {
        return score;
    }

    public void AddScore(int scorePoint) {

        this.score += scorePoint;
        if (this.score > this.bestScore) {
            bestScore = score;
        }
    }

    public void ResetScore() {
        this.score = 0;
    }

    public int GetBestScore() {
        return this.bestScore;
    }

    private void SetUpSingleton() {
        int count = FindObjectsOfType<GameSession>().Length;

        if (count > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetBestScore() {
        bestScore = 0;
    }
}
