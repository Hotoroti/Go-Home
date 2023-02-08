using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
 public void SwitchToMainMenu()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<KeepMusic>().StopAudio();
        SceneManager.LoadScene("MainMenu");        
    }
}
