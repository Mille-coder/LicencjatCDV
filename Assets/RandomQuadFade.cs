using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class RandomQuadFade : MonoBehaviour
{
    [Header("Spawn area")]
    [SerializeField] private Vector3 center = Vector3.zero;
    [SerializeField] private Vector2 areaSize = new Vector2(8f, 4f);

    [Header("Fade")]
    [SerializeField] private float visibleTime = 0.4f;
    [SerializeField] private float fadeDuration = 1.2f;
    [SerializeField] private float respawnDelay = 0.2f;

    private Renderer rend;
    private Material runtimeMaterial;
    private Color baseColor;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        // Unity zrobi instancję materiału tylko dla tego obiektu
        runtimeMaterial = rend.material;
        baseColor = runtimeMaterial.color;
    }

    private void OnEnable()
    {
        StartCoroutine(FadeLoop());
    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {
            MoveRandom();

            SetAlpha(1f);

            yield return new WaitForSeconds(visibleTime);

            float t = 0f;

            while (t < fadeDuration)
            {
                t += Time.deltaTime;
                SetAlpha(1f - (t / fadeDuration));
                yield return null;
            }

            SetAlpha(0f);

            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private void MoveRandom()
    {
        float x = Random.Range(-areaSize.x * 0.5f, areaSize.x * 0.5f);
        float y = Random.Range(-areaSize.y * 0.5f, areaSize.y * 0.5f);

        transform.position = center + new Vector3(x, y, 0f);
    }

    private void SetAlpha(float alpha)
    {
        Color c = baseColor;
        c.a = alpha;
        runtimeMaterial.color = c;
    }
}