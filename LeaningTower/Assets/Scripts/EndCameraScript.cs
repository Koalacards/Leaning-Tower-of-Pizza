using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCameraScript : MonoBehaviour
{
    //Size to increase for every call of the scroll function
    private int sizeInc = 2;
    //Y position upward to increase for every call of the scroll function
    private int yInc = 2;

    //The camera component of this GameObject
    private Camera attachedCamera;

    void start() {
        attachedCamera = this.gameObject.GetComponent<Camera>();
    }

    //Increases the size and adjusts the position when the main camera scrolls
    public void incSize(int numScroll) {
        attachedCamera.orthographicSize = attachedCamera.orthographicSize + (sizeInc * numScroll);
        this.transform.position = 
            new Vector3(this.transform.position.x, this.transform.position.y + (yInc * numScroll),
             this.transform.position.z);
    }
}
