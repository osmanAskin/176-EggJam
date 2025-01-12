using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SetRotationCenter : MonoBehaviour
{
    private Transform setRotationCenter;

    PlayerMovement _playerMovement;

    private void Update()
    {
        CenterRotatinPoint();
    }

    public void CenterRotatinPoint()
    {
        if (setRotationCenter != null)
        {
            setRotationCenter.position = _playerMovement.playerPosition.position;    
        }
        
    }
    
}
