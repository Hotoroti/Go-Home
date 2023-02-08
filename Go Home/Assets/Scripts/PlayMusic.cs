using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject music;
    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<KeepMusic>().PlayAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
