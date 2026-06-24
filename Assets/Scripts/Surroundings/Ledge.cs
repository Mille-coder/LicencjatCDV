using UnityEngine;

public class Ledge : MonoBehaviour
{
    [Header("Ledge Points")]
    [SerializeField] private Transform hangPosition;
    [SerializeField] private Transform climbTargetPosition;

    [Header("Player Facing Direction")]
    [SerializeField] private bool playerFacesRight = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ledge"))
            return;

        Movement player = other.transform.parent.GetComponent<Movement>();

        if (player != null)
        {
            player.Grabledge(this);
        }
    }

    public Vector3 GetHangPosition()
    {
        if (hangPosition != null)
            return hangPosition.position;

        return transform.position;
    }

    public Vector3 Gettargetpos()
    {
        if (climbTargetPosition != null)
            return climbTargetPosition.position;

        return transform.position;
    }

    public Quaternion GetHangRotation()
    {
        if (playerFacesRight)
            return Quaternion.Euler(0f, 90f, 0f);

        return Quaternion.Euler(0f, -90f, 0f);
    }
}