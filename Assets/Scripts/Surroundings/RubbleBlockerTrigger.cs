using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleBlockerTrigger : MonoBehaviour
{
    [SerializeField] GameObject rubble;

    void OnTriggerEnter(Collider other)
    {
        rubble.SetActive(true);

    }
}
