using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] GameObject checkPoint;
    [SerializeField] Collider2D checkpontConfiner;
    [SerializeField] DeathManager playerDeathManager;
    void OnTriggerEnter(Collider other)
    { if(other.gameObject.tag == "Player")
        {
            playerDeathManager.respawnPoint = checkPoint;
            playerDeathManager.respawnConfiner = checkpontConfiner;
        }
    }
}
