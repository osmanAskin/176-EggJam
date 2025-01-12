using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _audioMananger;
    
    public Sound[] sounds;

    //[SerializeField] AudioSource musicSource;
    //[SerializeField] AudioSource SFXSource;
    private void Awake()
    {
        if (!_audioMananger)
        {
            _audioMananger = this;
        }

        else
        {
            Destroy(this);
        }
        
        foreach (var sound in sounds) 
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void Play(SoundType soundType) 
    {
        var s = Array.Find(sounds , sound => sound.type == soundType);
        s.source.Play();
    }
}

public enum SoundType
{
    PlayerJump,
    PlayerDead,
    Door,
    Key,
    Controller,
    Dash,
    Dialogue
}