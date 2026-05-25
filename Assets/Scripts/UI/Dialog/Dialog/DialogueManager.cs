using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dialoguePanel;
    public Image portraitImage;
    public TMP_Text dialogueText;

    [Header("Ustawienia")]
    public float typingSpeed = 0.03f;
    public float autoNextDelay = 3f;

    private Coroutine autoNextCoroutine;

    private DialogueLine[] lines;
    private int index;
    private bool isTyping;
    private Coroutine typingCoroutine;

    public bool IsDialogueOpen { get; private set; }

    public void StartDialogue(DialogueLine[] newLines)
    {
        GlobalEvents.RaiseOnMovementOff();
        lines = newLines;
        index = 0;

        dialoguePanel.SetActive(true);
        IsDialogueOpen = true;

        ShowLine();
    }

    void Update()
    {
        if (!IsDialogueOpen) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopCoroutine(typingCoroutine);
                dialogueText.text = lines[index].text;
                isTyping = false;
            }
            else
            {
                index++;

                if (index >= lines.Length)
                    EndDialogue();
                else
                    ShowLine();
            }
        }
    }


    void ShowLine()
    {
        portraitImage.sprite = lines[index].portrait;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(lines[index].text));

        if (autoNextCoroutine != null)
            StopCoroutine(autoNextCoroutine);

        autoNextCoroutine = StartCoroutine(AutoNext());
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    IEnumerator AutoNext()
    {
        yield return new WaitForSeconds(autoNextDelay);

        if (!isTyping)
        {
            index++;

            if (index >= lines.Length)
                EndDialogue();
            else
                ShowLine();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        IsDialogueOpen = false;
        GlobalEvents.RaiseOnMovementOn();
    }
}

[System.Serializable]
public class DialogueLine
{
    public Sprite portrait;

    [TextArea(2, 5)]
    public string text;
}