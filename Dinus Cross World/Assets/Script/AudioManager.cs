using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource walkSource;
    public AudioSource rainSource;

    [Header("Daftar BGM")]
    public AudioClip[] bgmList;

    [Header("Daftar SFX")]
    public AudioClip[] sfxClips;

    [Header("Audio Jalan Kaki")]
    public AudioClip walkClip;

    [Header("Audio Hujan")]
    public AudioClip[] rainClips;

    [Header("Audio Typing")]
    public AudioClip typingClip;

    private Coroutine walkFadeCoroutine;

    private int currentBGMIndex = -1;
    private int currentRainIndex = -1;


    private void Awake()
    {
        // Singleton pattern
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

    // Memainkan BGM dari index tertentu
    public void PlayBGM(int index)
    {
        if (currentBGMIndex == index && bgmSource.isPlaying)
            return;

        if (bgmList.Length > index && bgmList[index] != null)
        {
            bgmSource.clip = bgmList[index];
            bgmSource.loop = true;
            bgmSource.Play();
            currentBGMIndex = index;
        }
    }

    // Menghentikan BGM
    public void StopBGM()
    {
        if (bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
    }

    // Memainkan sound effect dari index tertentu
    public void PlaySFX(int index)
    {
        if (sfxClips.Length > index && sfxClips[index] != null)
        {
            sfxSource.PlayOneShot(sfxClips[index]);
        }
    }
    public void StopSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }
    // Pause / Unpause BGM
    public void ToggleBGM()
    {
        if (bgmSource.isPlaying)
            bgmSource.Pause();
        else
            bgmSource.UnPause();
    }
    public void PlayWalkSound()
    {
        if (walkClip != null && !walkSource.isPlaying)
        {
            walkSource.clip = walkClip;
            walkSource.loop = true;
            walkSource.volume = 0f; // mulai dari volume 0
            walkSource.Play();

            // Mulai fade in
            if (walkFadeCoroutine != null)
            {
                StopCoroutine(walkFadeCoroutine);
            }
            walkFadeCoroutine = StartCoroutine(FadeInWalkSound(0.5f)); // durasi 0.5 detik
        }
    }

    private IEnumerator FadeInWalkSound(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            walkSource.volume = Mathf.Lerp(0f, 1f, time / duration);
            yield return null;
        }
        walkSource.volume = 1f;
    }
    public bool IsWalkSoundPlaying()
    {
        return walkSource.isPlaying;
    }
    // Menghentikan audio langkah
    public void StopWalkSound()
    {
        if (walkSource.isPlaying)
        {
            walkSource.Stop();
        }
    }
    public void PlayRainSound(int index)
    {
        if (currentRainIndex == index && rainSource.isPlaying)
            return;

        if (rainClips.Length > index && rainClips[index] != null)
        {
            rainSource.clip = rainClips[index];
            rainSource.loop = true;
            rainSource.Play();
            currentRainIndex = index;
        }
    }
    public void StopRainSound()
    {
        if (rainSource.isPlaying)
        {
            rainSource.Stop();
        }
    }

    public void PlayTypingSound()
    {
        if (typingClip != null)
        {
            sfxSource.PlayOneShot(typingClip);
        }
    }
    public void PauseAllAudio()
    {
        if (bgmSource.isPlaying) bgmSource.Pause();
        if (rainSource.isPlaying) rainSource.Pause();
    }

    public void ResumeAllAudio()
    {
        if (bgmSource.clip != null) bgmSource.UnPause();
        if (rainSource.clip != null) rainSource.UnPause();
    }
    public int GetCurrentBGMIndex()
    {
        return currentBGMIndex;
    }


}

