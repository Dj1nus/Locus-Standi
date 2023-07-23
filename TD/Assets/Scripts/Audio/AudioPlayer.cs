using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioClip _shot;

    [SerializeField] private AudioSource _shootAudioSource;


    public void PlayShootSound()
    {
        _shootAudioSource.pitch = Random.Range(0.9f, 1.1f);

        _shootAudioSource.PlayOneShot(_shot);
    }



}
