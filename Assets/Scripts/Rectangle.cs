using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour {

    [SerializeField] float speed = 11;

    // Update is called once per frame
    void Update() {
        speed += 0.05f;
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 10) {
            Destroy(gameObject);
        }
    }
}
