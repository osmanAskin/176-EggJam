using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class FinalScenario : MonoBehaviour
{
    [SerializeField] private Tilemap fallingGrid; 
    public int a;
    
       private void Start()
       
       {
          StartCoroutine(Scenario());
       }
    
       IEnumerator Scenario()
       {  
          PlayerMovement.Instance.isAlive = false;
          transform.DOMoveX(-10f, 3f);
          yield return new WaitForSeconds(19f);
          PlayerMovement.Instance.isAlive = true;
          
          yield return new WaitForSeconds(2f);
          TrapTrigger.Instance.Move();
          
          PlayerMovement.Instance.gfx.SetActive(false);
          PlayerExplosion.instance.PlayerDeadExplode();
          
          yield return new WaitForSeconds(3f);
          SceneManager.LoadScene(0);
       }
    
       IEnumerator FallingGrid()
       {
          fallingGrid.transform.DOMoveY(-10f, .5f);
          yield return null;
       }
       
       
}
