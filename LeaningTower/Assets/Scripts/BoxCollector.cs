using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollector : MonoBehaviour
{
    //Boxes that have been dropped
    private List<GameObject> boxes = new List<GameObject>();

    //Access to scroll the camera and resize it once the game is over
    public CameraScript mainCamera;

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

    //Boolean to make sure that the gameOver method is only called once
    private bool gameOverBool;

    //Array of clouds that can be spawned
    public GameObject[] clouds;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CanRelease", 0);
        scrollBool = true;
        numScrolls = 0;
        gameOverBool = false;
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

        if ((topBoxCenter < firstBoxCenter - 1) || (topBoxCenter > firstBoxCenter + 1) && !gameOverBool) {
            gameOver();
            gameOverBool = true;
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
        mainCamera.GameEndView(this.numScrolls);
    }

    //Spawns a cloud
    void spawnCloud() {
        float rand = Random.Range(0.0f, 1.0f);
        GameObject cloud = Instantiate(clouds[Random.Range(0, clouds.Length)]);
        if (rand > 0.5f) {
            if (numScrolls == 0) {
                cloud.gameObject.transform.position = new Vector3(-4, Random.Range(1.0f, 4.0f), 0);
            } else {
                cloud.gameObject.transform.position = new Vector3(-4, 
                Random.Range((numScrolls - 1) * 4, (numScrolls + 1) * 4), 0);
            }
        } else { // rand <= 0.5f
            if (numScrolls == 0) {
                cloud.gameObject.transform.position = new Vector3(4, Random.Range(1.0f, 4.0f), 0);
            } else {
                cloud.gameObject.transform.position = new Vector3(4, 
                Random.Range(((numScrolls - 1) * 4), ((numScrolls + 1) * 4)), 0);
            }
        }
    }
}

