using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject playerShip; //Holds the spaceship game object
    Vector3 offset; //Holds camera offset to follow the player object

     void Start()
    {
        offset = transform.position - playerShip.transform.position; //Takes the difference between the player and camera.
    }

     void LateUpdate()
    {
        CameraUpdate();
    }

    //Camera moves to new position over the object.
    void CameraUpdate()
    {
        transform.position = playerShip.transform.position + offset;  
    }
}
