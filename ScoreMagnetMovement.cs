using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMagnetMovement : MonoBehaviour {


    GameObject playerShip; // This is the object the orbs will follow
    public float moveSpeed; //This is how fast the orbs will move
    public float timerDuration; //Value of time in seconds, holds how long the buff will last.
    float timer = 0; //Holds the timer

     void Awake()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
    }

     void Start()
    {
        StartTimer();
    }

    void FixedUpdate()
    {
        OrbMovement();
    }

     void Update()
    {
        HandleTimer();
    }

    //On start, the timer for the buff will begin.
    void StartTimer()
    {
        timer = timerDuration;
    }

    //This function makes the orb move towards the player.
    void OrbMovement()
    {
        GetComponent<Rigidbody>().AddForce((playerShip.transform.position - transform.position) * moveSpeed);
    }

    //This function makes the timer tick down towards 0
    void HandleTimer()
    {
        if(timer > 0) //Keep on counting down
        {
            timer -= Time.deltaTime;
        }
        else //When the timer is less than or equal to 0, call StopTimer() and stop the timer
        {
            StopTimer();
        }
    }

    //This function will stop the timer and turn off this script.
    void StopTimer()
    {
        timer = 0;
        gameObject.GetComponent<ScoreMagnetMovement>().enabled = false;
    }
}
