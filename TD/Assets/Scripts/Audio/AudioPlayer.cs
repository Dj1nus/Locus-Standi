using System;
using UnityEngine;

public enum SoundTypes
{
    Hit,
    Die,
    Init,
    Attack,
    Explode,
    Active,
    UnActive,
    Click,
    Buy,
    Enter,
    Exit
}

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private SoundPool[] _sounds;

    private void Awake()
    {
        foreach(var soundPool in _sounds)
        {
            soundPool.source = gameObject.AddComponent<AudioSource>();
            soundPool.Init();
        }
    }

    public void Play(SoundTypes name, float pitch = 1)
    {
        SoundPool sound = Array.Find(_sounds, item => item.name == name);

        if (sound != null)
        {
            sound.source.pitch = pitch;
            sound.source.clip = sound.sounds[UnityEngine.Random.Range(0, sound.sounds.Length)].clip;
            sound.source.Play();
        }
    }

    public float GetSoundDuration(SoundTypes name)
    {
        return Array.Find(_sounds, item => item.name == name).source.clip.length;
    }
}
