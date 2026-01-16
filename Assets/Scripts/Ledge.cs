using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField] public GameObject targetpos;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ledge"))
        {
            var player = other.transform.parent.GetComponent<Movement>();
            if (player != null)
            {
                player.Grabledge(this);
            }
        }
    }

    public Vector3 Gettargetpos()
    {
        return targetpos.transform.position;
    }
}
