using System.Collections;
using UnityEngine;

public class DeathAudioPlayer : MonoBehaviour
{
    private AudioSource _player;

    public void Init(DeathSound sound)
    {
        _player = GetComponent<AudioSource>();

        _player.clip = sound.Clip;
        _player.volume = sound.Volume;
        _player.pitch = sound.Pitch;
        _player.spatialBlend = sound.Space;

        _player.Play();

        StartCoroutine(LifeTimer(sound.LifeTime));
    }

    private IEnumerator LifeTimer(float duration)
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
