using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    public string _levelSelect;
    public void LevelSelection()
    {
        SceneManager.LoadScene(_levelSelect);
    }

}
