using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraLimitChanger : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] CinemachineConfiner confiner;
    [SerializeField] Collider2D newbounds;

    void OnTriggerEnter(Collider other)
    { if(other.gameObject.tag == "Player")
        {
            confiner.m_BoundingShape2D = newbounds;
        }
    }

}
