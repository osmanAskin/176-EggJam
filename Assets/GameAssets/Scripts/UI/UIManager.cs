using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    public bool isPaused;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI optionsText;
    public TextMeshProUGUI playText;
    public TextMeshProUGUI creditsText;

    public Transform target; // volume target (gameobject for dotwin)
    public Transform target2; // option target
    public Transform target3; // player target
    public Transform target4; // credits target

    private bool volumeHasMoved;
    private bool optionsHasMoved;
    private bool creditsHasMoved;


    private int nextSceneIndex;

    private void Start()
    {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void StartButton()
    {
        playText.transform.DOMove(target3.transform.position, 0.35f).SetLoops(2, LoopType.Yoyo);
        playText.transform.DORotate(new Vector3(0, 180, 0), 1, RotateMode.WorldAxisAdd);
        StartCoroutine(playEnumerator());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    private void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void VolumeClick()
    {
        if (AudioListener.volume > 0)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;

        if (!volumeHasMoved)
        {
            volumeText.transform.DOMove(target.position, 0.35f).SetLoops(2, LoopType.Yoyo);
            volumeText.transform.DORotate(new Vector3(0, 0, 180), 1, RotateMode.WorldAxisAdd);
            StartCoroutine(volumeEnumerator());
        }

        if (volumeText.fontStyle != FontStyles.Strikethrough)
            volumeText.fontStyle = FontStyles.Strikethrough;
        else
            volumeText.fontStyle = FontStyles.Normal;
    }

    public void OptionsClick()
    {
        if (!optionsHasMoved)
        {
            optionsText.transform.DOMove(target2.position, 0.35f).SetLoops(2, LoopType.Yoyo);
            optionsText.transform.DORotate(new Vector3(0, 0, 180f), 1, RotateMode.WorldAxisAdd);
            StartCoroutine((optionsEnumerator()));
        }
    }

    public void CreditsClick()
    {
        if (!creditsHasMoved)
        {
            LevelSlide.instance.StartRoutine();
            creditsText.transform.DOMove(target4.position, 0.35f).SetLoops(2, LoopType.Yoyo);
            creditsText.transform.DORotate(new Vector3(180, 0, 0), 1, RotateMode.WorldAxisAdd);
            StartCoroutine(creditsEnumerator());    
        }
    }

    IEnumerator volumeEnumerator()
    {
        volumeHasMoved = true;
        yield return new WaitForSeconds(0.9f);
        volumeHasMoved = false;
    }
    

    IEnumerator optionsEnumerator()
    {
        optionsHasMoved = true;
        yield return new WaitForSeconds(0.9f);
        optionsHasMoved = false;
    }

    IEnumerator playEnumerator()
    {
        yield return new WaitForSeconds(0.9f);
        SceneManager.LoadScene(nextSceneIndex);
    }

    IEnumerator creditsEnumerator()
    {
        creditsHasMoved = true;
        yield return new WaitForSeconds(0.9f);
        creditsHasMoved = false;
    }
}