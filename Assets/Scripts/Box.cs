using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    [SerializeField] GameObject[] waypoints;
    private int waypointIndex = 0;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSFX;
    [Range(0,1)][SerializeField] float deathSFXVolume = 0.75f;
    private SceneController sceneController;
    private GameSession gameSession;
    

    private void Start() {
        sceneController = FindObjectOfType<SceneController>();
        gameSession = FindObjectOfType<GameSession>();
    }

    void Update() {
        if (!sceneController.gameIsPaused) {
            Move();
        }
    }

    private void Move() {

        var moveFrame = moveSpeed * Time.deltaTime;

        if (waypointIndex <= waypoints.Length - 1) {
            var targetPosition = waypoints[waypointIndex].transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed);

            if (transform.position == targetPosition) {
                waypointIndex++;
            }
        }
        if(waypointIndex == 3) {
            waypointIndex = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 9) {
            TriggerDeath();
            //var randomCount = gameSession.adCount;
            //if (gameSession.deadCount == randomCount) {
            //    StartCoroutine(ShowAdAfterSeconds());
            //    gameSession.deadCount = 0;
            //} 
        }
    }

    private void TriggerDeath() {
        var tempVFX = Instantiate(deathVFX, gameObject.transform.position, transform.rotation);
        transform.position = new Vector2(9999, 9999);
        Destroy(tempVFX, 2);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
        StopAllCoroutines();
        StartCoroutine(RespawnBoxAfterSeconds());
        //gameSession.deadCount++;
        gameSession.SaveScore();
    }

    IEnumerator RespawnBoxAfterSeconds() {
        yield return new WaitForSeconds(2.5f);
        sceneController.RestartGame();
    }

    //IEnumerator ShowAdAfterSeconds() {
    //    yield return new WaitForSeconds(1);
    //    AdManager.instance.ShowVideoAd();
    //}
}
