using UnityEngine;

public class DeathSound
{
    public AudioClip Clip;
    public float Volume;
    public float Pitch;
    public float LifeTime;
    public float Space;

    public DeathSound(AudioClip clip, float volume, float pitch, float space, float lifeTime)
    {
        Clip = clip;
        Volume = volume;
        Pitch = pitch;
        Space = space;
        LifeTime = lifeTime;
    }
}
