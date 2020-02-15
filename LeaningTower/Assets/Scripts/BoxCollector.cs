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

    //Represents the score of the game
    private int score;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CanRelease", 0);
        scrollBool = true;
        numScrolls = 0;
        gameOverBool = false;
        StartCoroutine(this.spawnClouds());
        score = 0;
        PlayerPrefs.SetInt("score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Checks to see if the stack of boxes is above the halfway mark of the screen.
        //If it is, then the camera scrolls
        if ((this.transform.position.y >= 
        mainCamera.gameObject.transform.position.y) && scrollBool) {
            mainCamera.Scroll();
            scrollBool = false;
            numScrolls = numScrolls + 1;
            addGrass();
        }

        //Grabs the top box of the stack of boxes
        if (boxes.Count != 0) {
            GameObject topBox = boxes[boxes.Count - 1];
            this.transform.position = new Vector2(0, topBox.gameObject.transform.position.y + .15f);
            topBoxCenter = topBox.gameObject.transform.position.x;
        }

        //Checks to see if the tower is off balance
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
            //Adds the box to the stack for center of mass measurement
            boxes.Add(other.gameObject);
            //Allows the pipe to release another box
            PlayerPrefs.SetInt("CanRelease", 0);
            scrollBool = true;
            //Increments the score
            score = score + 1;
            PlayerPrefs.SetInt("score", score);
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
        if(mainCamera.isScrolling()) {
            return;
        }
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

    //Spawns clouds periodically from time to time
    IEnumerator spawnClouds() {
        if (gameOverBool) {
            
        } else {
            float rand = Random.Range(2.0f, 6.0f);
            yield return new WaitForSecondsRealtime(rand);
            this.spawnCloud();
            StartCoroutine(this.spawnClouds());
        }
    }
}

