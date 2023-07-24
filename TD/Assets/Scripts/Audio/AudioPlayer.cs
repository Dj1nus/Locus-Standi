using System;
using System.Collections;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;

    private void Awake()
    {
        foreach (Sound sound in _sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.spatialBlend = sound.space;
            sound.source.outputAudioMixerGroup = sound.group;
        }
    }

    public void Play(string name, float pitch = 1)
    {
        Sound sound = Array.Find(_sounds, item => item.name == name);
        sound.source.pitch = pitch;
        sound.source.Play();
    }

    IEnumerator PlaySoundRoutine(string name)
    {
        Sound sound = Array.Find(_sounds, item => item.name == name);
        yield return new WaitForSeconds(sound.clip.length);
    }
}
