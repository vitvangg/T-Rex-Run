using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // luu audio source
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;

    // luu audio clip
    public AudioClip musicClip;
    public AudioClip deadClip;
    public AudioClip jumpClip;
    public AudioClip itemClip;

    void Start()
    {
        musicAudioSource.clip = musicClip;
    }
    public void PlaySFX(AudioClip sfxClip)
    {
        vfxAudioSource.clip = sfxClip;
        vfxAudioSource.Play();
    }
    public void PlayMusic(AudioClip musicClip)
    {
        musicAudioSource.clip = musicClip;
        musicAudioSource.loop = true;
        musicAudioSource.Play();
    }
    public void PlayJump(AudioClip jumpClip)
    {
        vfxAudioSource.clip = jumpClip;
        vfxAudioSource.Play();
    }
    public void PlayItem(AudioClip itemClip)
    {
        vfxAudioSource.clip = itemClip;
        vfxAudioSource.Play();
    }
}
