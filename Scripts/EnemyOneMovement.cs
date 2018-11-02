using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOneMovement : MonoBehaviour {

    public GameObject playerShip; // This is the object the enemy will follow
    public float moveSpeed; //This is how fast the enemy will move

     void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce((playerShip.transform.position - transform.position) * moveSpeed);   
    }
}
