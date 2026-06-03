using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [System.Serializable]
    public class TaskData
    {
        [TextArea(1, 3)]
        public string taskName;
    }

    [Header("Lista zadań")]
    [SerializeField] private List<TaskData> tasks = new List<TaskData>();

    [Header("UI")]
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private TMP_Text checkboxText;
    [SerializeField] private TMP_Text taskText;

    [Header("Ustawienia")]
    [SerializeField] private float delayBeforeNextTask = 0.8f;

    private int currentTaskIndex = 0;
    private bool taskCompleted = false;

    private void Start()
    {
        ShowCurrentTask();
    }

    private void ShowCurrentTask()
    {
        if (currentTaskIndex >= tasks.Count)
        {
            taskPanel.SetActive(false);
            return;
        }

        taskCompleted = false;

        taskPanel.SetActive(true);
        checkboxText.text = "☐";
        taskText.text = tasks[currentTaskIndex].taskName;
    }

    public void CompleteTask(int taskIndex)
    {
        if (taskCompleted) return;
        if (currentTaskIndex >= tasks.Count) return;

        if (taskIndex != currentTaskIndex)
        {
            Debug.Log("To nie jest aktualne zadanie. Aktualne: " + currentTaskIndex + ", próbowano wykonać: " + taskIndex);
            return;
        }

        taskCompleted = true;
        checkboxText.text = "☑";

        StartCoroutine(GoToNextTaskAfterDelay());
    }

    private IEnumerator GoToNextTaskAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeNextTask);

        currentTaskIndex++;

        ShowCurrentTask();
    }

    public int GetCurrentTaskIndex()
    {
        return currentTaskIndex;
    }
}