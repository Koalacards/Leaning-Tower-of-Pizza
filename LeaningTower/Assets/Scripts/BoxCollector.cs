using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollector : MonoBehaviour
{
    //Boxes that have been dropped
    private List<GameObject> boxes = new List<GameObject>();

    //Access to scroll the camera
    public CameraScript mainCamera;

    //The canvas corresponding to the camera - used for deactivation
    public GameObject mainCameraCanvas;

    //Boolean to make sure that the camera Scroll method is only called once
    private bool scrollBool;

    //Center of mass of the top box in the stack
    private float topBoxCenter;

    //Center of mass of the first box
    private float firstBoxCenter;

    //Keeps track of the number of scrolls
    private int numScrolls;

    //Grass object
    public GameObject grass;

    //The script attached to the end camera
    public EndCameraScript endCamera;

    //The canvas corresponding to the end camera- used for deactivation
    public GameObject endCameraCanvas;

    

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CanRelease", 0);
        scrollBool = true;
        numScrolls = 0;
        mainCamera.gameObject.SetActive(true);
        mainCameraCanvas.gameObject.SetActive(true);
        endCamera.gameObject.SetActive(false);
        endCameraCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.transform.position.y >= 
        mainCamera.gameObject.transform.position.y) && scrollBool) {
            mainCamera.Scroll();
            scrollBool = false;
            numScrolls = numScrolls + 1;
            addGrass();
        }

        if (boxes.Count != 0) {
            GameObject topBox = boxes[boxes.Count - 1];
            this.transform.position = new Vector2(0, topBox.gameObject.transform.position.y + .15f);
            topBoxCenter = topBox.gameObject.transform.position.x;
        }

        if ((topBoxCenter < firstBoxCenter - 1) || (topBoxCenter > firstBoxCenter + 1)) {
            gameOver();
        }
    }

    //Adds the box to boxes once it makes contact with the collector
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Box") && !boxes.Contains(other.gameObject)) {
            if (boxes.Count == 0) {
                firstBoxCenter = other.gameObject.transform.position.x;
            }
            boxes.Add(other.gameObject);
            PlayerPrefs.SetInt("CanRelease", 0);
            scrollBool = true;
        }
    }

    //Extends the grass on the ground to either side so that there is always enough ground
    //under the tower as it grows
    void addGrass() {
        GameObject newGrass1 = Instantiate(grass);
        newGrass1.gameObject.transform.position = new Vector3(6 * numScrolls, -4.65f, 0);
        GameObject newGrass2 = Instantiate(grass);
        newGrass2.gameObject.transform.position = new Vector3(-6 * numScrolls, -4.65f, 0);
    }


    //Ends the game - the player has lost
    public void gameOver() {
        mainCamera.gameObject.SetActive(false);
        mainCameraCanvas.gameObject.SetActive(false);
        endCamera.gameObject.SetActive(true);
        endCameraCanvas.gameObject.SetActive(true);
        endCamera.incSize(this.numScrolls);
    }
}

