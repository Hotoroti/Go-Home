using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    // maintains audio between levels
    void Start()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<KeepMusic>().PlayAudio();
    }

}
