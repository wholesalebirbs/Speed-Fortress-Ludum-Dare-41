using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    public bool musicOn = true;

    public bool sfxOn = true;

    [Range(0, 1)]
    public float musicVolume;

    [Range(0, 1)]
    public float sfxVolume;

    protected AudioSource _backgroundMusic;
    

    public virtual void PlayBackgroundMusic(AudioSource audio)
    {
        if (!musicOn)
        {
            return;
        }

        if (_backgroundMusic != null)
        {
            _backgroundMusic.Stop();
        }

        _backgroundMusic = audio;
        _backgroundMusic.volume = musicVolume;
        _backgroundMusic.loop = true;
        _backgroundMusic.Play();
    }

    public virtual AudioSource GetBackgroundMusic()
    {
        return _backgroundMusic;
    }

    public AudioSource PlaySound(AudioClip audio, Vector3 location)
    {
        if (!sfxOn)
        {
            return null;
        }

        GameObject tempAudio = new GameObject("Temp Audio");
        tempAudio.transform.position = location;
        AudioSource audioSource = tempAudio.AddComponent<AudioSource>() as AudioSource;
        audioSource.clip = audio;
        audioSource.volume = sfxVolume;
        audioSource.Play();
        Destroy(tempAudio, audio.length);

        //audioSource.loop = true;
        

        return audioSource;
    }
}
