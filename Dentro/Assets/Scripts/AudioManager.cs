using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(transform.gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Master")[0];
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlayS(string name)
    {
       Sound s =  Array.Find(sounds, sound => sound.name == name);
       if(s.source == null || s.source.isPlaying){
            return;
       }
        s.source.Play();
    }

    public void Play(string name)
    {
       Sound s =  Array.Find(sounds, sound => sound.name == name);
       if (s.source != null)
            s.source.Play();
    }
    public void Play(string name, float distance)
    {
        
       Sound s =  Array.Find(sounds, sound => sound.name == name);
       if (s.source != null)
            if(distance>1){
                 s.source.volume = 1f/distance;
            }else{
                s.source.volume = 1f;
            }
            s.source.Play();
    }

    public void StopAll(){
        foreach (Sound s in sounds){
            if (s.source != null)
                s.source.Stop();
        }
    }
    public void SetVolume(string name, float distance){
        Sound s =  Array.Find(sounds, sound => sound.name == name);
       if (s.source != null)
            if(distance>1){
                 s.source.volume = 1f/distance;
            }else{
                s.source.volume = 1f;
            }
    }
}
