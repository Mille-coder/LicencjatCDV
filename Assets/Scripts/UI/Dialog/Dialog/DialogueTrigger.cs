using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogi NPC")]
    public DialogueLine[] dialogueLines;

    [Header("Manager")]
    public DialogueManager dialogueManager;

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
        // czekamy a� dialog si� sko�czy
        while (dialogueManager.IsDialogueOpen)
        {
            yield return null;
        }

        // wy��cza ca�y obiekt DialogCollider
        gameObject.SetActive(false);
    }
}