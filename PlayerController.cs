using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    private bool isTranslating; //Confirms on key hold that movement is occuring
    private bool isRotating; //Confirms on key hold that rotations are occuring

    private float moveSpeed; //Movement speed
    public float baseSpeed; //Player's original movement speed, values can be changed in inspector
    private float turnSpeed;//Player's turning speed
    public float baseTurnSpeed; //Player's original turning speed

    public Color colorTrailStart; //Color of the trail at start.
    public Color colorTrail; //Color of the trail over time.
    private bool isSpeedPowerUpOn = false; //Indicator of the power up being on;
    private bool isRunningOut = false;//Indicator of the power up almost out;
    private float timer = 0; //This is the timer for the player buffs
    public float timeDurration; //This is the time durration for the buffs
    public float timeAlmostUp; //This is the indicator for when the power up is about to run out

    Rigidbody shipRigidBody; //Empty container for the rigidbody component
    ParticleSystem.EmissionModule shipParticleSystem; //Empty container for the particle system component
    TrailRenderer shipTrailRender; //Empty container for the ship's speed trails
     

    void Awake()
    {
        shipRigidBody = GetComponent<Rigidbody>();
        shipParticleSystem = GetComponent<ParticleSystem>().emission;
        shipTrailRender = GetComponent<TrailRenderer>();
        
    }

    void Start()
    {
        moveSpeed = baseSpeed; //Sets the movement speed to the same as the base speed
        turnSpeed = baseTurnSpeed; //Turn speed is now the same as the base turn speed
        shipParticleSystem.enabled = false; //Keep particle system off on spawn

    }

    //Physics check whenever player inputs some type of movement.
    void FixedUpdate()
    {
        if (isTranslating)
        {
            ForcePlayerTranslate();
        }
        if (isRotating)
        {
            ForcePlayerRotate();
        }
	}

    void Update()
    {
        HandleTimer();
        MovePlayer();
        RotatePlayer();

        if(timer < timeAlmostUp && isSpeedPowerUpOn) //Checks to see if time's almost up for the speed up.
        {
            isRunningOut = true;
            SpeedUpEffects();
        }


    }

    //-------------PLAYER MOVEMENTS------------------------------
    //On update, check when the player is pressing a movement key.
    void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            isTranslating = true;
        }
        else
        {
            isTranslating = false;
        }
            
    }

    //On update, check when the player is pressing a rotation key.
    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            isRotating = true;
        }
        else
        {
            isRotating = false;
        }
           
    }

    //This function controls the player's movements.
    void ForcePlayerTranslate()
    {
        float moveDistance = Input.GetAxis("Vertical") * moveSpeed;
            shipRigidBody.AddForce(transform.forward * moveDistance );


    }

    //This function controls the player's rotations
    void ForcePlayerRotate()
    {
        float turnDistance = Input.GetAxis("Horizontal") * turnSpeed;  
        shipRigidBody.AddTorque(0,turnDistance,0);
    }

    //-------------------------------PLAYER BUFFS-----------------------------------------------------------
    //Doubles player speed when the speed up is triggered. This function is called from the PowerUpEffects script
    public void SpeedUp(float multiplierMove, float multiplierTurn)
    {
        StartTimer();
        SpeedUpEffects();
        moveSpeed *= multiplierMove;
        turnSpeed *= multiplierTurn;


    }

    //Changes the speed up effects depending on how much time is left.
    void SpeedUpEffects()
    {
        if(!shipTrailRender.enabled) //Only attempts to enable the trail once.
        {
            shipTrailRender.enabled = true;
            isSpeedPowerUpOn = true;
        }

        if (isRunningOut && isSpeedPowerUpOn) //Manages the color whenever another collectable has been picked up and increases the timer.
        {
            shipTrailRender.material.SetColor("_Color", colorTrail);
            isRunningOut = false;
        }

        //Checks to make sure if timer resets, the color goes back to green to notify the player to make sure they know their speed up still works.
        else if (!isRunningOut && isSpeedPowerUpOn) 
        {
            shipTrailRender.material.SetColor("_Color", colorTrailStart);
            isSpeedPowerUpOn = true;
        }
    }

    //When the ship picks up a magnet power up, enable the particle emissions.
    public void MagnetParticles()
    {
        StartTimer();
        shipParticleSystem.enabled = true;
    }


    //------------------------------PLAYER TIMER--------------------------------------------
    //Starts the buff timer, will restart each time a buff is picked up
    //If each buff resets the timer, then the stop timer will reset all values back to normal
    //This starts the timer.
    void StartTimer()
    {
        timer = timeDurration;
        if (isSpeedPowerUpOn) //This mainly works due to there being only one power up
        {
            SpeedUpEffects();
        }
    }

    //Ticks the buff timer down
    void HandleTimer()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            StopTimer();
        }
    }

    //Stops the buff timer and resets values to normal.
    void StopTimer()
    {
        timer = 0;
        moveSpeed = baseSpeed;
        turnSpeed = baseTurnSpeed;
        shipParticleSystem.enabled = false;
        shipTrailRender.enabled = false;
        isSpeedPowerUpOn = false;
    }
 

    
}
