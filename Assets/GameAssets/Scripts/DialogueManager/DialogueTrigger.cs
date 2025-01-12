using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}
 
[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}
 
[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
 
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManage dialogueManage;
    //private AudioManager _audioManager;
    
    private void Start()
    {
        dialogueManage = FindObjectOfType<DialogueManage>();
      //  _audioManager = FindObjectOfType<AudioManager>();
    }
    
    public void TriggerDialogue()
    {
        DialogueManage.Instance.StartDialogue(dialogue);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !DialogueManage.Instance.isDialogueActive)
        {
            TriggerDialogue(); 
            Debug.Log("Dialogue Triggered");
            //_audioManager.Play(SoundType.EmreBaysal);
        }
        
    }
}