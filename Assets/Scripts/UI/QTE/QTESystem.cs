using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{
    [Header("QTE UI")]
    [SerializeField] private GameObject QTEPanel;
    [SerializeField] private Image successField;
    [SerializeField] public Image marker;

    [Header("QTE List")]
    [SerializeField] private List<QTEData> QTEs = new List<QTEData>();
    

    [Serializable]
    public class QTEData
    {
        public QTETrigger trigger;

        
        public float difficulty;
        public float speed;
    }

    private HashSet<QTETrigger> wonQTEs = new HashSet<QTETrigger>();
    private Dictionary<QTETrigger, QTEData> QTELookup;

    


    private void Awake()
    {
        QTELookup = new Dictionary<QTETrigger, QTEData>();

        foreach (var QTE in QTEs)
        {
            if (QTE.trigger == null)
                continue;

            if (!QTELookup.ContainsKey(QTE.trigger))
                QTELookup.Add(QTE.trigger, QTE);
        }
        QTEPanel.SetActive(false);
    }

    void FixedUpdate()
    {
        marker.rectTransform.Rotate(new Vector3(0, 0, -1));
        if (!QTEPanel.activeSelf)
        {
            successField.rectTransform.Rotate(new Vector3(0, 0, -2));
        }
        
    }

    void Update()
    {
        if(QTEPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                float markerAngle = marker.rectTransform.eulerAngles.z;
                float successAngle = successField.rectTransform.eulerAngles.z;

                    if (markerAngle < successAngle && markerAngle > successAngle - 36)
                    {
                        QTEPanel.SetActive(false);
                        GlobalEvents.RaiseOnMovementOn();
                    }
                    else
                    {
                        Debug.Log("failed");
                    }
            }
        }
    }


    public void TriggerQTE(QTETrigger trigger)
    {
        if (wonQTEs.Contains(trigger))
            return;
        if (!QTELookup.ContainsKey(trigger))
            return;
        var data = QTELookup[trigger];

        QTEPanel.SetActive(true);
        GlobalEvents.RaiseOnMovementOff();
    }

    
}
