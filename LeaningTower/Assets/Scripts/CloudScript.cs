using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{

    //Velocity of the cloud as it moves across the screen
    private float velocity;


    // Start is called before the first frame update
    void Start()
    {
        velocity = Random.Range(1.0f, 2.0f);
        if (transform.position.x < 0) {
            velocity = velocity * -1;
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(Vector2.left * velocity * Time.deltaTime);
    }

    //Destroys this object if it runs into the cloud boundaries on the left and right side of the screen
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("CloudCatcher")) {
            Destroy(this);
        }
    }
}
