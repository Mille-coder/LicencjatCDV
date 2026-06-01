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

    private const string FresnelProperty = "_Fresnel";
    private Color originalFresnelColor;

    private void Awake()
    {
        rend = GetComponent<Renderer>();

        // instancja materiału dla tego obiektu
        runtimeMaterial = rend.material;

        if (runtimeMaterial.HasProperty(FresnelProperty))
        {
            originalFresnelColor = runtimeMaterial.GetColor(FresnelProperty);
        }
        else
        {
            Debug.LogError($"Shader nie posiada właściwości {FresnelProperty}");
            enabled = false;
        }
    }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(FadeLoop());
    }

    private IEnumerator FadeLoop()
    {
        while (true)
        {
            MoveRandom();

            // pełna widoczność
            runtimeMaterial.SetColor(FresnelProperty, originalFresnelColor);

            yield return new WaitForSeconds(visibleTime);

            float t = 0f;

            while (t < fadeDuration)
            {
                t += Time.deltaTime;

                float progress = Mathf.Clamp01(t / fadeDuration);

                Color fresnelColor =
                    Color.Lerp(originalFresnelColor, Color.black, progress);

                runtimeMaterial.SetColor(FresnelProperty, fresnelColor);

                yield return null;
            }

            runtimeMaterial.SetColor(FresnelProperty, Color.black);

            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private void MoveRandom()
    {
        float x = Random.Range(-areaSize.x * 0.5f, areaSize.x * 0.5f);
        float y = Random.Range(-areaSize.y * 0.5f, areaSize.y * 0.5f);

        transform.position = center + new Vector3(x, y, 0f);
    }

    private void OnDisable()
    {
        if (runtimeMaterial != null)
        {
            runtimeMaterial.SetColor(FresnelProperty, originalFresnelColor);
        }
    }
}