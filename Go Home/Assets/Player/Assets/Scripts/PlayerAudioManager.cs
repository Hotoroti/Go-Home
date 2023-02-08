using UnityEngine.Audio;
using System;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static PlayerAudioManager instance;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Give all sounds in the array the the right information
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // Play a sound clip
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    // Stop a sound clip
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }
}
