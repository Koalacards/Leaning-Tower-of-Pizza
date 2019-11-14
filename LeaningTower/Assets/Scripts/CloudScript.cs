using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CloudScript : MonoBehaviour
{

    //Velocity of the cloud as it moves across the screen
    private float velocity;

    //This cloud's rigidbody
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        velocity = UnityEngine.Random.Range(0.5f, 1.5f);
        rb = this.GetComponent<Rigidbody2D>();
        if (transform.position.x > 0) {
            velocity = velocity * -1;
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector2.right * velocity);
    }

    //Destroys this object if it runs into the cloud boundaries on the left and right side of the screen
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("CloudCatcher")) {
            Destroy(this);
        }
    }
}
