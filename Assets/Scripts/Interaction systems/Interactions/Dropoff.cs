using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropoff: MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Woman1;
    [SerializeField] GameObject Woman2;
    [SerializeField] GameObject CarriedWoman;
    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    public bool Interact(Interactor interactor)
    {
        if (Woman1.activeSelf)
        {
            Woman2.SetActive(true);
            CarriedWoman.SetActive(false);
            gameObject.SetActive(false);
            return true;
        }
        Woman1.SetActive(true);
        CarriedWoman.SetActive(false);
        gameObject.SetActive(false);
        return true;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
