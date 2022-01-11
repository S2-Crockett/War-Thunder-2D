using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    [Header("Audio")] 
    public AudioSource effectSource;
    public AudioSource playerSource;

    public void PlayPlayerOneShot(AudioClip clip)
    {
        playerSource.PlayOneShot(clip);
    }
    public void PlayEffectOneShot(AudioClip clip)
    {
        effectSource.PlayOneShot(clip);
    }
}
