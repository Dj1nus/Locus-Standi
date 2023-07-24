
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundPool
{
    public Sound[] sounds;

    public string name;

    public AudioMixerGroup group;

    [HideInInspector]
    public AudioSource source;
    public bool _isFree = true;
}
