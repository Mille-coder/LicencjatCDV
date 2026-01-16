using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] public int movespeed = 1;
    [SerializeField] public int jumppower;
    [SerializeField] bool onLedge = false;
    [SerializeField] GameObject InteractionRange;
    private Ledge activeLedge;
    private bool pushing = false;
    
    private bool grounded = true;

    private bool hanging = false;
    private Rigidbody playerRB;
    
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onLedge == true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position =activeLedge.Gettargetpos();
                onLedge = false;
                playerRB.isKinematic = false;
            }
        }
        if(onLedge == false)
        {
            if (grounded == true )
        {
            playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movespeed , playerRB.velocity.y);

            if (Input.GetButtonDown("Jump") && pushing == false)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x , jumppower);
                grounded = false;
            }
            
        }

        if (Input.GetButtonUp("Jump"))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x , playerRB.velocity.y * 0.5f);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x * 0.5f , playerRB.velocity.y);
        }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            
        }

        
    }

    void FixedUpdate()
    {
        
            if(playerRB.velocity.x <0)
                {
                    Turn(true);
                }
            if(playerRB.velocity.x >0)
                {
                    Turn(false);
                }
    
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded =true;
        }
    }

    void Turn(bool direction)
    {
        if(pushing == false)
        {
            if(direction == true)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);

        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        }
        
    }

    public void Grabledge(Ledge currentLedge)
    {
        onLedge = true;
        activeLedge = currentLedge;
        playerRB.isKinematic = true;
    }

    public void Push()
    {
        pushing = !pushing;
    }
}
