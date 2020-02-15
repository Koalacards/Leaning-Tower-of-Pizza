using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonScript : MonoBehaviour
{
    //Sprite renderer for this sprite
    private SpriteRenderer sprender;

    // Start is called before the first frame update
    void Start()
    {
        //Get the sprite renderer for this sprite
        sprender = this.gameObject.GetComponent<SpriteRenderer>();

        //Determines the color for the balloon
        this.DetermineColor();
    }

    //Determines the color for the balloon
    void DetermineColor() {
        /*
        The way the determination works is it first picks a number from 1 to 6
        What the numbers stand for:
        1- Almost all red
        2- Between red and blue
        3- Almost all blue
        4- Between blue and green
        5- Almost all green
        6- Between green and red

        Depending on the number, rgb values will be different.
        For the colors that are prominent, their rgb will be between 200 and 255.
        For the colors that aren't, their rbg will be between 0 and 30.
         */

        int colordeterminer = Random.Range(1, 7);
        int red;
        int green;
        int blue;
        switch(colordeterminer){
            case 1:
                red = Random.Range(200, 256);
                green = Random.Range(0, 31);
                blue = Random.Range(0, 31);
            break;
            case 2:
                red = Random.Range(200, 256);
                green = Random.Range(0, 31);
                blue = Random.Range(200, 256);
            break;
            case 3:
                red = Random.Range(0, 31);
                green = Random.Range(0, 31);
                blue = Random.Range(200, 256);
            break;
            case 4:
                red = Random.Range(0, 31);
                green = Random.Range(200, 256);
                blue = Random.Range(200, 256);
            break;
            case 5:
                red = Random.Range(0, 31);
                green = Random.Range(200, 256);
                blue = Random.Range(0, 31);
            break;
            case 6:
                red = Random.Range(200, 256);
                green = Random.Range(200, 256);
                blue = Random.Range(0, 31);    
            break;
            default:
                throw new System.ArgumentException("The number somehow wasnt an integer from 1 to 6");
        }
        sprender.color = new Color32((byte)red, (byte)green, (byte)blue, 255);
    }
}
