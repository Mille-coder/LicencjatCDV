using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scripts : MonoBehaviour
{
    [SerializeField] public int movespeed = 1;
    [SerializeField] public int jumppower;
    
    private bool grounded = true;
    private Rigidbody playerRB;
    
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (grounded == true )
        {
            playerRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movespeed , playerRB.velocity.y);

            if (Input.GetButtonDown("Jump"))
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded =true;
        }
    }
}
