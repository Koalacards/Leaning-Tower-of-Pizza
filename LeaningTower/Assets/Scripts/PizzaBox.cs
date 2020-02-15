using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBox : MonoBehaviour
{
    //List of box sprites to choose from
    public List<Sprite> boxSprites = new List<Sprite>();
    //box sprite renderer
    private SpriteRenderer sprender;




    // Start is called before the first frame update
    void Start()
    {
        //Grabs the SpriteRenderer component from this box
        sprender = this.gameObject.GetComponent<SpriteRenderer>();
        //Randomly selects one of the sprites in BoxSprites to be this box's sprite
        sprender.sprite = boxSprites[Random.Range(0, boxSprites.Count)];


    }

    //Adds a sprite to the list of sprites
    //Will be called when the player buys a sprite in the store
    public void addSprite(Sprite toAdd) {
        boxSprites.Add(toAdd);
    }

}    
