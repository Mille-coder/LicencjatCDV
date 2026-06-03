using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    [SerializeField] private TaskStepCompleter taskStepCompleter;
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        taskStepCompleter.CompleteTask();

        gameObject.SetActive(false);
    }
}