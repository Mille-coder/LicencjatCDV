using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] public GameObject respawnPoint;

    private void OnEnable()
    {
        GlobalEvents.OnPlayerDeath += Die;
    }

    private void OnDisable()
    {
        GlobalEvents.OnPlayerDeath -= Die;
    }
    void Die()
    {
        transform.position = respawnPoint.transform.position;
    }
}
