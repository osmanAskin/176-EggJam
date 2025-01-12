using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    
    private static BackgroundMusic instance;
    private AudioSource _audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Eğer başka bir MusicManager varsa bu objeyi yok et
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
    }
}
