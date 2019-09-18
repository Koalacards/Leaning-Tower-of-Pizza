using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    //Velocity that the camera scrolls
    public float scrollVelocity;

    //Accessing the pipe
    public PizzaPiper3000 pipe;

    //boolean for whether or not to scrool
    private bool scroll;

    //Keeps track of the old Y Position to scroll up 4 units from this position
    private float oldYPos;

    // Start is called before the first frame update
    void Start()
    {
        scroll = false;
        oldYPos = 0;
    }


    //Scrolls the camera upward if scroll is true
    void FixedUpdate() {
        if (scroll) {
            MoveCamera(oldYPos + 4f);
        }
    }

    //Scrolls the screen upwards
    public void Scroll() {
        PlayerPrefs.SetInt("CanRelease", 1);
        oldYPos = this.transform.position.y;
        scroll = true;
        pipe.speedUp();
        pipe.stopPipe();
    }

    //Moves the camera
    public void MoveCamera(float targetYPos) {
        if (this.transform.position.y < targetYPos) {
            transform.Translate(Vector3.up * scrollVelocity * Time.deltaTime);
        } else {
            scroll = false;
            PlayerPrefs.SetInt("CanRelease", 0);
            pipe.allowPipeMovement();
        }
    }
}
