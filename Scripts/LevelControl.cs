using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour {


    void Update()
    {
        ResetLevel();
        CloseGame();
      
    }

    //Resets the current level/scene.
    void ResetLevel()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    //This function allows the player to exit the game
    void CloseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
