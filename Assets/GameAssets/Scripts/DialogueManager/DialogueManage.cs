using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class DialogueManage : MonoBehaviour
{
    // Singleton
    public static DialogueManage Instance;

    // Dialogue
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

   [SerializeField] private float typingSpeed = 2f; // Harf yazma hızı

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        StartCoroutine(DisplayDialogueLines());
    }

    private IEnumerator DisplayDialogueLines()
    {
        while (lines.Count > 0)
        {
            DialogueLine currentLine = lines.Dequeue();

            // Harf harf yazdır
            yield return StartCoroutine(TypeSentence(currentLine));

            // Diyaloglar arasında kısa bir bekleme süresi eklenebilir
            yield return new WaitForSeconds(2f);
        }

        EndDialogue();
    }

    private IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        dialogueArea.text = ""; // Diyalog tamamlandığında ekran temizlenebilir
    }
}
