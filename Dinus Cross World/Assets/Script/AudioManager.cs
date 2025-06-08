using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip[] sfxClips;

    private void Awake()
    {
        // Singleton pattern agar AudioManager tidak dobel saat pindah scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        //PlayBGM();
    }

    // Memainkan background music yang di-loop
    public void PlayBGM()
    {
        if (bgmSource != null && backgroundMusic != null)
        {
            bgmSource.clip = backgroundMusic;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    // Memainkan sound effect sekali
    public void PlaySFX(int index)
    {
        if (sfxSource != null && sfxClips.Length > index)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
    }

    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    public void StopAllSFX()
    {
        sfxSource.Stop();
    }
}

