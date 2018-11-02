using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

    public int remainCount; //Keeps track of the remaining number of orbs to collect
    public Text remainText; //The text object used to display how many orbs are left
    public Text winText; //Holds the text that will display when the player wins
    public float teleportPosition; //Where the player teleports to




    void OnTriggerExit(Collider other)
    {

        //Displays the text the first time the player crosses the line and turns the object into a collider
        if (other.gameObject.CompareTag("StartCollect"))
        {
            if (remainText.enabled == false)
            {
                remainText.enabled = true;
                ChangeDisplayText();
                other.isTrigger = false;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, teleportPosition);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Keeps track of how close the player is to their objective
         if (other.gameObject.CompareTag("Score"))
        {
            other.gameObject.SetActive(false);
            remainCount -= 1;
            ChangeDisplayText();
            
            //If the player wins, turn off their object to prevent the game over and win screen from popping up at the same time.
            if(remainCount == 0)
            {
              gameObject.SetActive(false);
            }
        }

    }

    //This function controsl the changes to the text that display on the screen that interact with the player's score.
    void ChangeDisplayText()
    {
        remainText.text = "Remaining Orbs: " + remainCount;

        if(remainCount <= 0)
        {
            winText.enabled = true;
        }
    }
}
