using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogi NPC")]
    public DialogueLine[] dialogueLines;

    [Header("Manager")]
    public DialogueManager dialogueManager;

    [Header("Event po zakończeniu dialogu")]
    public UnityEvent onDialogueFinished;

    private bool alreadyPlayed = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (alreadyPlayed) return;

        alreadyPlayed = true;

        dialogueManager.StartDialogue(dialogueLines);

        StartCoroutine(DisableAfterDialogue());
    }

    IEnumerator DisableAfterDialogue()
    {
        
        while (dialogueManager.IsDialogueOpen)
        {
            yield return null;
        }

        onDialogueFinished?.Invoke();

       
        gameObject.SetActive(false);
    }
}