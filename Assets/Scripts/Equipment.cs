using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] GameObject axe;
    public void Grabaxe()
    {
        axe.SetActive(true);
    }
}
