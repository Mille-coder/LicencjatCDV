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

    [Header("Popups")]
    [SerializeField] private List<PopupData> popups = new List<PopupData>();

    private Dictionary<Collider, PopupData> popupLookup;
    private HashSet<Collider> shownPopups = new HashSet<Collider>();

    private Coroutine typingCoroutine;

    private bool isPopupOpen;
    private bool isTyping;
    private string currentFullText;

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

        if (popupPanel != null)
            popupPanel.SetActive(false);
    }

    private void Update()
    {
        if (!isPopupOpen)
            return;

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            if (isTyping)
            {
                if (typingCoroutine != null)
                    StopCoroutine(typingCoroutine);

                popupText.text = currentFullText;
                isTyping = false;

                GlobalEvents.RaiseOnMovementOn();
            }
            else
            {
                HidePopup();
            }
        }
    }

    public void ShowPopup(Collider trigger)
    {
        if (shownPopups.Contains(trigger))
            return;

        if (!popupLookup.ContainsKey(trigger))
            return;

        shownPopups.Add(trigger);

        var data = popupLookup[trigger];

        popupPanel.SetActive(true);
        isPopupOpen = true;

        GlobalEvents.RaiseOnMovementOff();

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        currentFullText = data.text;
        typingCoroutine = StartCoroutine(TypeText(currentFullText));

        if (portraitImage != null)
        {
            portraitImage.sprite = data.portrait;
            portraitImage.gameObject.SetActive(data.portrait != null);
        }
    }

    private IEnumerator TypeText(string fullText)
    {
        isTyping = true;
        popupText.text = "";

        foreach (char c in fullText)
        {
            popupText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

        GlobalEvents.RaiseOnMovementOn();
    }

    private void HidePopup()
    {
        popupPanel.SetActive(false);
        isPopupOpen = false;
        isTyping = false;
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