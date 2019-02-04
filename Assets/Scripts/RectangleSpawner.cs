using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectangleSpawner : MonoBehaviour {

    [SerializeField] bool spawning;
    [SerializeField] float timeBetweenSpawns = 1;
    [SerializeField] Rectangle rectanglePrefab;
    [SerializeField] Rectangles rectangles;
    [SerializeField] int minYposition = -4;
    [SerializeField] int maxYposition = 4;
   
    
    // Start is called before the first frame update
    private void Start() {
        StopAllCoroutines();
        StartCoroutine(SpawnRectangle());
        rectangles = FindObjectOfType<Rectangles>();
    }



    IEnumerator SpawnRectangle() {
        while (spawning) {
            var randomYPosition = Random.Range(-minYposition, maxYposition);
            var clone = Instantiate(rectanglePrefab, new Vector2(10, transform.position.y + randomYPosition), transform.rotation);
            clone.transform.parent = rectangles.transform;
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
