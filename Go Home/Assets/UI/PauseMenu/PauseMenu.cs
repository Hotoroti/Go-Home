using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool paused;
    [SerializeField] private GameObject menu;

    private void Start()
    {
        //Pausemenu becomes inactive so that it wont obscure the player's vision when not neccecairy
        menu.SetActive(false);
    }

    private void Update()
    {
        //When P is pressed pause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
        //Only use the code below when paused
        if (!paused)
        {
            return;
        }
        //When the spacebar is pressed, resume. Otherwise when escape is pressed, go to the title screen
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Play();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }

    //Pause the game and show the pausemenu
    private void Pause()
    {
        paused = true;
        menu.SetActive(true);
        Time.timeScale = 0f;
    }
    //Resume the game and hide the pausemenu
    private void Play()
    {
        paused = false;
        menu.SetActive(false);
        Time.timeScale = 1f;
    }
}
