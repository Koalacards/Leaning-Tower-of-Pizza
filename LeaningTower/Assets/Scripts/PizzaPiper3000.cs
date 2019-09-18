using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPiper3000 : MonoBehaviour
{
    //speed at which the pipe moves
    public float velocity;

    //Box that the pipe drops
    public GameObject box;

    //determines whether the pipe can move or not
    private bool canMove;


    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Calls once per tick consistently
    void FixedUpdate() {
        if (canMove) {
            MovePipe();
        }
    }

    //Moves the pipe
    void MovePipe() {
        this.transform.Translate(Vector2.left * velocity * Time.deltaTime);
    }

    //checks for collisions between the pipe and its boundary
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("PipeBoundary")) {
            //changes the direction the pipe moves
            velocity = velocity * -1;
        }
    }

    //Creates a box
    //Will be called when the user taps on the screen
    public void createBox() {
        //0 in CanRelease means that the pipe can create a new Box
        if (0 == PlayerPrefs.GetInt("CanRelease")) {
            GameObject newBox = Instantiate(box);
            newBox.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 0.468f);
            //Stops the creation of another box in quick succession
            //This will be changed to 0 by BoxCollector
            PlayerPrefs.SetInt("CanRelease", 1);
        }
    }

    //Increases the velocity of the pipe (to be called by CameraScript)
    public void speedUp() {
        if (velocity > 0) {
            velocity = velocity + 0.25f;
        } else {
            velocity = velocity - 0.25f;
        }
    }

    //Stops movement of the pipe
    public void stopPipe() {
        canMove = false;
    }

    //Allows the pipe to move
    public void allowPipeMovement() {
        canMove = true;
    }
}
