using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    [SerializeField] GameObject[] waypoints;
    private int waypointIndex = 0;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject deathVFX;


    void Update() {
        Move();
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
            var tempVFX = Instantiate(deathVFX, gameObject.transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(tempVFX, 2);
        }
    }
}
