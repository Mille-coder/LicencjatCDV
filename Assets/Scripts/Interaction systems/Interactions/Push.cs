using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class NewBehaviourScript : MonoBehaviour, IInteractable
{
    [SerializeField] private bool pulling = false;
    private float movespeed = 3;
    [SerializeField] private Rigidbody me;

    // 🎧 FMOD
    [SerializeField] private EventReference moveEvent;
    private EventInstance moveInstance;
    private bool isMoving = false;

    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    private void Start()
    {
        moveInstance = RuntimeManager.CreateInstance(moveEvent);
    }

    private void Update()
    {
        if (pulling == true)
        {
            float input = Input.GetAxisRaw("Horizontal");

            me.velocity = new Vector2(input * movespeed, me.velocity.y);

            // 👉 START dźwięku przy ruchu
            if (Mathf.Abs(input) > 0.1f)
            {
                if (!isMoving)
                {
                    moveInstance.start();
                    isMoving = true;
                }
            }
            else
            {
                // 👉 STOP gdy brak ruchu
                if (isMoving)
                {
                    moveInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                    isMoving = false;
                }
            }
        }
    }

    public bool Interact(Interactor interactor)
    {
        if (pulling == false)
        {
            pulling = true;
            interactor.gameObject.GetComponent<Movement>().Push();
        }
        else
        {
            me.velocity = new Vector2(0, 0);
            pulling = false;

            // 🎧 STOP audio przy puszczeniu
            if (isMoving)
            {
                moveInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                isMoving = false;
            }

            interactor.gameObject.GetComponent<Movement>().Push();
        }

        return true;
    }
}