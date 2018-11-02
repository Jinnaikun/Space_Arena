using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEffects : MonoBehaviour {

    PlayerController shipPlayerScript; //Script to the player's ship, used to activate buffs
    GameObject [] scoreOrbs; //This will refer to the score orbs and hold all of them.
    public float multiplierMove; //Move speed multiplier
    public float multiplierTurn; //Turn speed multiplier

     void Awake()
    {
        //Filling in references
        shipPlayerScript = FindObjectOfType<PlayerController>();
        scoreOrbs = GameObject.FindGameObjectsWithTag("Score");
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")) //In all scenarios, power up will be deactivated.
        {
            gameObject.SetActive(false);

            if(gameObject.CompareTag("SpeedUp")) //Activate speed ups when a speed up is picked up.
            {
                ActivateSpeedPower();
            }

            if(gameObject.CompareTag("Magnet")) //Activate magnet property when a magnet is picked up
            {
                ActivateMagnetPower();
            }
        }
    }

    //Activates magnet properties for each score Orb so they can move towards the player
    void ActivateMagnetPower()
    {
        shipPlayerScript.MagnetParticles();
        foreach (GameObject scoreOrbScript in scoreOrbs) //Occurs for all score objects
        {
            scoreOrbScript.GetComponent<ScoreMagnetMovement>().enabled = true;
        }
    }

    //Activate speed up properties by sending in multipliers to the player script that will adjust it's movement speed.
    void ActivateSpeedPower()
    {
        shipPlayerScript.SpeedUp(multiplierMove,multiplierTurn);
    }
}
