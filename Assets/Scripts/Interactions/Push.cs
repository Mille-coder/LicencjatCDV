using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour, IInteractable
{
    private bool pulling = false;
    private float movespeed = 3;
    [SerializeField] private Rigidbody me;
    public bool CanInteract()
    {
        return true;
    }


    private void Update()
    {
        if(pulling == true)
        {
            me.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movespeed , me.velocity.y);
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
            pulling = false;
            interactor.gameObject.GetComponent<Movement>().Push();
        } 
       
        

        return true;
    }

    // Start is called before the first frame update
    
}
