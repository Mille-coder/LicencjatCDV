using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTETrigger : MonoBehaviour
{
    private QTESystem popupSystem;

    private void Start()
    {
        popupSystem = FindObjectOfType<QTESystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        popupSystem.TriggerQTE(GetComponent<QTETrigger>());
    }
}
