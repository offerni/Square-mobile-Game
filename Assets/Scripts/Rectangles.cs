using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangles : MonoBehaviour {

    private bool selected;

    [SerializeField] bool spawning;
    [SerializeField] Rectangle rectanglePrefab;

    [Header("Time between spawns Random Factor")]
    [SerializeField] float minTime = 1;
    [SerializeField] float maxTime = 2;

    [Header("Rectangle Position Random Factor")]
    [SerializeField] int minYposition = -4;
    [SerializeField] int maxYposition = 4;

    // Start is called before the first frame update
    private void Start() {
        StopAllCoroutines();
        StartCoroutine(SpawnRectangle());
        
    }

    void Update() {
        if (selected == true) {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(transform.position.x, cursorPos.y);
        }
        if(Input.GetMouseButtonUp(0)) {
            selected = false;
        }
    }
    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            selected = true;
        }
    }

    IEnumerator SpawnRectangle() {
        while (spawning) {
            var timeBetweenSpawns = Random.Range(minTime, maxTime);
            var randomYPosition = Random.Range(minYposition, maxYposition);
            var clone = Instantiate(rectanglePrefab, new Vector2(10, transform.position.y + randomYPosition), transform.rotation);
            clone.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
