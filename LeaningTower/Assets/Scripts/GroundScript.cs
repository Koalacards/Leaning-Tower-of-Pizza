using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    //The first box in the stack
    private GameObject firstBox;

    //Access to the box collector to call gameOver
    public BoxCollector bc;


    //If this is the first collision with a box, record it as firstBox
    //If not, the game is over
    void OnTriggerEnter2D(Collider2D other) {
        if (firstBox == null && other.CompareTag("Box")) {
            firstBox = other.gameObject;
        } else if (other.CompareTag("Box")) {
            bc.gameOver();
        }
    }
}
