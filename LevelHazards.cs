using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelHazards : MonoBehaviour {

    public Text gameOver; //Holds the game over text object that will be used when the player dies.
    PlayerDeath playerDeathScript; //Object holding player death script


    void Awake()
    {
        //Reference to player's death script.
        playerDeathScript = FindObjectOfType<PlayerDeath>();   
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) //Turns off the player object and shows explosion on contact
        {
            playerDeathScript.PlayerDeathSequence(); //Begins the player death sequence, explosion.
            other.gameObject.SetActive(false);
            DisplayGameOver();
        }
    }

    //This function enables the game over text and displays a message
    void DisplayGameOver()
    {
        gameOver.enabled = true;
        gameOver.text = "GAME OVER!" + System.Environment.NewLine + "Press Backspace to try again.";
        
    }

}
