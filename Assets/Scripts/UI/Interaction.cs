using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                gameObject.SetActive(false);
                GlobalEvents.RaiseOnMovementOn();
            }
        }
    }
}
