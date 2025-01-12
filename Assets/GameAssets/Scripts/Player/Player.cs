using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get;private set; }

    public Transform playerPosition;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);//farklı bri sahne yüklendiğinde objeyi yine bulmasını sağlar
    }
}
