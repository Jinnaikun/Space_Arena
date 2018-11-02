using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {

    private float timer = 0; //This is the timer for the player buffs
    public float timeDurration; //This is the time durration for the buffs

    GameObject playerShip; //Player's ship objectn
    ParticleSystem.EmissionModule playerCorpseEmission; //The particles from the explosion component access

    void Awake()
    {
        //reference to player objects
        playerShip = GameObject.FindGameObjectWithTag("Player");
        playerCorpseEmission = gameObject.GetComponent<ParticleSystem>().emission;

    }

    void Start()
    { 
        playerCorpseEmission.enabled = false; //Particles are off on start.
    }

    void Update()
    {
        HandleTimer();
    }


    //Moves the corpse object to the player object
    public void PlayerDeathSequence()
    {
        gameObject.transform.position = playerShip.transform.position; //Move corpse to playerShip
        playerCorpseEmission.enabled = true;
        StartTimer(); //Start the timer
    }

    //Starts counting how long the particles should stay
    void StartTimer()
    {
        timer = timeDurration;

    }

    //Ticks the explosion timer down
    void HandleTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            StopTimer();
        }
    }

    //Stops the explosion
    void StopTimer()
    {
        timer = 0;
        playerCorpseEmission.enabled = false;
 
    }

}
