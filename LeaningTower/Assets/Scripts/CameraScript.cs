using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    //Velocity that the camera scrolls
    public float scrollVelocity;

    //Accessing the pipe
    public PizzaPiper3000 pipe;

    //boolean for whether or not to scroll
    private bool scroll;

    //Keeps track of the old Y Position to scroll up 4 units from this position
    private float oldYPos;

    //Size modifier to increase for every call of the scroll function
    private int sizeInc = 2;

    //Y position modifier upward to increase for every call of the scroll function
    private int yInc = 2;

    //The camera component of this object
    private Camera attachedCamera;

    // Start is called before the first frame update
    void Start()
    {
        scroll = false;
        oldYPos = 0;
        attachedCamera = this.gameObject.GetComponent<Camera>();
        pipe.gameObject.SetActive(true);
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
        pipe.stopAnim();
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
            pipe.startAnim();
            PlayerPrefs.SetInt("CanRelease", 0);
            pipe.allowPipeMovement();
        }
    }

    //Called once the tower is falling- changes the camera to an overall view of the boxes
    public void GameEndView(int numScrolls) {
        //Deactivates the pipe
        pipe.gameObject.SetActive(false);
        //Sets the camera back to the original position
        this.transform.position = new Vector3(0, 0, -10);
        attachedCamera.orthographicSize = attachedCamera.orthographicSize + (sizeInc * numScrolls);
        this.transform.position = 
            new Vector3(this.transform.position.x, this.transform.position.y + (yInc * numScrolls),
             this.transform.position.z);
    }
}
