using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//maintain music between menus by not destroying the item when the scenes are switched
public class KeepMusic : MonoBehaviour
{
    private AudioSource audio;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audio = gameObject.GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        if (audio.isPlaying)
        {
            return;
        }
        else
        {
            audio.Play();
        }
      
    }

    //making sure audio can be stopped between scenes if necessary
    public void StopAudio()
    {
        audio.Stop();
    }

}
