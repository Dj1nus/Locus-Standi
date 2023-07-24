using System;
using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private SoundPool[] _sounds;

    private void Awake()
    {
        foreach (SoundPool soundPool in _sounds)
        {
            soundPool.source = gameObject.AddComponent<AudioSource>();
            soundPool.source.clip = soundPool.sounds[0].clip;
            soundPool.source.volume = soundPool.sounds[0].volume;
            soundPool.source.pitch = soundPool.sounds[0].pitch;
            soundPool.source.spatialBlend = soundPool.sounds[0].space;
            soundPool.source.outputAudioMixerGroup = soundPool.group;
        }
    }

    public void Play(string name, float pitch = 1)
    {
        SoundPool sound = Array.Find(_sounds, item => item.name == name);
        sound.source.pitch = pitch;
        sound.source.clip = sound.sounds[UnityEngine.Random.Range(0, sound.sounds.Length)].clip;
        sound.source.Play();
    }

    public float GetSoundDuration(string name)
    {
        return Array.Find(_sounds, item => item.name == name).source.clip.length;
    }
}
