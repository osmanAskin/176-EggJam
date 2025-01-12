using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressBarManager : MonoBehaviour
{
    [SerializeField] private GameObject progressImage1;
    [SerializeField] private GameObject progressImage2;
    [SerializeField] private GameObject progressImage3;
    [SerializeField] private GameObject progressImage4;

    private void Update()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex - 2;
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (sceneIndex == 0)
        {
            progressImage1.SetActive(true);
        }
        
        else if (sceneIndex == 2 || currentSceneIndex == 5 || currentSceneIndex == 9)
        {
            progressImage1.SetActive(true);
            progressImage2.SetActive(true);
        }
        
        else if (sceneIndex == 3 || currentSceneIndex == 6 || currentSceneIndex == 10)
        {
            progressImage1.SetActive(true);
            progressImage2.SetActive(true);
            progressImage3.SetActive(true);
        }
        
        else if (sceneIndex == 4 || currentSceneIndex == 7 || currentSceneIndex == 11)
        {
            progressImage1.SetActive(true);
            progressImage2.SetActive(true);
            progressImage3.SetActive(true);
            progressImage4.SetActive(true);
        }
        }
    }
