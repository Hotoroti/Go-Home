using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchMenu : MonoBehaviour
{
    //The destinations for the buttons to switch to
    [SerializeField] GameObject[] uiDestinations;
    [SerializeField] string[] sceneDestination;

    private void Start()
    {
        //All UI is turned of and the main title screen's UI gets turned back on
        for (int i = 0; i < uiDestinations.Length; i++)
        {
            uiDestinations[i].SetActive(false);
        } 
        uiDestinations[0].SetActive(true);
    }

    public void SwitchUI(int destination)
    {
        //All UI is turned of and the given destination's UI gets turned back on
        for (int i = 0; i < uiDestinations.Length; i++)
        {
            uiDestinations[i].SetActive(false);
        }
        uiDestinations[destination].SetActive(true);
    }
    
    public void EnterLevel(int destination)
    {
        //Load the scene based on the given destination
        SceneManager.LoadScene(sceneDestination[destination]);
    }

    public void EnterLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
