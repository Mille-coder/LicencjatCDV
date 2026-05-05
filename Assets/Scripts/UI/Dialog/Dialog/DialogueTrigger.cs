using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Dialogi NPC")]
    public DialogueLine[] dialogueLines;

    [Header("Manager")]
    public DialogueManager dialogueManager;

    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!dialogueManager.IsDialogueOpen)
            {
                dialogueManager.StartDialogue(dialogueLines);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}