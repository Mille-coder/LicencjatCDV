using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] public GameObject respawnPoint;
    [SerializeField] public CinemachineConfiner CameraConfiner;
    [SerializeField] public Collider2D respawnConfiner;

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
        CameraConfiner.m_BoundingShape2D = respawnConfiner;

    }
}
