using UnityEngine;

public class Equipment : MonoBehaviour
{
    [SerializeField] private GameObject axe;
    [SerializeField] private GameObject woman;

    public bool hasAxe = false;
    public bool haswoman = false;

    public void Grabaxe()
    {
        if (axe != null)
            axe.SetActive(true);

        hasAxe = true;
    }

    public void Grabwoman()
    {
        if (woman != null)
            woman.SetActive(true);

        haswoman = true;
    }

    public void Dropwoman()
    {
        if (woman != null)
            woman.SetActive(false);

        haswoman = false;
    }
}