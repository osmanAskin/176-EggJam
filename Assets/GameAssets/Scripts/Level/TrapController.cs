using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject tilemapPiece;
    
    [SerializeField] private float moveAmountX;
    [SerializeField] private float moveAmountY;
    [SerializeField] private float duration;
    
    private bool isTrigger = false;
    
    private void OnTriggerEnter2D(Collider2D col)
        {
            if (isTrigger != true)
            {
                if (col.gameObject.CompareTag("Player"))
                {
                    isTrigger = true; 
                    tilemapPiece.transform.DOMoveX(moveAmountX, duration).SetEase(Ease.InOutQuad);
                    Debug.Log("TrapTrigger");
                }
    
            }
        }

}
