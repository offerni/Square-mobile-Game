using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {
    [SerializeField] GameObject deathvFX;
    [SerializeField] AudioClip[] sFXArray;
    [Range(0,1)][SerializeField] float sFXVolume = 0.25f;
    [SerializeField] int diamondValue = 10;
    private GameSession gameSession;



    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 8) {
            var sfxIndex = Random.Range(0, sFXArray.Length);
            AudioSource.PlayClipAtPoint(sFXArray[sfxIndex], Camera.main.transform.position, sFXVolume);
            gameSession = FindObjectOfType<GameSession>();
            gameSession.AddScore(diamondValue);
            Destroy(gameObject);
            TriggerDiamondDeath();
        }
    }

    public void TriggerDiamondDeath() {
        var tempvFX = Instantiate(deathvFX, transform.parent.position, transform.rotation);
        Destroy(tempvFX, 2);
    }
}
