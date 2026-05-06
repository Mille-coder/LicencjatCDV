using System.Collections;
using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    [SerializeField] private float delay = 1f;
    [SerializeField] private float fadeDuration = 2f;

    private Renderer[] renderers;
    private Material[] materials;

    public void Begin()
    {
        renderers = GetComponentsInChildren<Renderer>();

        materials = new Material[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
        }

        StartCoroutine(FadeRoutine());
    }

    private IEnumerator FadeRoutine()
    {
        yield return new WaitForSeconds(delay);

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);

            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].HasProperty("_Color"))
                {
                    Color c = materials[i].color;
                    c.a = alpha;
                    materials[i].color = c;
                }
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}