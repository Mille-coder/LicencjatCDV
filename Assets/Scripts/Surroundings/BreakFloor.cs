using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakFloor : MonoBehaviour
{
   private Transform[] children;
   [SerializeField] float duration = 2f;
    void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        
        var children = new List<Transform>();
        

    for (int i = 0; i < gameObject.transform.childCount; i++) 
    {
    children.Add(gameObject.transform.GetChild(i));
    }

    for (int i = 0; i < children.Count; i++) 
    {
    children[i].gameObject.AddComponent<Rigidbody>();
    }       
    StartCoroutine(DoSomething(duration));
    
    }

    IEnumerator DoSomething(float duration)
    {
        Debug.Log("Before");
        // waits here
        yield return new WaitForSeconds(duration);
        Debug.Log("After");
        var children = new List<Transform>();

        for (int i = 0; i < gameObject.transform.childCount; i++) 
        {
        children.Add(gameObject.transform.GetChild(i));
        }
        for (int i = 0; i < children.Count; i++) 
        {
        Destroy(children[i].gameObject);
        }       
    }

}
