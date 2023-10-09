using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;

    [Range(0f, 1f)]
    public float Volume;

    [Range(.1f, 3f)]
    public float Pitch;

    public bool Loop;

    public bool PlayOnAwake;

    [HideInInspector]
    public AudioSource Source;
}


public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;
    public static AudioManager Instance;
    public bool Muted;
    
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.playOnAwake = s.PlayOnAwake;
        }
    }

    public void Play(string name)
    {
        Sound s = System.Array.Find(Sounds, sound => sound.Name == name);
        if (s != null)
            s.Source.Play();
    }

    public void ChangeVolumeStatus()
    {
        if (Muted)
        {
            foreach (Sound s in Sounds)
            {
                s.Source.volume = s.Volume;
            }
            Muted = false;
        }
        else
        {
            foreach (Sound s in Sounds)
            {
                s.Source.volume = 0;
            }
            Muted = true;
        }
    }
}
