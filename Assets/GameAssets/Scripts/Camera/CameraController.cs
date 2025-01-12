using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam1;
    [SerializeField] private CinemachineVirtualCamera vcam2;
    [SerializeField] private CinemachineVirtualCamera vcam3;
    [SerializeField] private CinemachineVirtualCamera vcam4;
    [SerializeField] private CinemachineVirtualCamera vcamPlayer;
    
    private static bool hasCamUsed = false;

    private void Start()
    {
        if (!hasCamUsed)
        {
            StartCoroutine(TransitionCam());
        }
        
    }

    IEnumerator TransitionCam()
    {
        PlayerMovement.Instance.isAlive = false;
        ChangeVirtualCam1();
        yield return new WaitForSecondsRealtime(2f);
        ChangeVirtualCam2();
        yield return new WaitForSecondsRealtime(2f);
        ChangeVirtualCam3();
        yield return new WaitForSecondsRealtime(2f);
        ChangeVirtualCam4();
        yield return new WaitForSecondsRealtime(2f);
        ChangeVirtualCamPlayer();
        yield return new WaitForSecondsRealtime(2f);
        PlayerMovement.Instance.isAlive = true;
        
        hasCamUsed = true;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            hasCamUsed = false;
        }
    }

    private void ChangeVirtualCam1()
    {
        vcam1.Priority = 11;
        vcam2.Priority = 10;
        vcam3.Priority = 10;
        vcam4.Priority = 10;
        vcamPlayer.Priority = 10;
    }
    
    private void ChangeVirtualCam2()
    {
        vcam1.Priority = 10;
        vcam2.Priority = 11;
        vcam3.Priority = 10;
        vcam4.Priority = 10;
        vcamPlayer.Priority = 10;
    }
    
    private void ChangeVirtualCam3()
    {
        vcam1.Priority = 10;
        vcam2.Priority = 10;
        vcam3.Priority = 11;
        vcam4.Priority = 10;
        vcamPlayer.Priority = 10;
    }
    
    private void ChangeVirtualCam4()
    {
        vcam1.Priority = 10;
        vcam2.Priority = 10;
        vcam3.Priority = 10;
        vcam4.Priority = 11;
        vcamPlayer.Priority = 10;
    }
    
    private void ChangeVirtualCamPlayer()
    {
        vcam1.Priority = 10;
        vcam2.Priority = 10;
        vcam3.Priority = 10;
        vcam4.Priority = 10;
        vcamPlayer.Priority = 11;
    }
    
    
    
}
