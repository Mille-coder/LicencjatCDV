using UnityEngine;

public class TaskStepCompleter : MonoBehaviour
{
    [Header("Task Manager")]
    [SerializeField] private TaskManager taskManager;

    [Header("Które zadanie koñczy ten obiekt?")]
    [SerializeField] private int taskIndex;

    private bool completed = false;

    public void CompleteTask()
    {
        if (completed) return;
        if (taskManager == null) return;

        if (taskManager.GetCurrentTaskIndex() != taskIndex)
        {
            Debug.Log("Ten obiekt próbuje wykonaæ zadanie " + taskIndex + ", ale aktualne zadanie to " + taskManager.GetCurrentTaskIndex());
            return;
        }

        completed = true;
        taskManager.CompleteTask(taskIndex);
    }
}