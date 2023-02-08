using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//void code, only used in testing, can be removed

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] InfoRetainer retainer;
    [SerializeField] int LevelCompleted;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(LevelCompleted > retainer.levelCompleted)
            {
                retainer.levelCompleted = LevelCompleted;
            }           
            SceneManager.LoadScene("LevelSelect");
        }
 
    }
}
