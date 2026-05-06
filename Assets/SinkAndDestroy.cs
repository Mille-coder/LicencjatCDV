using System.Collections;
using UnityEngine;

public class SinkAndDestroy : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    [SerializeField] private float duration = 2f;
    [SerializeField] private float sinkDistance = 0.5f;

    private void Start()
    {
        StartCoroutine(SinkRoutine());
    }

    private IEnumerator SinkRoutine()
    {
        yield return new WaitForSeconds(delay);

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.down * sinkDistance;

        float t = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;

            transform.position = Vector3.Lerp(
                startPos,
                endPos,
                t / duration
            );

            yield return null;
        }

        Destroy(gameObject);
    }
}