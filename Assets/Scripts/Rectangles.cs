using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangles : MonoBehaviour {

    private bool selected;
    private SceneController sceneController;

    [SerializeField] bool spawning;
    [SerializeField] Rectangle rectanglePrefab;
    [SerializeField] GameObject cursorPressed;
    [SerializeField] float rectangleSpeed;
    [SerializeField] float rectangleIncrementSpeed;

    [Header("Time between spawns Random Factor")]
    [SerializeField] float minTime = 1;
    [SerializeField] float maxTime = 2;

    [Header("Rectangle Position Random Factor")]
    [SerializeField] int minYposition = -4;
    [SerializeField] int maxYposition = 4;

    [SerializeField] GameObject spawnPosition;

    // Start is called before the first frame update
    private void Awake() {
        StopAllCoroutines();
        StartCoroutine(SpawnRectangle());
        sceneController = FindObjectOfType<SceneController>();


    }

    void Update() {
        rectanglePrefab.speed = rectangleSpeed;
        rectanglePrefab.incrementSpeed = rectangleIncrementSpeed;

        if (sceneController.gameIsPaused) {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        } else {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        if (selected == true) {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(transform.position.x, cursorPos.y);
            cursorPressed.SetActive(true);
        }
        if(Input.GetMouseButtonUp(0)) {
            selected = false;
            cursorPressed.SetActive(false);
        }
    }
    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            selected = true;
        }
    }

    IEnumerator SpawnRectangle() {
        while (spawning) {
            rectanglePrefab.speed = rectangleSpeed;
            rectanglePrefab.incrementSpeed = rectangleIncrementSpeed;
            var timeBetweenSpawns = Random.Range(minTime, maxTime);
            var randomYPosition = Random.Range(minYposition, maxYposition);
            var clone = Instantiate(rectanglePrefab, new Vector2(spawnPosition.transform.position.x, transform.position.y + randomYPosition), transform.rotation);
            clone.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
