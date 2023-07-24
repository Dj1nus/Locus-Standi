using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{


    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;

    [Range(0f, 3f)]
    public float pitch = 1;

    [Range(0f, 1f)]
    public float space = 1; 

}
