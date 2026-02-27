using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakFloor : MonoBehaviour
{
   [SerializeField] GameObject floor;

    void OnTriggerEnter(Collider other)
    {
        Destroy(floor);
    }

}
