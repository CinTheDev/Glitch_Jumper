using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public float pitchDiff;
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
            s.source.playOnAwake = false;
        }
    }
    // method to call a sound
    public void Play(string name)
    {
        

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError("Name " + name + " does not match any sound.");
            return;
        }
        s.source.Play();
        //to call a sound: AudioManager.FindObjectOfType<AudioManager>().Play("Name of Sound");
    }

    public void UpdateSound()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
}
