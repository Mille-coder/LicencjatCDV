using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] GameObject axe;
    public bool hasAxe = false;
    public void Grabaxe()
    {

        axe.SetActive(true);

        hasAxe = true;
    }
}
