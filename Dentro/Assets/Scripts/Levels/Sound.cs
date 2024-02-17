using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    [SerializeField]
    public string name;
    [SerializeField]
    public AudioClip clip;
    public bool loop;
    [Range (0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [HideInInspector]
    public AudioSource source;

    // Start is called before the first frame update
    
}
