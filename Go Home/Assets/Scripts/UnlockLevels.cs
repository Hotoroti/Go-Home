using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevels : MonoBehaviour
{
    [SerializeField] InfoRetainer retainer;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1) && retainer.levelCompleted < 3)
        {
            retainer.levelCompleted = 3;
            audio.Play();
        }
    }
}
