using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup mixer;
    public static AudioManager instance;
    void Awake()
    {
        // prevent to AudioManagers from existing at the same time
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // prevent the sound to get cut off
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = mixer;
        }
    }
    // method to call a sound
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("The name of the sound in the script is not equal to the name of the sound in the inspector of the audio manager");
            return;
        }
        s.source.Play();
        //to call a sound: AudioManager.FindObjectOfType<AudioManager>().Play("Name of Sound");
    }
}
