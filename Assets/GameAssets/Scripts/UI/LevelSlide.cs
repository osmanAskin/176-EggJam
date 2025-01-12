using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSlide : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float textPosBegin;
    [SerializeField] private float boundaryText;

    public RectTransform GoRectTransform;
    [SerializeField] private TextMeshProUGUI mainText;

    [SerializeField] private bool isLooping;

    public static LevelSlide instance;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;
    }

    public void StartRoutine()
    {
        StartCoroutine(AutoScroolText());
    }

    IEnumerator AutoScroolText()
    {
        while (GoRectTransform.localPosition.y < boundaryText)
        {
            GoRectTransform.Translate((Vector3.up * speed) * Time.deltaTime);
            yield return null;
        }
    }
}