using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] GameObject axe;
    [SerializeField] GameObject woman;
    public bool hasAxe = false;
    public bool haswoman = false;
    public void Grabaxe()
    {

        axe.SetActive(true);

        hasAxe = true;
    }

    public void Grabwoman()
    {
        woman.SetActive(true);

        haswoman = true;
    }
}
