using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool hasPlayerKey = false;
    public bool worked = false;
    public Animator transition;

    private RotateLevel _rotateLevel;
    
    private void Start()
    {
        _rotateLevel = FindObjectOfType<RotateLevel>();
    }

    public void RotateLevel()
    {
        _rotateLevel.RotateScreen();
    }

    public void PlayerTouchedKey()
    {
        hasPlayerKey = true;
        Debug.Log("key!");
    }

    public bool HasPlayerTouchedKey()
    {
        return hasPlayerKey;
    }

    public void NextLevel()
    {
        if (hasPlayerKey)
        {
            StartCoroutine(LoadNextLevelWithDelay(.5f)); 
        }
        else
        {
            Debug.Log("Once anahtari al");
        }
    }
    
    public void RespawnPlayer()
    {
        StartCoroutine(RespawnPositionPlayerWithDelay(.5f));
    }

    private IEnumerator LoadNextLevelWithDelay(float delay)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator RespawnPositionPlayerWithDelay(float delay)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}