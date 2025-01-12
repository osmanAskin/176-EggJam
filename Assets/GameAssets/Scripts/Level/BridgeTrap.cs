using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTrap : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bridgeRb;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Collision");

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Bridge");
            bridgeRb.bodyType = RigidbodyType2D.Dynamic;
            
        }
    }
}

