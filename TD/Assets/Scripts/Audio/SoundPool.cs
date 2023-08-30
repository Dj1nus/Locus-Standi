
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundPool
{
    public Sound[] sounds;

    public SoundTypes name;

    public AudioMixerGroup group;

    [HideInInspector]
    public AudioSource source;
    public bool _isFree = true;

    public void Init()
    {  
        source.clip = sounds[0].clip;
        source.volume = sounds[0].volume;
        source.pitch = sounds[0].pitch;
        source.spatialBlend = sounds[0].space;
        source.outputAudioMixerGroup = group;
    }
}
