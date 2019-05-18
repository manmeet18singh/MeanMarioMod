using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{   
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;
           s.source.volume = s.volume;
           s.source.loop = s.loop;
        }
    }


    void Start () {
        Play("Theme");
    }

    // Find sounds from Sound list and play
    public void Play (string name){
       Sound s = Array.Find(sounds, sound => sound.name == name);
       s.source.Play();
    }
}
