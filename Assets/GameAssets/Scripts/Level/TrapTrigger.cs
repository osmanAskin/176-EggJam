using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapTrigger : MonoBehaviour
{ 
    public static TrapTrigger Instance;
    
    [SerializeField] private GameObject trap1;
    [SerializeField] private GameObject trap2;
    [SerializeField] private GameObject trap3;
    [SerializeField] private bool hasTrapped = false;

    private void Awake()
    {
        if (!Instance)Instance = this;
    }

    public void Move()
    {
        var seq = DOTween.Sequence();
            seq.Append(trap1.transform.DOMoveX(-40f,.5f).SetRelative().SetEase(Ease.Linear));
            seq.Append(trap2.transform.DOMoveX(40f,.5f).SetRelative().SetEase(Ease.Linear));
            seq.Append(trap3.transform.DOMoveY(-20f,1f).SetRelative().SetEase(Ease.Linear));
    }
}
 


