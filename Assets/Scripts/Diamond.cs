using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {

    [SerializeField] int diamondValue = 10;
    private GameSession gameSession;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            gameSession = FindObjectOfType<GameSession>();
            gameSession.AddScore(diamondValue);
            Destroy(gameObject);
        }
    }
}
