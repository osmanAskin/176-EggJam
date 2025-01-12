using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Tilemaps;

public class DialogueScenario : MonoBehaviour
{
   [SerializeField] private Tilemap fallingGrid; 

   private void Start()
   {
      StartCoroutine(Scenario());
   }

   IEnumerator Scenario()
   {  
      PlayerMovement.Instance.isAlive = false;
      transform.DOMoveX(-10f, 3f);
      yield return new WaitForSeconds(12f);
      PlayerMovement.Instance.isAlive = true;
      
      yield return new WaitForSeconds(1f);
      StartCoroutine(FallingGrid());
      
   }

   IEnumerator FallingGrid()
   {
      fallingGrid.transform.DOMoveY(-10f, .5f);
      yield return null;
   }
}
