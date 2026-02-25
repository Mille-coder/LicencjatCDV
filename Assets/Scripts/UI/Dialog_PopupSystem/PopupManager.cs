using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    [Header("Popup UI")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TMP_Text popupText;
    [SerializeField] private Image portraitImage;

    [Header("Typing Settings")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Auto Hide")]
    [SerializeField] private float autoHideDelay = 3f;

    [Header("Popups")]
    [SerializeField] private List<PopupData> popups = new List<PopupData>();

    private Dictionary<Collider, PopupData> popupLookup;
    private HashSet<Collider> shownPopups = new HashSet<Collider>();

    private Coroutine typingCoroutine;
    private Coroutine autoHideCoroutine;

    private void Awake()
    {
        popupLookup = new Dictionary<Collider, PopupData>();

        foreach (var popup in popups)
        {
            if (popup.triggerCollider == null)
                continue;

            if (!popupLookup.ContainsKey(popup.triggerCollider))
                popupLookup.Add(popup.triggerCollider, popup);
        }

        popupPanel.SetActive(false);
    }

    public void ShowPopup(Collider trigger)
    {
        // pokaż tylko raz
        if (shownPopups.Contains(trigger))
            return;

        if (!popupLookup.ContainsKey(trigger))
            return;

        shownPopups.Add(trigger);

        var data = popupLookup[trigger];

        popupPanel.SetActive(true);

        GlobalEvents.RaiseOnMovementOff();

        // reset coroutine
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        if (autoHideCoroutine != null)
            StopCoroutine(autoHideCoroutine);

        typingCoroutine = StartCoroutine(TypeText(data.text));

        if (portraitImage != null)
        {
            portraitImage.sprite = data.portrait;
            portraitImage.gameObject.SetActive(data.portrait != null);
        }
    }

    private IEnumerator TypeText(string fullText)
    {
        popupText.text = "";

        foreach (char c in fullText)
        {
            popupText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

       
        GlobalEvents.RaiseOnMovementOn();

        autoHideCoroutine = StartCoroutine(AutoHide());
    }

    private IEnumerator AutoHide()
    {
        yield return new WaitForSeconds(autoHideDelay);
        popupPanel.SetActive(false);
    }

    [Serializable]
    public class PopupData
    {
        public Collider triggerCollider;

        [TextArea(3, 6)]
        public string text;

        public Sprite portrait;
    }
}