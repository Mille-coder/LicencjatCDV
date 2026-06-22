using UnityEngine;

public class DisableWithTarget : MonoBehaviour
{
    public GameObject targetObject;

    void Update()
    {
        if (targetObject != null && targetObject.activeSelf == false)
        {
            gameObject.SetActive(false);
        }
    }
}